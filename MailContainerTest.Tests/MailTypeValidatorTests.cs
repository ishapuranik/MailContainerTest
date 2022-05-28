using MailContainerTest.Types;
using Xunit;

namespace MailContainerTest.Tests
{
    public class MailTypeValidatorTests
    {
        private MailTypeValidator _validator;

        [Theory]
        [InlineData(MailType.LargeLetter, AllowedMailType.LargeLetter, 10, 15, true)]
        [InlineData(MailType.LargeLetter, AllowedMailType.LargeLetter, 15, 20, true)]
        [InlineData(MailType.LargeLetter, AllowedMailType.SmallParcel, 25, 50, false)]
        [InlineData(MailType.LargeLetter, AllowedMailType.StandardLetter, 30, 50, false)]
        [InlineData(MailType.LargeLetter, AllowedMailType.LargeLetter, 60, 30, false)]
        public void LargeLetterValidations(MailType mailType, AllowedMailType allowedMailType, int numberOfMailItems, int capacity, bool expectedResult)
        {
            _validator = new MailTypeValidator();

            var largeLetterRequest = new MakeMailTransferRequest
            {
                MailType = mailType,
                NumberOfMailItems = numberOfMailItems
            };

            var mailContainer = new MailContainer()
            {
                AllowedMailType = allowedMailType,
                Capacity = capacity
            };

            expectedResult.Equals(_validator.Validate(mailContainer, largeLetterRequest));
        }

    }
}
