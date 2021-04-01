namespace ParkingChargeCalculator
{
    /// <summary>
    /// Interface for long stay parking ticket
    /// </summary>
    public interface ILongStayParkingTicket : IParkingTicket
    {
        /// <summary>
        /// Parking charge per day
        /// </summary>
        double ParkingChargePerDay { get; }
    }
}
