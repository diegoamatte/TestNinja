using System;
using System.Collections.Generic;
using TestNinja.Fundamentals;
using Xunit;


namespace TestNinja.Tests.Fundamentals
{
    public class ReservationTests
    {
        [Fact]
        public void CanBeCancelledBy_ReturnsTrueIfMadeByAdmin()
        {
            var user = new User() { IsAdmin = true };
            var reservation = new Reservation();

            Assert.True(reservation.CanBeCancelledBy(user));
        }
        
        [Fact]
        public void CanBeCancelledBy_ReturnsTrueIfMadeByOwner()
        {
            var user = new User();
            var reservation = new Reservation() { MadeBy = user };

            Assert.True(reservation.CanBeCancelledBy(user));
        }
    }
}
