using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ISUTest.Data.Entities;
using ISUTest.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ISUTest.Data.ReservationRepository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ISUDbContext _db;
        private readonly string _connectionString;
        public ReservationRepository( IConfiguration configuration, ISUDbContext db1 )
        {
            _connectionString = configuration.GetConnectionString( "DefaultConnection" );
            _db = db1;
        }

        //To add a reservation using Entity framework.
        public async Task<int> AddReservation( Reservation reservation )
        {
            if ( _db != null )
            {
                await _db.Reservations.AddAsync( reservation );
                await _db.SaveChangesAsync();

                return reservation.Id;
            }

            return 0;
        }        

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns></returns>
        public List<ReservationViewModel> GetReservations()
        {
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();
            try
            {
                
                string sql = "SP_GetReservations";
                using ( SqlConnection connection = new SqlConnection( _connectionString ) )
                {
                    SqlCommand command = new SqlCommand( sql, connection );
                    connection.Open();
                    using ( SqlDataReader dataReader = command.ExecuteReader() )
                    {
                        while ( dataReader.Read() )
                        {
                            ReservationViewModel reservation = new ReservationViewModel();
                            reservation.Id = Convert.ToInt32( dataReader["reservationId"] );
                            reservation.ContactId = Convert.ToInt32( dataReader["contacId"] );
                            reservation.Description = dataReader["resevDesc"].ToString();
                            reservation.ContactName = dataReader["name"].ToString();
                            reservation.PhoneNumber = dataReader["phoneNumber"].ToString();
                            reservation.ContactTypeId = Convert.ToInt32( dataReader["contactTypeId"] );
                            reservation.ContactTyppe = dataReader["contactTyppe"].ToString();
                            reservation.Stars = Convert.ToInt32( dataReader["stars"].ToString());
                            reservation.Favorite = Convert.ToBoolean( dataReader["favorite"].ToString());
                            reservationList.Add( reservation );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                throw;
            }
            return reservationList;
        }
        /* public async Task<List<Category>> GetCategories()
         {
             if ( _db != null )
             {
                 return await db.Category.ToListAsync();
             }

             return null;
         }*/

        /*public async Task<List<ReservationViewModel>> GetReservations()
        {
            if ( _db != null )
            {
                return await ( from r in _db.Reservations
                               from c in _db.Contacts
                               where r.CategoryId == c.Id
                               select new ReservationViewModel
                               {
                                   Id = r.id,
                                   ContactId = r.contactId,
                                   Description = r.description
                               } ).ToListAsync();
            }

            return null;
        }

        public async GetReservation( int? reservationId )
        {
            if ( _db != null )
            {
                return await ( from p in _db.Post
                               from c in _db.Category
                               where p.PostId == postId
                               select new ReservationViewModel
                               {
                                   PostId = p.PostId,
                                   Title = p.Title,
                                   Description = p.Description,
                                   CategoryId = p.CategoryId,
                                   CategoryName = c.Name,
                                   CreatedDate = p.CreatedDate
                               } ).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddReservation( Reservation reservation )
        {
            if ( _db != null )
            {
                await _db.Post.AddAsync( post );
                await _db.SaveChangesAsync();

                return post.PostId;
            }

            return 0;
        }

        public async Task<int> DeleteReservation( int? reservationId )
        {
            int result = 0;

            if ( _db != null )
            {
                //Find the post for specific post id
                var post = await _db.Post.FirstOrDefaultAsync( x => x.PostId == postId );

                if ( post != null )
                {
                    //Delete that post
                    _db.Post.Remove( post );

                    //Commit the transaction
                    result = await _db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateReservation( Reservation reservation )
        {
            if ( _db != null )
            {
                //Delete that post
                _db.Post.Update( post );

                //Commit the transaction
                await _db.SaveChangesAsync();
            }
        }*/
    }
}
