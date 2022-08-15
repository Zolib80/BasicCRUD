using EnsureThat;
using MediatR;
using System;

namespace BasicCRUD.Domain.Models.Users.Commands
{
    public sealed class UpdateUserPassword : IRequest<string>
    {
        public Guid Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public UpdateUserPassword BindId(Guid id)
        {
            EnsureArg.IsNotDefault(id, nameof(id));
            Id = id;
            return this;
        }
    }
}
