using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ParkingChargeCalculator
{
    [TestFixture]
    public class ParkingChargeCalculatorTests
    {
        public static IEnumerable<TestCaseData> GetShortStayTestCases
        {
            get
            {
                yield return new TestCaseData(new DateTime(2021, 3, 3, 6, 30, 0), new DateTime(2021, 3, 3, 7, 25, 0), 0).SetName("{m} (Same Day - Weekday Parking - Before 8:00 AM)");
                yield return new TestCaseData(new DateTime(2021, 3, 3, 18, 30, 0), new DateTime(2021, 3, 3, 19, 25, 0), 0).SetName("{m} (Same Day - Weekday Parking - After 6:00 PM)");
                yield return new TestCaseData(new DateTime(2021, 3, 3, 8, 30, 0), new DateTime(2021, 3, 3, 17, 25, 0), 9.81).SetName("{m} (Same Day - Weekday Parking - Between 8:00 AM And 6:00 PM)");
                yield return new TestCaseData(new DateTime(2021, 3, 3, 7, 30, 0), new DateTime(2021, 3, 3, 19, 25, 0), 11).SetName("{m} (Same Day - Weekday Parking - Park Before 8:00 AM, Leave After 6:00 PM)");
                yield return new TestCaseData(new DateTime(2021, 3, 6, 6, 30, 0), new DateTime(2021, 3, 6, 19, 25, 0), 0).SetName("{m} (Same Day - Weekend Parking)");
                yield return new TestCaseData(new DateTime(2021, 3, 6, 6, 30, 0), new DateTime(2021, 3, 7, 19, 25, 0), 0).SetName("{m} (Two Days - Weekend Parking)");
                yield return new TestCaseData(new DateTime(2021, 3, 4, 18, 30, 0), new DateTime(2021, 3, 5, 7, 25, 0), 0).SetName("{m} (Two Days - Weekday Parking - Park After 6:00 PM, Leave Next Day Before 8:00 AM)");
                yield return new TestCaseData(new DateTime(2021, 3, 4, 16, 30, 0), new DateTime(2021, 3, 5, 9, 25, 0), 3.21).SetName("{m} (Two Days - Weekday Parking - Park Before 6:00 PM, Leave Next Day After 8:00 AM)");
                yield return new TestCaseData(new DateTime(2021, 3, 1, 16, 30, 0), new DateTime(2021, 3, 4, 9, 25, 0), 25.21).SetName("{m} (Four Days - Parking Starting On A Monday)");
                yield return new TestCaseData(new DateTime(2021, 3, 2, 16, 30, 0), new DateTime(2021, 3, 5, 9, 25, 0), 25.21).SetName("{m} (Four Days - Parking Starting On A Tuesday)");
                yield return new TestCaseData(new DateTime(2021, 3, 3, 16, 30, 0), new DateTime(2021, 3, 6, 9, 25, 0), 23.65).SetName("{m} (Four Days - Parking Starting On A Wednesday)");
                yield return new TestCaseData(new DateTime(2021, 3, 4, 16, 30, 0), new DateTime(2021, 3, 7, 9, 25, 0), 12.65).SetName("{m} (Four Days - Parking Starting On A Thursday)");
                yield return new TestCaseData(new DateTime(2021, 3, 5, 16, 30, 0), new DateTime(2021, 3, 8, 9, 25, 0), 3.21).SetName("{m} (Four Days - Parking Starting On A Friday)");
                yield return new TestCaseData(new DateTime(2021, 3, 6, 16, 30, 0), new DateTime(2021, 3, 9, 9, 25, 0), 12.56).SetName("{m} (Four Days - Parking Starting On A Saturday)");
                yield return new TestCaseData(new DateTime(2021, 3, 7, 16, 30, 0), new DateTime(2021, 3, 10, 9, 25, 0), 23.56).SetName("{m} (Four Days - Parking Starting On A Sunday)");
                yield return new TestCaseData(new DateTime(2021, 3, 1, 16, 30, 0), new DateTime(2021, 3, 22, 9, 25, 0), 157.21).SetName("{m} (Three Weeks - Parking Starting On A Monday)");
                yield return new TestCaseData(new DateTime(2021, 3, 2, 16, 30, 0), new DateTime(2021, 3, 23, 9, 25, 0), 157.21).SetName("{m} (Three Weeks - Parking Starting On A Tuesday)");
                yield return new TestCaseData(new DateTime(2021, 3, 3, 16, 30, 0), new DateTime(2021, 3, 24, 9, 25, 0), 157.21).SetName("{m} (Three Weeks - Parking Starting On A Wednesday)");
                yield return new TestCaseData(new DateTime(2021, 3, 4, 16, 30, 0), new DateTime(2021, 3, 25, 9, 25, 0), 157.21).SetName("{m} (Three Weeks - Parking Starting On A Thursday)");
                yield return new TestCaseData(new DateTime(2021, 3, 5, 16, 30, 0), new DateTime(2021, 3, 26, 9, 25, 0), 157.21).SetName("{m} (Three Weeks - Parking Starting On A Friday)");
                yield return new TestCaseData(new DateTime(2021, 3, 6, 16, 30, 0), new DateTime(2021, 3, 27, 9, 25, 0), 165).SetName("{m} (Three Weeks - Parking Starting On A Saturday)");
                yield return new TestCaseData(new DateTime(2021, 3, 7, 16, 30, 0), new DateTime(2021, 3, 28, 9, 25, 0), 165).SetName("{m} (Three Weeks - Parking Starting On A Sunday)");
            }
        }

        public static IEnumerable<TestCaseData> GetLongStayTestCases
        {
            get
            {
                yield return new TestCaseData(new DateTime(2021, 3, 3, 6, 30, 0), new DateTime(2021, 3, 3, 7, 25, 0), 7.5).SetName("{m} (Same Day - Weekday Parking - Less Than 2 Hours)");
                yield return new TestCaseData(new DateTime(2021, 3, 3, 6, 30, 0), new DateTime(2021, 3, 3, 19, 25, 0), 7.5).SetName("{m} (Same Day - Weekday Parking - More Than 10 Hours)");
                yield return new TestCaseData(new DateTime(2021, 3, 6, 6, 30, 0), new DateTime(2021, 3, 6, 7, 25, 0), 7.5).SetName("{m} (Same Day - Weekend Parking - Less Than 2 Hours)");
                yield return new TestCaseData(new DateTime(2021, 3, 6, 6, 30, 0), new DateTime(2021, 3, 6, 19, 25, 0), 7.5).SetName("{m} (Same Day - Weekend Parking - More Than 10 Hours)");
                yield return new TestCaseData(new DateTime(2021, 3, 1, 6, 30, 0), new DateTime(2021, 3, 5, 19, 25, 0), 37.5).SetName("{m} (Five Days - Weekday Parking)");
                yield return new TestCaseData(new DateTime(2021, 3, 3, 6, 30, 0), new DateTime(2021, 3, 7, 19, 25, 0), 37.5).SetName("{m} (Five Days - Weekday Parking Including Weekends)");
            }
        }

        [TestCaseSource(nameof(GetShortStayTestCases))]
        public void TestShortStayParkingCharge(DateTime parkingDate, DateTime leavingDate, double expectedCharge)
        {
            ShortStayParkingTicket ticket = new ShortStayParkingTicket("Edinburgh", parkingDate);
            Assert.That(ticket.CalculateParkingCharge(leavingDate), Is.EqualTo(expectedCharge));
        }

        [TestCaseSource(nameof(GetLongStayTestCases))]
        public void TestLongStayParkingCharge(DateTime parkingDate, DateTime leavingDate, double expectedCharge)
        {
            LongStayParkingTicket ticket = new LongStayParkingTicket("Edinburgh", parkingDate);
            Assert.That(ticket.CalculateParkingCharge(leavingDate), Is.EqualTo(expectedCharge));
        }
    }
}
