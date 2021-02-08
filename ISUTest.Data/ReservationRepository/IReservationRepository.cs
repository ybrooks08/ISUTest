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

        /*Task<List<ReservationViewModel>> GetReservations();

        Task<ReservationViewModel> GetReservation( int? reservationId );

        Task<int> AddReservation( Reservation reservation );

        Task<int> DeleteReservation( int? reservationId );

        Task UpdateReservation( Reservation reservation );*/
    }
}
