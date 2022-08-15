using MediatR;
using System;

namespace BasicCRUD.Domain.Models.Users.Commands
{
    public sealed class CreateUser : IRequest<Guid>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
