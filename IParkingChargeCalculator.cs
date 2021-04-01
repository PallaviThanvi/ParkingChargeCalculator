using System;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Interface for parking charge calculator
    /// </summary>
    public interface IParkingChargeCalculator
    {
        /// <summary>
        /// Calculates parking charge
        /// <param name="leavingDate">Car park leaving date</param>
        /// </summary>
        double CalculateParkingCharge(DateTime leavingDate);
    }
}
