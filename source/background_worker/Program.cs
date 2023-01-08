using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Identity;

namespace background_worker
{
    class Program
    {
        static IssueTableManager issueTableManager = new IssueTableManager(
        "citydangersapp",
        "FXCo0fgVH688Ju69OHk0ghsEQ58cWMOyqYVYeEcxd3rcAZJjp09JYJVWJ0R3brOcorrOGQvRpfUz+AStYiA/Dw==",
        "ReportedIssues"
        );

        static StatisticTableManager statisticTableManager = new StatisticTableManager(
            "datcpontaciprian",
            "0r3bz8HHcCROB056ceMNhZfeXhAdycvogaf8pjT4cz7c/tQYp7Ov4IBRCXI2zuFHMIK4cimFgD/a+AStMpDS9Q==",
            "datcProjectStatisticTable"
        );

        static async Task Main(string[] args)
        {
            ServiceBusClient client;
            ServiceBusProcessor processor;

            ServiceBusClientOptions clientOptions = new ServiceBusClientOptions() 
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            client = new ServiceBusClient("Endpoint=sb://datcprojectbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=et2n+UVbrvU2gxRLOjnOPx8l4pmnN381wdlyeWjtFtw=", clientOptions);
            processor = client.CreateProcessor("datcprojectqueue", new ServiceBusProcessorOptions());

            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;
                await processor.StartProcessingAsync();
                while(true);
                await processor.StopProcessingAsync();
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string MessageBody = args.Message.Body.ToString();
            Console.WriteLine("Message received: " + MessageBody);

            if(MessageBody.ToUpper().Contains("POST") ||
               MessageBody.ToUpper().Contains("PUT") ||
               MessageBody.ToUpper().Contains("DELETE"))
            {
                await statisticTableManager.UpdateTable(await issueTableManager.GetList());
            }

            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}




