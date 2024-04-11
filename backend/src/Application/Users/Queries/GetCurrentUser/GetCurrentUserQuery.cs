using ApplicationHandler.Application.Users.DTO;
using MediatR;

namespace ApplicationHandler.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserDTO> { }
