using Project1_App.App;
using Project1_App.App.RequestHttp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Project1_App.Tests
{
    public class LoginTest
    {
        Uri myServer = new(File.ReadAllText("C:/Users/rootb/Revature/Database_File/LocalHostP1.txt"));

        // Skip "2" due to call other method with console.Readline()
        [Theory]
        [InlineData("1")]
        [InlineData("3")]
        public async Task CheckInputValidReturnFalse(string input)
        {
            // Arrange
            Program program = new Program();

            // Act
            bool result = await Login.GetUserInput(program, input);

            // Assign
            Assert.False(result);
        }

        [Theory]
        [InlineData("4")]
        [InlineData("Eddie")]
        public async Task CheckInputValidReturnTrue(string input)
        {
            // Arrange
            Program program = new Program();

            // Act
            bool result = await Login.GetUserInput(program, input);

            // Assign
            Assert.True(result);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CheckProgramMode(bool IsManager)
        {
            // Arrange 
            Program program = new();

            // Act
            Program.Mode mode = Login.ChangeMode(program, IsManager);

            // Assign
            if (IsManager)
                Assert.Equal(Program.Mode.ManagerRequest, mode);
            else
                Assert.Equal(Program.Mode.CustomerRequest, mode);
        }


        // If result is null, then false, or not null, then true
        [Theory]
        [InlineData("2", "true")]
        [InlineData("1", "False")]
        [InlineData(null, null)]
        public void ResultNullOrNotNull(string? input1, string? input2)
        {
            // Arrange
            Program program = new();
            List<string> inputs = new();
            bool temp = false;
            if(input1 != null && input2 != null)
                inputs = new() { input1!, input2! };

            // Act
            bool matching = Login.GetMatching(program, inputs, ref temp);

            // Assign
            if(inputs == null || inputs.Count == 0)
                Assert.False(matching);
            else
                Assert.True(matching);
        }
    }
}