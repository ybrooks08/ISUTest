using System.Collections.Generic;
using System.Threading.Tasks;
using ISUTest.Data.Entities;
using ISUTest.Data.Models;

namespace ISUTest.Data.ContactRepository
{
    public interface IContactRepository
    {
        /// <summary>
        /// Add a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task<int> AddContact( Contact contact );

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Task UpdateContact( Contact contact );

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        Task<int> DeleteContact( int? contactId );

        /// <summary>
        /// Get a contact by name
        /// </summary>
        /// <param name="contactName"></param>
        /// <returns></returns>
        ContactViewModel GetContactByName( string contactName);

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns></returns>
        List<ContactViewModel> GetContacts();
    }
}
