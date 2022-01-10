using Project1_App.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project1_App.Tests
{
    public class NewCustomerTest
    {
        [Theory]
        [InlineData(0, "First Name: ")]
        [InlineData(1, "Last Name: ")]
        [InlineData(2, "Phone Number: ")]
        [InlineData(3, "Home Address: ")]
        [InlineData(4, "Username: ")]
        [InlineData(5, "Password: ")]
        public void TestStringForInformation(int num, string print)
        {
            // Arrange
            string getInput = "";

            // Act
            getInput = NewCustomer.getString(num);

            // Assign
            Assert.Equal(print, getInput);
        }
    }
}
