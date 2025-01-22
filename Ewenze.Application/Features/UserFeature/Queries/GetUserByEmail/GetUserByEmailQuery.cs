using Ewenze.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Features.UserFeature.Queries.GetUserByEmail
{
    public record class GetUserByEmailQuery(string email) : IRequest<User>;
}
