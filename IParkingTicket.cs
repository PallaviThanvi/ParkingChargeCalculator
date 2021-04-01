using System;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Interface for parking ticket
    /// </summary>
    public interface IParkingTicket
    {
        /// <summary>
        /// Parking location
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Parking date
        /// </summary>
        DateTime ParkingDate { get; }
    }
}
