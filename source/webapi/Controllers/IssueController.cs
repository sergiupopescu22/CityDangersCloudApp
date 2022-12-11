using Microsoft.AspNetCore.Mvc;
using Azure.Messaging.ServiceBus;
using Azure.Identity;

namespace webapi.Controllers;

[ApiController]
[Route("issuecontroller")]
public class IssueController : ControllerBase
{
    private readonly ILogger<IssueController> _logger;
    private IssueTableManager issueTableManager = new IssueTableManager(
        "datcpontaciprian",
        "0r3bz8HHcCROB056ceMNhZfeXhAdycvogaf8pjT4cz7c/tQYp7Ov4IBRCXI2zuFHMIK4cimFgD/a+AStMpDS9Q==",
        "datcProjectIssueTable"
    );

    public IssueController(ILogger<IssueController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetIssue")]
    public async Task<IEnumerable<IssueEntry>> Get()
    {
        return await issueTableManager.GetList();
    }

    [HttpPost(Name = "PostIssue")]
    public async Task<string> Post(IssueEntry issueEntry)
    {
        await issueTableManager.Insert(issueEntry);
        await SendMessageToBackgroundWorker("POST");
        return "Post Succesful";
    }

    [HttpPut(Name = "PutIssue")]
    public async Task<string> Put(IssueEntry issueEntry)
    {
        await issueTableManager.Update(issueEntry);
        await SendMessageToBackgroundWorker("PUT");
        return "Put Succesful";
    }

    [HttpDelete(Name = "DeleteIssue")]
    public async Task<string> Delete(GeneralTableEntry generalTableEntry)
    {
        await issueTableManager.Delete(generalTableEntry.PartitionKey, generalTableEntry.RowKey);
        await SendMessageToBackgroundWorker("DELETE");
        return "Delete Succesful";
    }

    private async Task SendMessageToBackgroundWorker(string message)
    {
        ServiceBusClientOptions clientOptions = new ServiceBusClientOptions() 
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        
        ServiceBusClient client = new ServiceBusClient("Endpoint=sb://datcprojectbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=et2n+UVbrvU2gxRLOjnOPx8l4pmnN381wdlyeWjtFtw=", clientOptions);
        ServiceBusSender sender = client.CreateSender("datcprojectqueue");

        try
        {
            await sender.SendMessageAsync(new ServiceBusMessage(message));
            Console.WriteLine(string.Format("Message '{0}' sent", message));
        }
        finally
        {
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }
    }
}
