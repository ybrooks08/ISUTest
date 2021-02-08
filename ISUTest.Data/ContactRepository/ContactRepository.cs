using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using ISUTest.Data.Entities;
using ISUTest.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ISUTest.Data.ContactRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ISUDbContext _db;
        private readonly string _connectionString;
        public ContactRepository( IConfiguration configuration, ISUDbContext db1 )
        {
            _connectionString = configuration.GetConnectionString( "DefaultConnection" );
            _db = db1;
        }

        //To add a contact I will use Entity framework.
        /// <summary>
        /// Add a Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task<int> AddContact( Contact contact )
        {
            if ( _db != null )
            {
                await _db.Contacts.AddAsync( contact );
                await _db.SaveChangesAsync();

                return contact.Id;
            }

            return 0;
        }

        /// <summary>
        /// Updating a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task UpdateContact( Contact contact )
        {
            if ( _db != null )
            {
                //Update Contact
                _db.Contacts.Update( contact );

                //Commit the transaction
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public async Task<int> DeleteContact( int? contactId )
        {
            int result = 0;

            if ( _db != null )
            {
                //Find the post for specific post id
                var post = await _db.Contacts.FirstOrDefaultAsync( x => x.Id == contactId );

                if ( post != null )
                {
                    //Delete that post
                    _db.Contacts.Remove( post );

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        /// <summary>
        /// To get a contact by name, I will use a stored procedure to achieve one of test goals.
        /// </summary>
        /// <param name="contactName"></param>
        /// <returns></returns>
        public ContactViewModel GetContactByName( string contactName)
        {
            ContactViewModel contact = new ContactViewModel();
            try
            {
                
                string sql = "SP_GetContactByName";
                using ( SqlConnection connection = new SqlConnection( _connectionString ) )
                {
                    using ( SqlCommand command = new SqlCommand( sql, connection ) )
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if ( !string.IsNullOrEmpty( contactName ) )
                            command.Parameters.Add( "@contactName", SqlDbType.VarChar ).Value = contactName;
                        else
                            command.Parameters.Add( "@contactName", SqlDbType.VarChar ).Value = DBNull.Value;
                        connection.Open();
                        using ( SqlDataReader dataReader = command.ExecuteReader() )
                        {
                            if ( dataReader.HasRows && dataReader.Read() )
                            {
                                contact.Id = Convert.ToInt32( dataReader["contacId"] );
                                contact.Name = dataReader["name"].ToString();
                                contact.PhoneNumber = dataReader["phoneNumber"].ToString();
                                contact.BirthDate = Convert.ToDateTime( dataReader["birth"].ToString() );
                                contact.ContactTypeId = Convert.ToInt32( dataReader["contactTypeId"] );
                                contact.ContactType = dataReader["contactTyppe"].ToString();
                            }
                        } 
                    }
                }
            }
            catch ( Exception ex )
            {
                throw;
            }
            return contact;
        }

        public List<ContactViewModel> GetContacts()
        {
            List<ContactViewModel> contactList = new List<ContactViewModel>();
            try
            {

                string sql = "SP_GetContacts";
                using ( SqlConnection connection = new SqlConnection( _connectionString ) )
                {
                    SqlCommand command = new SqlCommand( sql, connection );
                    connection.Open();
                    using ( SqlDataReader dataReader = command.ExecuteReader() )
                    {
                        while ( dataReader.Read() )
                        {
                            ContactViewModel contact = new ContactViewModel();
                            contact.Id = Convert.ToInt32( dataReader["id"] );
                            contact.Name = dataReader["name"].ToString();
                            contact.PhoneNumber = dataReader["phoneNumber"].ToString();
                            contact.BirthDate = Convert.ToDateTime( dataReader["birth"].ToString() );
                            contact.ContactTypeId = Convert.ToInt32( dataReader["contactTypeId"] );
                            contact.ContactType = dataReader["contactTyppe"].ToString();
                            contactList.Add( contact );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                throw;
            }
            return contactList;
        }
    }
}
