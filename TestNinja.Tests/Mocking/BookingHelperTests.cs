using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.Tests.Mocking
{
    public class BookingHelperTests
    {
        private Mock<IBookingRepository> _repository;

        public BookingHelperTests()
        {
            _repository = new Mock<IBookingRepository>();
            _repository.Setup(repo => repo.GetActiveBookings(1)).Returns(new List<Booking>()
            {
                new Booking(){
                    Id = 2,
                    ArrivalDate = new DateTime(2021, 12, 12, 10, 0, 0),
                    DepartureDate = new DateTime(2022, 1, 12, 14, 0, 0),
                    Reference = "a",
                },                
                new Booking(){
                    Id = 3,
                    ArrivalDate = new DateTime(2022, 7, 1, 10, 0, 0),
                    DepartureDate = new DateTime(2022, 7, 20, 14, 0, 0),
                    Reference = "b"
                }
            }.AsQueryable());
        }

        [Fact]
        public void OverlappingBookingExists_ReturnsEmpty_WhenNoOverlap()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = new DateTime(2021, 10, 10, 10, 0, 0),
                DepartureDate = new DateTime(2021, 10, 13, 14, 0, 0),
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.Equal(String.Empty, result);
        }

        [Fact]
        public void OverlappingBookingExists_ReturnsReferenceString_WhenOverlapExists()
        {
            var booking = new Booking()
            {
                Id= 1,
                ArrivalDate = new DateTime(2021, 12, 31, 10, 0, 0),
                DepartureDate = new DateTime(2022, 1, 31, 14, 0, 0),
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.Equal("a", result);
        }

        [Fact]
        public void OverlappingBookingExists_ReturnsEmptyString_WhenCancelledOverlapExists()
        {
            var booking = new Booking()
            {
                Id = 1,
                ArrivalDate = new DateTime(2021, 6, 29, 10, 0, 0),
                DepartureDate = new DateTime(2022, 7, 14, 14, 0, 0),
                Status = "Cancelled"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);

            Assert.Equal(String.Empty, result);
        }
    }
}
