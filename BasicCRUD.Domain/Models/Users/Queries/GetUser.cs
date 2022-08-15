using EnsureThat;
using MediatR;
using System;

namespace BasicCRUD.Domain.Models.Users.Queries
{
    public class GetUser : IRequest<User>
    {
        public GetUser(Guid id)
        {
            EnsureArg.IsNotDefault(id, nameof(id));

            Id = id;
        }

        public Guid Id { get; set; }
    }
}
