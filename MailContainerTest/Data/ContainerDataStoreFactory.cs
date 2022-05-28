namespace MailContainerTest.Data
{
    public static class ContainerDataStoreFactory
    {
        public static IContainerDataStore CreateContainerDataStore(string? dataStoreType)
        {
            switch (dataStoreType)
            {
                case "Backup":
                    return new BackupMailContainerDataStore();
                default:
                    return new MailContainerDataStore();
            }
        }
    }
}
