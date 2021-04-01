namespace ParkingChargeCalculator
{
    /// <summary>
    /// Interface for short stay parking ticket
    /// </summary>
    public interface IShortStayParkingTicket : IParkingTicket
    {
        /// <summary>
        /// Parking charge per hour
        /// </summary>
        double ParkingChargePerHour { get; }
    }
}
