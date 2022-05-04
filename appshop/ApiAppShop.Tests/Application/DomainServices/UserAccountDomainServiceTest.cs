using ApiAppShop.Application.DomainServices;
using ApiAppShop.Domain.Cache;
using FluentAssertions;
using ApiAppShop.Domain.Entities;
using ApiAppShop.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace ApiAppShop.Tests.Application.DomainServices
{
    
    public class UserAccountDomainServiceTest
    {
        private readonly Mock<ICache> _cache = new Mock<ICache>();

        private readonly Mock<IUserAccountRepository> _userAccountRepository = new Mock<IUserAccountRepository>();

        private readonly Mock<ILogger<UserAccountDomainService>> _logger = new Mock<ILogger<UserAccountDomainService>>();

        private readonly UserAccountDomainService _sut;

        public static IEnumerable<object[]> AppsList()
        {
            return new List<object[]>
            {
                new object[] { new List<AppEntity>()  },
                new object[] { new List<AppEntity>() { new AppEntity() { Name="1", Price = 12 } } },
                new object[] { new List<AppEntity>() {
                        new AppEntity() { Name = "2", Price = 4 },
                        new AppEntity() { Name = "3", Price = 0 }
                    },
                }
            };
        }

        public UserAccountDomainServiceTest()
        {
            _sut = new UserAccountDomainService(_userAccountRepository.Object, _cache.Object, _logger.Object);
        }

        [Theory]
        [MemberData(nameof(AppsList))]
        public async Task When_ResultNotFoundInCache_Must_GetFromRepository(List<AppEntity> appList)
        {
            // Arrange
            var userId = "userId";

            var userAccount = new UserAccountEntity()
            {
                Id = "Id",
                UserId = userId,
                Apps = appList
            };

            _cache.Setup(c => c.Get(It.IsAny<string>()))
                .Returns(default(string))
                .Verifiable();

            _userAccountRepository.Setup(r => r.GetAsync(userId))
                .Returns(Task.FromResult(userAccount))
                .Verifiable();

            // Act
            var result = await _sut.GetAsync(userId);

            // Assert
            _cache.Verify(c => c.Get(It.IsAny<string>()), Times.Once);

            _userAccountRepository.Verify(r => r.GetAsync(It.IsAny<string>()), Times.Once);

            result.Id.Should().Be(userAccount.Id);

            result.Apps.Should().HaveCount(userAccount.Apps.Count());

            result.UserId.Should().Be(userAccount.UserId);
        }

        [Theory]
        [MemberData(nameof(AppsList))]
        public async Task When_ResultFoundInCache_Must_NotGetFromRepository(List<AppEntity> appList)
        {
            // Arrange
            var userId = "userId";

            var userAccount = new UserAccountEntity()
            {
                Id = "teste",
                UserId = userId,
                Apps = appList
            };

            _cache.Setup(c => c.Get(It.IsAny<string>()))
                .Returns(JsonSerializer.Serialize(userAccount.Apps))
                .Verifiable();

            _userAccountRepository.Setup(r => r.GetAsync(userId))
                .Returns(Task.FromResult(userAccount))
                .Verifiable();

            // Act
            var result = await _sut.GetAsync(userId);

            // Assert
            _cache.Verify(c => c.Get(It.IsAny<string>()), Times.Once);

            _userAccountRepository.Verify(r => r.GetAsync(It.IsAny<string>()), Times.Never);

            result.Apps.Should().HaveCount(userAccount.Apps.Count());

            result.UserId.Should().Be(userAccount.UserId);
        }
    }
}
