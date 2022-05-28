namespace MailContainerTest.Types
{
    public interface IMailTypeValidator
    {
        bool Validate(MailContainer mailContainer, MakeMailTransferRequest request);
    }
}