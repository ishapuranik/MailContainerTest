using FluentAssertions;
using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using Moq;
using Xunit;

namespace MailContainerTest.Tests
{
    public class MailTransferServiceTests
    {
        private Mock<IContainerDataStore> _mockDataStore;
        private Mock<IMailTypeValidator> _mockMailTypeValidator;
        private IMailTransferService _mockService;

        public void Init()
        {
            _mockDataStore = new Mock<IContainerDataStore>();
            _mockMailTypeValidator = new Mock<IMailTypeValidator>();
            _mockService = new MailTransferService(_mockDataStore.Object, _mockMailTypeValidator.Object);
        }

        [Fact]
        public void MakeMailTransfer_calls_UpdatesMailContainer_And_Returns_Success_If_Validation_Passes()
        {
            Init();

            var sourceMailContainerNumber = "Test123";
            var request = new MakeMailTransferRequest()
            {
                SourceMailContainerNumber = sourceMailContainerNumber,
                NumberOfMailItems = 45
            };

            var container = new MailContainer
            {
                Capacity = 60
            };

            _mockDataStore.Setup(x => x.GetMailContainer(sourceMailContainerNumber)).Returns(container);
            _mockMailTypeValidator.Setup(x => x.Validate(container, request)).Returns(true);

            var result = _mockService.MakeMailTransfer(request);

            result.Success.Should().BeTrue();
            container.Capacity.Should().Be(15);
            _mockDataStore.Verify(s => s.UpdateMailContainer(container), Times.Exactly(1));
        }

        [Fact]
        public void MakeMailTransfer_does_not_calls_UpdateMailContainer_and_Returns_false_If_Validation_Fails()
        {
            Init();

            var sourceMailContainerNumber = "Test123";
            var request = new MakeMailTransferRequest()
            {
                SourceMailContainerNumber = sourceMailContainerNumber,
                NumberOfMailItems = 55
            };

            var container = new MailContainer
            {
                Capacity = 555
            };

            _mockDataStore.Setup(x => x.GetMailContainer(sourceMailContainerNumber)).Returns(container);
            _mockMailTypeValidator.Setup(x => x.Validate(container, request)).Returns(false);

            var result = _mockService.MakeMailTransfer(request);

            result.Success.Should().BeFalse();
            container.Capacity.Should().Be(555);
            _mockDataStore.Verify(s => s.UpdateMailContainer(container), Times.Never);
        }
    }
}
