using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Missing()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act - test
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert - wynik
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_LastName_Is_Missing()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act - test
        var addResult = userService.AddUser("John", "", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert - wynik
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Not_Valid()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act - test
        var addResult = userService.AddUser("John", "", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
        //Assert - wynik
        Assert.False(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_User_Age_Less_Then_Allowed()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act - test
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2005-04-21"), 1);
        //Assert - wynik
        Assert.False(addResult);
        
    }
    
    [Fact]
    public void AddUser_Should_Throw_Argument_Exception_When_Client_Doesnt_Exit()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 0);
        });
    }
    
    [Fact]
    public void AddUser_Should_Throw_Argument_Exception_When_Client_LastName_Incorrect()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Alonso", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 3);
        });
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_User_Has_Credit_Limit_Less_Than_Five_Hundred()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act - test
        var addResult = userService.AddUser("John", "Kowalski", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert - wynik
        Assert.False(addResult);
        
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_All_User_Data_Is_Correct()
    {
        //Arrange - zaleznosz do testu
        var userService = new UserService();
        //Act - test
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);        //Assert - wynik
        Assert.True(addResult);
        
    }
    
}