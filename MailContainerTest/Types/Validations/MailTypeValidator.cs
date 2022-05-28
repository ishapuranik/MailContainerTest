using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailContainerTest.Types
{

    public class MailTypeValidator : IMailTypeValidator
    {
        public bool Validate(MailContainer mailContainer, MakeMailTransferRequest request)
        {
            switch (request.MailType)
            {
                case MailType.StandardLetter:
                    return ValidateStandardLetter(mailContainer);

                case MailType.LargeLetter:
                    return ValidateLargeLetter(mailContainer, request.NumberOfMailItems);

                case MailType.SmallParcel:
                    return ValidateSmallParcel(mailContainer);
            }

            return true;
        }

        private static bool ValidateStandardLetter(MailContainer mailContainer)
        {
            return mailContainer.AllowedMailType.HasFlag(AllowedMailType.StandardLetter);
        }

        private static bool ValidateLargeLetter(MailContainer mailContainer, int numberOfMailItems)
        {
            if (!mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter))
            {
                return false;
            }

            return mailContainer.Capacity >= numberOfMailItems;
        }
        private static bool ValidateSmallParcel(MailContainer mailContainer)
        {
            if (!mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel))
            {
                return false;
            }

            return mailContainer.Status == MailContainerStatus.Operational;
        }
    }
}
