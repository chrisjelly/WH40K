using JellyDev.WH40K.Domain.Faction;
using JellyDev.WH40K.Domain.SharedKernel.Interfaces;
using JellyDev.WH40K.Domain.SharedKernel.ValueObjects;
using JellyDev.WH40K.Domain.Keyword;
using JellyDev.WH40K.Domain.Keyword.ParameterObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JellyDev.WH40K.Domain.Tests.Keyword
{
    public class KeywordAggregateTests
    {
        [Fact]
        public void Keyword_cannot_be_created_with_null_values()
        {
            // Arrange
            var id = new KeywordId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Keyword");
            var createKeywordParams = new CreateKeywordParams(id, factionId, name);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new KeywordAggregate(null, null));
            Assert.Throws<ArgumentNullException>(() => new KeywordAggregate(createKeywordParams, null));
        }

        [Fact]
        public void Keyword_cannot_be_created_with_non_existent_faction()
        {
            // Arrange
            var id = new KeywordId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Keyword");
            var createKeywordParams = new CreateKeywordParams(id, factionId, name);

            var repositoryChecker = new Mock<IRepositoryChecker<FactionId>>();
            repositoryChecker.Setup(x => x.Exists(new FactionId(createKeywordParams.FactionId)))
                .Returns(false);

            // Act & Assert
            Assert.Throws<Exception>(() => new KeywordAggregate(createKeywordParams, repositoryChecker.Object));
        }

        [Fact]
        public void Keyword_can_be_created()
        {
            // Arrange
            var id = new KeywordId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Keyword");
            var createKeywordParams = new CreateKeywordParams(id, factionId, name);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createKeywordParams.FactionId)))
                .Returns(true);

            // Act
            var keyword = new KeywordAggregate(createKeywordParams, factionChecker.Object);

            // Assert
            Assert.Equal(id, keyword.Id);
            Assert.Equal(factionId, keyword.FactionId);
            Assert.Equal(name, keyword.Name);
        }

        [Fact]
        public void Keyword_cannot_be_updated_with_null_values()
        {
            // Arrange
            var id = new KeywordId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Keyword");
            var createKeywordParams = new CreateKeywordParams(id, factionId, name);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createKeywordParams.FactionId)))
                .Returns(true);

            var keyword = new KeywordAggregate(createKeywordParams, factionChecker.Object);

            var updateKeywordParams = new UpdateKeywordParams(factionId, name);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => keyword.Update(null, null));
            Assert.Throws<ArgumentNullException>(() => keyword.Update(updateKeywordParams, null));
        }

        [Fact]
        public void Keyword_cannot_be_updated_with_non_existent_faction()
        {
            // Arrange
            var id = new KeywordId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Keyword");
            var createKeywordParams = new CreateKeywordParams(id, factionId, name);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createKeywordParams.FactionId)))
                .Returns(true);

            var keyword = new KeywordAggregate(createKeywordParams, factionChecker.Object);

            var newFactionid = new FactionId(Guid.NewGuid());
            var updateKeywordParams = new UpdateKeywordParams(newFactionid, name);

            factionChecker.Setup(x => x.Exists(new FactionId(updateKeywordParams.FactionId)))
                .Returns(false);

            // Act & Assert
            Assert.Throws<Exception>(() => keyword.Update(updateKeywordParams, factionChecker.Object));
        }

        [Fact]
        public void Keyword_can_be_updated()
        {
            // Arrange
            var id = new KeywordId(Guid.NewGuid());
            var factionId = new FactionId(Guid.NewGuid());
            var name = Name.FromString("Test Keyword");
            var createKeywordParams = new CreateKeywordParams(id, factionId, name);

            var factionChecker = new Mock<IRepositoryChecker<FactionId>>();
            factionChecker.Setup(x => x.Exists(new FactionId(createKeywordParams.FactionId)))
                .Returns(true);

            var keyword = new KeywordAggregate(createKeywordParams, factionChecker.Object);

            var newFactionid = new FactionId(Guid.NewGuid());
            var newName = Name.FromString("Updated");
            var updateKeywordParams = new UpdateKeywordParams(newFactionid, newName);

            factionChecker.Setup(x => x.Exists(new FactionId(updateKeywordParams.FactionId)))
                .Returns(true);

            // Act
            keyword.Update(updateKeywordParams, factionChecker.Object);

            // Assert
            Assert.Equal(newFactionid, keyword.FactionId);
            Assert.Equal(newName, keyword.Name);
        }
    }
}
