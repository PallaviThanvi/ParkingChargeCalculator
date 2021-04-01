using System;

namespace ParkingChargeCalculator
{
    /// <summary>
    /// Represents a short stay parking ticket
    /// </summary>
    public class ShortStayParkingTicket : ParkingTicketBase, IShortStayParkingChargeCalculator
    {
        private const double ChargedParkingHoursPerDay = 10;

        /// <see cref="IShortStayParkingTicket.ParkingChargePerHour"/>
        public double ParkingChargePerHour { get; } = 1.1;

        /// <summary>
        /// Initializes new instance of short stay parking ticket.
        /// </summary>
        /// <param name="location">Parking location</param>
        /// <param name="parkingDate">Parking Date</param>
        /// <exception cref="ArgumentNullException">location</exception>
        public ShortStayParkingTicket(string location, DateTime parkingDate) : base(location, parkingDate)
        { }

        /// <see cref="IParkingChargeCalculator.CalculateParkingCharge"/>
        public double CalculateParkingCharge(DateTime leavingDate)
        {
            double CalculateCharge(double minutes) => Math.Round((minutes * ParkingChargePerHour) / 60, 2);
            DateTime GetParkingOpenTime(DateTime date) => new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
            DateTime GetParkingCloseTime(DateTime date) => new DateTime(date.Year, date.Month, date.Day, 18, 0, 0);

            DateTime GetBillingStartTime(DateTime date)
            {
                DateTime start = GetParkingOpenTime(date);
                return date < start ? start : date;
            }

            DateTime GetBillingEndTime(DateTime date)
            {
                DateTime end = GetParkingCloseTime(date);
                return date > end ? end : date;
            }

            if (ParkingDate.Date == leavingDate.Date)
            {
                if (IsWeekend(ParkingDate))
                    return 0;

                DateTime date = GetParkingOpenTime(leavingDate);
                if (ParkingDate < date && leavingDate < date)
                    return 0;

                date = GetParkingCloseTime(leavingDate);
                if (ParkingDate > date && leavingDate > date)
                    return 0;

                return CalculateCharge((GetBillingEndTime(leavingDate) - GetBillingStartTime(ParkingDate)).TotalMinutes);
            }

            double totalNoOfMinutesParked = 0;

            if(!IsWeekend(ParkingDate) && ParkingDate < GetParkingCloseTime(ParkingDate))
            {
                totalNoOfMinutesParked += (GetParkingCloseTime(ParkingDate) - GetBillingStartTime(ParkingDate)).TotalMinutes;
            }

            if (!IsWeekend(leavingDate) && leavingDate > GetParkingOpenTime(leavingDate))
            {
                totalNoOfMinutesParked += (GetBillingEndTime(leavingDate) - GetParkingOpenTime(leavingDate)).TotalMinutes;
            }

            totalNoOfMinutesParked += GetWorkingDaysExclusive(ParkingDate, leavingDate) * ChargedParkingHoursPerDay * 60;

            return CalculateCharge(totalNoOfMinutesParked);
        }

        private static int GetWorkingDaysExclusive(DateTime start, DateTime end)
        {
            start = start.AddDays(1);
            end = end.AddDays(-1);

            while (IsWeekend(start))
                start = start.Date.AddDays(1);

            while (IsWeekend(end))
                end = end.Date.AddDays(-1);

            if (start.Date > end.Date)
                return 0;

            int totalDays = (end.Date - start.Date).Days + 1;
            int totalHolidays = 0;

            int GetHolidays(int days)
            {
                int quotient = Math.DivRem(days, 7, out int rem);
                int holidays = quotient * 2;
                if (rem != 0 && (rem + 1) % 7 == 0)
                {
                    holidays++;
                }

                return holidays;
            }

            switch (start.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        totalHolidays = GetHolidays(totalDays);
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        totalHolidays = GetHolidays(totalDays + 1);
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        totalHolidays = GetHolidays(totalDays + 2);
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        totalHolidays = GetHolidays(totalDays + 3);
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        totalHolidays = GetHolidays(totalDays + 4);
                        break;
                    }
            }

            return totalDays - totalHolidays;
        }

        private static bool IsWeekend(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;       
    }
}
