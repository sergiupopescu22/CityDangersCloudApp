using Microsoft.WindowsAzure.Storage.Table;

namespace background_worker
{
    public class GeneralTableEntry
    {
        public string PartitionKey {get; set;}
        public string RowKey {get; set;}

        public GeneralTableEntry(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }
    }

    public class IssueEntry : TableEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string SocialSecurityNumber { get; set; }
        public DateTime ReportingTime { get; set; }
        public string Issue { get; set; }

        public IssueEntry()
        {
            this.PartitionKey = "";
            this.RowKey = "";
            this.FirstName = "";
            this.Surname = "";
            this.SocialSecurityNumber = "";
            this.ReportingTime = DateTime.Now;
            this.Issue = "";
        }

        public IssueEntry(string FirstName, string Surname, string SocialSecurityNumber, DateTime ReportingTime, string Issue)
        {
            this.PartitionKey = SocialSecurityNumber;
            this.RowKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.SocialSecurityNumber = SocialSecurityNumber;
            this.ReportingTime = ReportingTime;
            this.Issue = Issue;
        }

        public IssueEntry(IssueEntry issueEntry)
        {
            this.PartitionKey = issueEntry.SocialSecurityNumber;
            this.RowKey = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            this.FirstName = issueEntry.FirstName;
            this.Surname = issueEntry.Surname;
            this.SocialSecurityNumber = issueEntry.SocialSecurityNumber;
            this.ReportingTime = issueEntry.ReportingTime;
            this.Issue = issueEntry.Issue;
        }
    }

    public class StatisticEntry : TableEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string SocialSecurityNumber { get; set; }
        public DateTime StatisticTime { get; set; }
        public int ReportedIssues { get; set; }

        public StatisticEntry()
        {
            this.PartitionKey = "";
            this.RowKey = "";
            this.FirstName = "";
            this.Surname = "";
            this.SocialSecurityNumber = "";
            this.StatisticTime = DateTime.Now;
            this.ReportedIssues = 0;
        }

        public StatisticEntry(string FirstName, string Surname, string SocialSecurityNumber, DateTime StatisticTime, int ReportedIssues)
        {
            this.PartitionKey = SocialSecurityNumber;
            this.RowKey = "";
            this.FirstName = FirstName;
            this.Surname = Surname;
            this.SocialSecurityNumber = SocialSecurityNumber;
            this.StatisticTime = StatisticTime;
            this.ReportedIssues = ReportedIssues;
        }
    }
}

