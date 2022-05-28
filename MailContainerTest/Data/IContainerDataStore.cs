using MailContainerTest.Types;

namespace MailContainerTest.Data
{
    public interface IContainerDataStore
    {
        MailContainer GetMailContainer(string mailContainerNumber);
        void UpdateMailContainer(MailContainer mailContainer);
    }
}
