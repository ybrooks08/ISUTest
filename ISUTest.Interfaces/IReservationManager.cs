using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ISUTest.Data.Models;

namespace ISUTest.Interfaces
{
    public interface IReservationManager
    {

        /// <summary>
        /// Add a reservation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddReservation( ReservationViewModel model );
        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <returns></returns>
        List<ReservationViewModel> GetReservations();
    }
}
