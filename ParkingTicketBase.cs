using System;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Base class for parking ticket
    /// </summary>
    public abstract class ParkingTicketBase : IParkingTicket
    {
        /// <see cref="IParkingTicket.Location"/>
        public string Location { get; }

        /// <see cref="IParkingTicket.ParkingDate"/>
        public DateTime ParkingDate { get; }       

        /// <summary>
        /// Initializes new instance of parking ticket.
        /// </summary>
        /// <param name="location">Parking location</param>
        /// <param name="parkingDate">Parking Date</param>
        /// <exception cref="ArgumentNullException">location</exception>
        protected ParkingTicketBase(string location, DateTime parkingDate)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            ParkingDate = parkingDate;
        }
    }
}
