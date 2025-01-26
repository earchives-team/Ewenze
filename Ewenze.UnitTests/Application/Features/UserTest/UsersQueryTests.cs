using AutoMapper;
using Ewenze.Application.Features.UserFeature.Dto;
using Ewenze.Application.Features.UserFeature.Queries.GetUserByEmail;
using Ewenze.Application.Features.UserFeature.Queries.GetUserById;
using Ewenze.Domain.Entities;
using Ewenze.Domain.Exceptions;
using Ewenze.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.UnitTests.Application.Features.UserTest
{

    public class GetUserByEmailQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetUserByEmailQueryHandler _handler;

        public GetUserByEmailQueryHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetUserByEmailQueryHandler(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_UserExists_ReturnsUserDto()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Email = email };
            var userDto = new UserDto { Email = email };

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<UserDto>(user))
                .Returns(userDto);

            var query = new GetUserByEmailQuery(email);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);

            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(user), Times.Once);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var email = "nonexistent@example.com";

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(email))
                .ReturnsAsync((User)null);

            var query = new GetUserByEmailQuery(email);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));

            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAsync(email), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<UserDto>(It.IsAny<User>()), Times.Never);
        }
    }
}
