using Ewenze.Domain.Entities;
using MediatR;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUsers
{
    public record class GetUsersQuery : IRequest<IEnumerable<User>>;
}
