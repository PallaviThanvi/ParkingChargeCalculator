using System;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Factory interface for creating parking charge calculators
    /// </summary>
    public interface IParkingTicketFactory
    {
        /// <summary>
        /// Creates parking calculator for short stay
        /// </summary>
        /// <param name="location">Parking location</param>
        /// <param name="parkingDate">Parking date</param>
        /// <exception cref="ArgumentNullException">location</exception>
        /// <returns>Parking calculator for short stay</returns>
        IShortStayParkingChargeCalculator CreateShortStayParkingChargeCalculator(string location, DateTime parkingDate);

        /// <summary>
        /// Creates parking calculator for long stay
        /// </summary>
        /// <param name="location">Parking location</param>
        /// <param name="parkingDate">Parking date</param>
        /// <returns>Parking calculator for long stay</returns>
        /// <exception cref="ArgumentNullException">location</exception>
        ILongStayParkingChargeCalculator CreateLongStayParkingChargeCalculator(string location, DateTime parkingDate);
    }
}
