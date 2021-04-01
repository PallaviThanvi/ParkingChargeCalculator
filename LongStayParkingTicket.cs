using System;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Represents a long stay parking ticket
    /// </summary>
    public class LongStayParkingTicket : ParkingTicketBase, ILongStayParkingChargeCalculator
    {
        /// <see cref="ILongStayParkingTicket.ParkingChargePerDay"/>
        public double ParkingChargePerDay { get; } = 7.5;

        /// <summary>
        /// Initializes new instance of long stay parking ticket.
        /// </summary>
        /// <param name="location">Parking location</param>
        /// <param name="parkingDate">Parking Date</param>
        /// <exception cref="ArgumentNullException">location</exception>
        public LongStayParkingTicket(string location, DateTime parkingDate) : base(location, parkingDate)
        { }

        /// <see cref="IParkingChargeCalculator.CalculateParkingCharge"/>
        public double CalculateParkingCharge(DateTime leavingDate)
        {
            int noOfDaysParked = (leavingDate.Date - ParkingDate.Date).Days + 1;
            return noOfDaysParked * ParkingChargePerDay;
        }
    }
}
