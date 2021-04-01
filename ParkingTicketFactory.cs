using System;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Factory for creating parking charge calculators
    /// </summary>
    public class ParkingTicketFactory : IParkingTicketFactory
    {
        /// <see cref="IParkingTicketFactory.CreateShortStayParkingChargeCalculator"/>
        public IShortStayParkingChargeCalculator CreateShortStayParkingChargeCalculator(string location, DateTime parkingDate) => new ShortStayParkingTicket(location, parkingDate);

        /// <see cref="IParkingTicketFactory.CreateLongStayParkingChargeCalculator"/>
        public ILongStayParkingChargeCalculator CreateLongStayParkingChargeCalculator(string location, DateTime parkingDate) => new LongStayParkingTicket(location, parkingDate);
    }
}
