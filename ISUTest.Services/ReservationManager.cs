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
    public class ReservationManager : IReservationManager
    {
        private IReservationRepository _repo;
        private IContactRepository _repoContact;
        public IMapper _mapper;

        public ReservationManager( IReservationRepository repo, IContactRepository repoContact, IMapper mapper )
        {
            _repo = repo;
            _repoContact = repoContact;
            _mapper = mapper;
        }

        public async Task<int> AddReservation( ReservationViewModel model )
        {
            try
            {
                Reservation reservation = _mapper.Map<Reservation>( model );
                
                
                //Finding contact by name to know if exits
                var tempContact = _repoContact.GetContactByName( model.ContactName );
                if ( tempContact.Id == 0 )
                {
                    //Trying other way to map
                    Contact contact = new Contact
                    {
                        Name = model.ContactName,
                        PhoneNumber = model.PhoneNumber,
                        Birth = model.Birth,
                        ContactTypeId = model.ContactTypeId
                    };
                    //Adding contact if not exists
                    await _repoContact.AddContact( contact ); 
                }
                var result = await _repo.AddReservation( reservation );
                //Returning Reservation Id
                return result;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public List<ReservationViewModel> GetReservations()
        {
            try
            {
                List<ReservationViewModel> reservations = _repo.GetReservations();
                return reservations;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateReservation( ReservationViewModel model )
        {
            try
            {
                Reservation reservation = _mapper.Map<Reservation>( model );
                await _repo.UpdateReservation( reservation );
                //Returning true if everithing went fine
                return true;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }
    }
}
