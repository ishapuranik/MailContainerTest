using MailContainerTest.Data;
using MailContainerTest.Types;
using System.Configuration;

namespace MailContainerTest.Services
{
    public class MailTransferService : IMailTransferService
    {
        private IContainerDataStore _dataStore;
        private readonly IMailTypeValidator _mailTypeValidator;

        public MailTransferService(IContainerDataStore dataStore, IMailTypeValidator mailTypeValidator)
        {
            _dataStore = dataStore;
            _mailTypeValidator = mailTypeValidator;
        }

        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {
            MailContainer mailContainer = _dataStore.GetMailContainer(request.SourceMailContainerNumber);

            var result = new MakeMailTransferResult
            {
                Success = _mailTypeValidator.Validate(mailContainer, request)
            };

            if (result.Success)
            {
                mailContainer.Capacity -= request.NumberOfMailItems;

                _dataStore.UpdateMailContainer(mailContainer);
            }

            return result;
        }
    }
}
