using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace background_worker
{
    public class TableManager
    {
        public string StorageAccount { get; set; }
        public string StorageKey { get; set; }
        public string TableName { get; set; }
        
        public TableManager(string StorageAccount, string StorageKey, string TableName)
        {
            this.StorageAccount = StorageAccount;
            this.StorageKey = StorageKey;
            this.TableName = TableName;
        }

        protected async Task<CloudTable> GetTableAsync()
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                    this.StorageAccount, this.StorageKey
                ), 
                true
            );

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(this.TableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }
    }

    public class IssueTableManager : TableManager
    {
        public IssueTableManager(string StorageAcount, string StorageKey, string TableName) : base(StorageAcount, StorageKey, TableName)
        {

        }

        public async Task<List<IssueEntry>> GetList()
        {
            CloudTable table = await GetTableAsync();
            TableQuery<IssueEntry> query = new TableQuery<IssueEntry>();
            List<IssueEntry> results = new List<IssueEntry>();

            TableContinuationToken? continuationToken = null;

            do
            {
                TableQuerySegment<IssueEntry> queryResults = 
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = queryResults.ContinuationToken;
                results.AddRange(queryResults.Results);
            } while(continuationToken != null);

            return results;
        }
    }

    public class StatisticTableManager : TableManager
    {
        public StatisticTableManager(string StorageAcount, string StorageKey, string TableName) : base(StorageAcount, StorageKey, TableName)
        {

        }

        private async Task Update(StatisticEntry statisticEntry)
        {
            CloudTable table = await GetTableAsync();
            TableOperation operation = TableOperation.InsertOrReplace(statisticEntry);
            await table.ExecuteAsync(operation);
        }

        private async Task ClearTable()
        {
            CloudTable table = await GetTableAsync();      
            TableQuery<StatisticEntry> query = new TableQuery<StatisticEntry>();
            TableContinuationToken token = null;
                                            
            do
            {
                TableQuerySegment<StatisticEntry> result = await table.ExecuteQuerySegmentedAsync(query, token);  

                foreach (StatisticEntry row in result)
                {
                    TableOperation operation = TableOperation.Delete(row);
                    await table.ExecuteAsync(operation);
                }

                token = result.ContinuationToken;
            } while (token != null);  
        }

        public async Task UpdateTable(List<IssueEntry> issueEntriesList)
        {
            List<StatisticEntry> statisticEntriesList = new List<StatisticEntry>();

            await this.ClearTable();

            foreach(var issueEntry in issueEntriesList)
            {
                bool isInList = false;

                foreach(var statisticEntry in statisticEntriesList)
                {
                    if(statisticEntry.SocialSecurityNumber == issueEntry.SocialSecurityNumber)
                    {
                        statisticEntry.ReportedIssues++;
                        isInList = true;
                    }
                }

                if(!isInList)
                {
                    statisticEntriesList.Add(new StatisticEntry(
                        issueEntry.FirstName,
                        issueEntry.Surname,
                        issueEntry.SocialSecurityNumber,
                        DateTime.Now,
                        1
                    ));
                }
            }

            foreach(var statisticEntry in statisticEntriesList)
            {
                await this.Update(statisticEntry);
            }
        }
    }
}

