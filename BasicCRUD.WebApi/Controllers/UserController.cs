using BasicCRUD.Domain.Models.Users;
using BasicCRUD.Domain.Models.Users.Commands;
using BasicCRUD.Domain.Models.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicCRUD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Creates a New User.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Guid> Create(
            CreateUser command,
            CancellationToken cancellationToken = default)
            => Mediator.Send(command, cancellationToken);
        
        /// <summary>
        /// Gets all Users.
        /// <param name="cancellationToken"></param>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<User>> GetAll(
            CancellationToken cancellationToken = default)
            => Mediator.Send(new ListUsers(), cancellationToken);
        
        /// <summary>
        /// Gets User Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<User> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
            => Mediator.Send(new GetUser(id), cancellationToken);

        /// <summary>
        /// Deletes User Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task Delete(
            Guid id,
            CancellationToken cancellationToken = default)
            => Mediator.Send(new DeleteUser(id), cancellationToken);

        /// <summary>
        /// Updates the User Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public Task<Guid> Update(
            Guid id,
            UpdateUser command,
            CancellationToken cancellationToken = default)
            => Mediator.Send(command.BindId(id), cancellationToken);

        /// <summary>
        /// Updates the User Entity's Password based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}/updatePassword")]
        public Task<string> UpdatePassword(
            Guid id,
            UpdateUserPassword command,
            CancellationToken cancellationToken = default)
            => Mediator.Send(command.BindId(id), cancellationToken);
        
    }
}