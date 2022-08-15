using BasicCRUD.Domain.Context;
using BasicCRUD.Domain.Models.Users;
using BasicCRUD.Domain.Models.Users.Queries;
using EnsureThat;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCRUD.Application.QueryHandlers.Users
{
    internal sealed class ListUsersHandler : IRequestHandler<ListUsers, IEnumerable<User>>
    {
        private readonly IApplicationContext _context;

        public ListUsersHandler(
            IApplicationContext context)
        {
            EnsureArg.IsNotNull(context, nameof(context));

            _context = context;
        }

        public async Task<IEnumerable<User>> Handle(ListUsers query, CancellationToken cancellationToken)
        {
            EnsureArg.IsNotNull(query, nameof(query));

            var userList = await _context.Users.ToListAsync();

            return userList.AsReadOnly();
        }
    }
}
