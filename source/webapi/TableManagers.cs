using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace webapi
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

        public async Task<IssueEntry> GetItem(string partitionKey, string rowKey)
        {
            CloudTable table = await GetTableAsync();
            TableOperation operation = TableOperation.Retrieve<IssueEntry>(partitionKey, rowKey);
            TableResult result = await table.ExecuteAsync(operation);
            return (IssueEntry)(dynamic)result.Result;
        }

        public async Task Insert(IssueEntry issueEntry)
        {
            IssueEntry normalizedIssueEntry = new IssueEntry(issueEntry);
            CloudTable table = await GetTableAsync();
            TableOperation operation = TableOperation.Insert(normalizedIssueEntry);
            await table.ExecuteAsync(operation);
        }

        public async Task Update(IssueEntry issueEntry)
        {
            CloudTable table = await GetTableAsync();
            TableOperation operation = TableOperation.InsertOrReplace(issueEntry);
            await table.ExecuteAsync(operation);
        }

        public async Task Delete(string partitionKey, string rowKey)
        {
            CloudTable table = await GetTableAsync();  
            IssueEntry issueEntry = await GetItem(partitionKey, rowKey);  
            TableOperation operation = TableOperation.Delete(issueEntry);  
            await table.ExecuteAsync(operation); 
        }
    }
}

