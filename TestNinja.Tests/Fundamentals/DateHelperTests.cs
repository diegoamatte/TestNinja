using System;
using System.Collections.Generic;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.Tests.Fundamentals
{
    public class DateHelperTests
    {
        public static IEnumerable<Object[]> Days { get
            {
                yield return new Object[] { new DateTime(2022, 1, 16), new DateTime(2022, 2, 1) };
                yield return new Object[] { new DateTime(2022, 12, 15), new DateTime(2023, 1, 1) };
            }
        }

        [Theory]
        [MemberData(nameof(Days))]
        public void FirstOfNextMonth_ReturnsCorrectDate(DateTime date, DateTime expectedResult)
        {
            var result = DateHelper.FirstOfNextMonth(date);

            Assert.Equal(expectedResult, result);
        }
    }

}
