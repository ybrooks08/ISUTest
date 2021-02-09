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
                            reservation.Birth = Convert.ToDateTime( dataReader["birth"].ToString() );
                            reservation.CreatedDate = Convert.ToDateTime( dataReader["createdDate"].ToString() );
                            reservation.ModifiedDate = Convert.ToDateTime( dataReader["modifiedDate"].ToString() );
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
        
        /// <summary>
        /// Update reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public async Task UpdateReservation( Reservation reservation )
        {
            if ( _db != null )
            {
                //Update that reservation
                _db.Reservations.Update( reservation );

                //Commit the transaction
                await _db.SaveChangesAsync();
            }
        }
    }
}
