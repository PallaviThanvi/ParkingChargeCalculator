using System;
using System.Collections.Generic;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Represents a car park
    /// </summary>
    public class CarPark
    {
        readonly string location;
        readonly IParkingTicketFactory parkingTicketFactory;
        readonly Dictionary<IParkingTicket, IParkingChargeCalculator> parkingTickets;

        /// <summary>
        /// Initializes new instance of car park.
        /// </summary>
        /// <param name="location">Location of the car park</param>
        /// <param name="parkingTicketFactory">Factory for returning parking charge calculator</param>
        /// <exception cref="ArgumentNullException">location or parkingTicketFactory is null</exception>
        public CarPark(string location, IParkingTicketFactory parkingTicketFactory)
        {
            this.location = location ?? throw new ArgumentNullException(nameof(location));
            this.parkingTicketFactory = parkingTicketFactory ?? throw new ArgumentNullException(nameof(parkingTicketFactory));
            
            parkingTickets = new Dictionary<IParkingTicket, IParkingChargeCalculator>();
        }

        /// <summary>
        /// Allows car parking for short stay
        /// </summary>
        /// <returns>Parking ticket for short stay</returns>
        public IShortStayParkingTicket ParkForShortStay() => ParkForShortStay(DateTime.Now);

        /// <summary>
        /// Allows car parking for short stay
        /// </summary>
        /// <param name="parkingDate">Parking date</param>
        /// <returns>Parking ticket for short stay</returns>
        public IShortStayParkingTicket ParkForShortStay(DateTime parkingDate)
        {
            IShortStayParkingChargeCalculator ticket = parkingTicketFactory.CreateShortStayParkingChargeCalculator(location, parkingDate);
            parkingTickets[ticket] = ticket;

            return ticket;
        }

        /// <summary>
        /// Allows car parking for long stay
        /// </summary>
        /// <returns>Parking ticket for long stay</returns>
        public ILongStayParkingTicket ParkForLongStay() => ParkForLongStay(DateTime.Now);

        /// <summary>
        /// Allows car parking for long stay
        /// </summary>
        /// <param name="parkingDate">Parking date</param>
        /// <returns>Parking ticket for long stay</returns>
        public ILongStayParkingTicket ParkForLongStay(DateTime parkingDate)
        {
            ILongStayParkingChargeCalculator ticket = parkingTicketFactory.CreateLongStayParkingChargeCalculator(location, parkingDate);
            parkingTickets[ticket] = ticket;

            return ticket;
        }

        /// <summary>
        /// Allows cars to leave the parking
        /// </summary>
        /// <param name="ticket">Ticket to calculate the parking charges</param>
        /// <returns>Parking charge</returns>
        public double Leave(IParkingTicket ticket) => Leave(ticket, DateTime.Now);

        /// <summary>
        /// Allows cars to leave the parking
        /// </summary>
        /// <param name="ticket">Ticket to calculate the parking charges</param>
        /// <param name="leavingDate">Date leaving the car park</param>
        /// <returns>Parking charge</returns>
        public double Leave(IParkingTicket ticket, DateTime leavingDate)
        {
            if (!parkingTickets.TryGetValue(ticket, out IParkingChargeCalculator chargeCalculator))
                throw new InvalidOperationException("Invalid ticket");

            parkingTickets.Remove(ticket);
            return chargeCalculator.CalculateParkingCharge(leavingDate);
        }
    }
}
