using EnsureThat;
using MediatR;
using System;

namespace BasicCRUD.Domain.Models.Users.Commands
{
    public sealed class UpdateUser : IRequest<Guid>
    {
        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public UpdateUser BindId(Guid id)
        {
            EnsureArg.IsNotDefault(id, nameof(id));
            Id = id;
            return this;
        }
    }
}
