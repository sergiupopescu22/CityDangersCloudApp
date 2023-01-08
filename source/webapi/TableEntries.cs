using Microsoft.WindowsAzure.Storage.Table;

namespace webapi
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
        public double Latitudine { get; set; }
        public double Longitudine { get; set; }
        public string Issue { get; set; }

        public IssueEntry()
        {
            this.PartitionKey = "";
            this.RowKey = "";
            this.Latitudine = 0;
            this.Longitudine = 0;
            this.Issue = "";
        }

        public IssueEntry(string CNP, string Data, double Latitudine, double Longitudine, string Issue)
        {
            this.PartitionKey = CNP;
            this.RowKey = Data;
            this.Latitudine = Latitudine;
            this.Longitudine = Longitudine;
            this.Issue = Issue;
        }

        public IssueEntry(IssueEntry issueEntry)
        {
            this.PartitionKey = issueEntry.PartitionKey;
            this.RowKey = issueEntry.RowKey;
            this.Latitudine = issueEntry.Latitudine;
            this.Longitudine = issueEntry.Longitudine;
            this.Issue = issueEntry.Issue;
        }
    }
}

