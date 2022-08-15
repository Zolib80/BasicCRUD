using BasicCRUD.Domain.Models.Users;
using MediatR;
using System.Collections.Generic;

namespace BasicCRUD.Domain.Models.Users.Queries
{
    public class ListUsers : IRequest<IEnumerable<User>>
    {
    }
}
