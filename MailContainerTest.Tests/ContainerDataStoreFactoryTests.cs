using FluentAssertions;
using MailContainerTest.Data;
using Xunit;

namespace MailContainerTest.Tests
{
    public class ContainerDataStoreFactoryTests
    {
        [Fact]
        public void CreatesBackupDataStoreIfDataStoreTypeIsBackup()
        {
            //Act
            var dataStore = ContainerDataStoreFactory.CreateContainerDataStore("Backup");

            //Assert
            dataStore.Should().NotBeNull();
            dataStore.Should().BeOfType<BackupMailContainerDataStore>();
        }

        [Theory]
        [InlineData("Test")]
        [InlineData("123")]
        [InlineData(null)]
        public void CreatesMailDataStore_If_DataStoreType_Is_Other_Than_Backup(string dataStoreType)
        {
            //Act
            var dataStore = ContainerDataStoreFactory.CreateContainerDataStore(dataStoreType);

            //Assert
            dataStore.Should().NotBeNull();
            dataStore.Should().BeOfType<MailContainerDataStore>();
        }
    }
}
