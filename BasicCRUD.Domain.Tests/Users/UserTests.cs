using BasicCRUD.Domain.Models.Users;
using FluentAssertions;
using System;
using Xunit;

namespace BasicCRUD.Domain.Tests.Users
{
    public class UserTests
    {
        private const string EMAIL_ADDRESS = "test@email.com";
        private const string FIRST_NAME = "First";
        private const string LAST_NAME = "Last";

        private static User User => new User(Guid.NewGuid(), EMAIL_ADDRESS, FIRST_NAME, LAST_NAME);

        [Fact]
        public void NotNullEmailNotAllowedWhenCreated()
        {
            Assert.Throws<NullReferenceException>(
                () => new User(Guid.NewGuid(), null, FIRST_NAME, LAST_NAME));
        }

        [Fact]
        public void NotNullFirstNameNotAllowedWhenCreated()
        {
            Assert.Throws<NullReferenceException>(
                () => new User(Guid.NewGuid(), EMAIL_ADDRESS, null, LAST_NAME));
        }

        [Fact]
        public void NotNullLastNameNotAllowedWhenCreated()
        {
            Assert.Throws<NullReferenceException>(
                () => new User(Guid.NewGuid(), EMAIL_ADDRESS, FIRST_NAME, null));
        }

        [Fact]
        public void NotNullEmailNotAllowedWhenUpdated()
        {
            Assert.Throws<NullReferenceException>(
                () => User.SetEmail(null));
        }

        [Fact]
        public void NotNullFirstNameNotAllowedWhenUpdated()
        {
            Assert.Throws<NullReferenceException>(
                () => User.SetFirstName(null));
        }

        [Fact]
        public void NotNullLastNameNotAllowedWhenUpdated()
        {
            Assert.Throws<NullReferenceException>(
                () => User.SetLastName(null));
        }

        [Fact]
        public void CreateUser()
        {
            var user = User;

            user.Email.Should().Be("test@email.com");
            user.FirstName.Should().Be("First");
            user.LastName.Should().Be("Last");
        }

        [Fact]
        public void UpdateUser()
        {
            var user = User;

            user.SetEmail("new@email.com");
            user.SetFirstName("newFirst");
            user.SetLastName("newLast");

            user.Email.Should().Be("new@email.com");
            user.FirstName.Should().Be("newFirst");
            user.LastName.Should().Be("newLast");
        }
    }
}
