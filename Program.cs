using System;

namespace ParkingChargeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            IParkingTicketFactory factory = new ParkingTicketFactory();
            CarPark carPark = new CarPark("Edinburgh", factory);

            IShortStayParkingTicket shortStayParkingTicket = carPark.ParkForShortStay(new DateTime(2017, 9, 7, 16, 50, 0));
            Console.WriteLine($"**** Short Stay Parking ****");
            Console.WriteLine($"Parking location: {shortStayParkingTicket.Location}");
            Console.WriteLine($"Parking date: {shortStayParkingTicket.ParkingDate}");
            Console.WriteLine($"Parking charge per hour: £{shortStayParkingTicket.ParkingChargePerHour}");

            DateTime leave = new DateTime(2017, 9, 9, 19, 15, 0);
            Console.WriteLine($"Total Parking charge on {leave}: £{carPark.Leave(shortStayParkingTicket, leave)}");

            Console.WriteLine();

            shortStayParkingTicket = carPark.ParkForShortStay(new DateTime(2021, 3, 6, 16, 50, 0));
            Console.WriteLine($"**** Short Stay Parking On Weekend****");
            Console.WriteLine($"Parking location: {shortStayParkingTicket.Location}");
            Console.WriteLine($"Parking date: {shortStayParkingTicket.ParkingDate}");
            Console.WriteLine($"Parking charge per hour: £{shortStayParkingTicket.ParkingChargePerHour}");

            leave = new DateTime(2021, 3, 7, 19, 15, 0);
            Console.WriteLine($"Total Parking charge on {leave}: £{carPark.Leave(shortStayParkingTicket, leave)}");

            Console.WriteLine();

            shortStayParkingTicket = carPark.ParkForShortStay(new DateTime(2021, 3, 10, 20, 50, 0));
            Console.WriteLine($"**** Short Stay Parking Outside Billing Hours****");
            Console.WriteLine($"Parking location: {shortStayParkingTicket.Location}");
            Console.WriteLine($"Parking date: {shortStayParkingTicket.ParkingDate}");
            Console.WriteLine($"Parking charge per hour: £{shortStayParkingTicket.ParkingChargePerHour}");

            leave = new DateTime(2021, 3, 11, 7, 45, 0);
            Console.WriteLine($"Total Parking charge on {leave}: £{carPark.Leave(shortStayParkingTicket, leave)}");

            Console.WriteLine();

            ILongStayParkingTicket longStayParkingTicket = carPark.ParkForLongStay(new DateTime(2017, 9, 7, 7, 50, 0));
            Console.WriteLine($"**** Long Stay Parking ****");
            Console.WriteLine($"Parking location: {longStayParkingTicket.Location}");
            Console.WriteLine($"Parking date: {longStayParkingTicket.ParkingDate}");
            Console.WriteLine($"Parking charge per day: £{longStayParkingTicket.ParkingChargePerDay}");

            leave = new DateTime(2017, 9, 9, 5, 20, 0);
            Console.WriteLine($"Total Parking charge on {leave}: £{carPark.Leave(longStayParkingTicket, leave)}");

            Console.ReadLine();
        }
    }
}
