using EnsureThat;
using MediatR;
using System;

namespace BasicCRUD.Domain.Models.Users.Commands
{
    public sealed class DeleteUser : IRequest
    {
        public DeleteUser(Guid id)
        {
            EnsureArg.IsNotDefault(id, nameof(id));

            Id = id;
        }

        public Guid Id { get; set; }
    }
}
