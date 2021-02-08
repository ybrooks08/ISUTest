using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ISUTest.Data.Models;

namespace ISUTest.Interfaces
{
    public interface IContactManager
    {
        /// <summary>
        /// Add new contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddContact( ContactViewModel model );

        /// <summary>
        /// Update a contact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateContact( ContactViewModel model );

        /// <summary>
        /// Delete a contact
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        Task<int> DeleteContact( int? contactId );

        /// <summary>
        /// Get contact by name
        /// </summary>
        /// <returns></returns>
        ContactViewModel GetContactByName( string contactName);

        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns></returns>
        List<ContactViewModel> GetContacts();
    }
}
