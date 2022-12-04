using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("issuecontroller")]
public class IssueController : ControllerBase
{
    private readonly ILogger<IssueController> _logger;
    public IssueTableManager issueTableManager = new IssueTableManager(
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
        return "Post Succesful";
    }

    [HttpPut(Name = "PutIssue")]
    public async Task<string> Put(IssueEntry issueEntry)
    {
        await issueTableManager.Update(issueEntry);
        return "Put Succesful";
    }

    [HttpDelete(Name = "DeleteIssue")]
    public async Task<string> Delete(GeneralTableEntry generalTableEntry)
    {
        await issueTableManager.Delete(generalTableEntry.PartitionKey, generalTableEntry.RowKey);
        return "Delete Succesful";
    }
}
