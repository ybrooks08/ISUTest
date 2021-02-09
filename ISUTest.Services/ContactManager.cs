using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ISUTest.Data.ContactRepository;
using ISUTest.Data.Entities;
using ISUTest.Data.Models;
using ISUTest.Data.ReservationRepository;
using ISUTest.Interfaces;

namespace ISUTest.Services
{
    public class ContactManager : IContactManager
    {
        private readonly IContactRepository _repo;
        public IMapper _mapper;

        public ContactManager( IContactRepository repo, IMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> AddContact( ContactViewModel model )
        {
            try
            {
                Contact contact = _mapper.Map<Contact>( model );
                var result = await _repo.AddContact( contact );
                return result;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
        
        public async Task<bool> UpdateContact( ContactViewModel model )
        {
            try
            {
                Contact contact = _mapper.Map<Contact>( model );
                await _repo.UpdateContact( contact );
                return true;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public async Task<int> DeleteContact( int? contactId )
        {
            try
            {
                var result = await _repo.DeleteContact( contactId );
                return result;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public ContactViewModel GetContactByName( string contactName )
        {
            try
            {
                //Making validations from server side
                if ( !string.IsNullOrEmpty( contactName ) )
                {
                    ContactViewModel contact = _repo.GetContactByName( contactName );
                    return contact;
                }
                else
                    //Here we could manage a kind of exception to take an action according to the requirements.
                    return null;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public List<ContactViewModel> GetContacts()
        {
            try
            {
                List<ContactViewModel> contacts = _repo.GetContacts();
                return contacts;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
        
        public List<ContactTypeViewModel> GetContactTypes()
        {
            try
            {
                List<ContactTypeViewModel> contactTypes = _repo.GetContactTypes();
                return contactTypes;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
    }
}
