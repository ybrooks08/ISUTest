using System.Collections.Generic;
using System.Threading.Tasks;
using ISUTest.Data.Entities;
using ISUTest.Data.Models;

namespace ISUTest.Data.ReservationRepository
{
    public interface IReservationRepository
    {
        /// <summary>
        /// Add a reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        Task<int> AddReservation( Reservation reservation );        

        /// <summary>
        /// Getting all reservations
        /// </summary>
        /// <returns></returns>
        List<ReservationViewModel> GetReservations();

        Task UpdateReservation( Reservation reservation );
    }
}
