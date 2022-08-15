using EnsureThat;
using System;
using System.ComponentModel.DataAnnotations;

namespace BasicCRUD.Domain.Models.Users
{
    public class User
    {
        private User() { }

        public User(
            Guid loginId,
            string email,
            string firstName,
            string lastName)
        {
            EnsureArg.IsNotDefault(loginId, nameof(loginId));
            LoginId = loginId;

            SetEmail(email);
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        [Key]
        public Guid LoginId { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public void SetEmail(string email)
        {
            EnsureArg.IsNotEmptyOrWhiteSpace(email, nameof(email));
            Email = email.Trim();
        }

        public void SetLastName(string lastName)
        {
            EnsureArg.IsNotEmptyOrWhiteSpace(lastName, nameof(lastName));
            LastName = lastName.Trim();
        }

        public void SetFirstName(string firstName)
        {
            EnsureArg.IsNotEmptyOrWhiteSpace(firstName, nameof(firstName));
            FirstName = firstName.Trim();
        }
    }
}
