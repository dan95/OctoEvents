using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OctoEvents.CrossCutting.Interfaces.Mediatr;
using OctoEvents.Domain.Entities;
using OctoEvents.Domain.Operations.Commands;
using OctoEvents.Domain.Operations.Queries;
using OctoEvents.Domain.ViewModel;
using System.Net;

namespace OctoEvents.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IssuesController : ControllerBase
    {
        private readonly IMediatrHandler _mediatrHandler;
        private readonly ILogger<IssuesController> _logger;

        public IssuesController(
            IMediatrHandler mediatrHandler,
            ILogger<IssuesController> logger
            )
        {
            _mediatrHandler = mediatrHandler;
            _logger = logger;
        }

        [HttpPost("events")]
        public async Task<IActionResult> ReceiveIssue([FromBody]EventViewModel data)
        {
            try
            {
                _logger.LogInformation("Event received. Starting process.");

                var response = await _mediatrHandler.SendCommandAsync<SaveIssueInteractionCommand, ValidationResult>(new SaveIssueInteractionCommand(data));

                if (response.IsValid)
                {
                    _logger.LogInformation("Event successfully saved. Returning response.");

                    return Ok();
                }

                _logger.LogWarning("Creation of event was not successful.");

                return BadRequest(response);
            }
            catch(Exception ex)
            {
                var message = $"An error occurred while trying to process event creation.";
                _logger.LogError(ex, message);

                return StatusCode((int)HttpStatusCode.NoContent, message);
            }
        }

        [HttpGet("{issueId:long}/events")]
        public async Task<IActionResult> GetIssueEvents([FromRoute]long issueId)
        {
            try
            {
                _logger.LogInformation($"Processing query. Issue ID: {issueId}");

                if(issueId <= 0)
                {
                    var message = "Issue ID cannot be less than or equal to 0.";

                    _logger.LogWarning(message);

                    return BadRequest(message);
                }

                var response = await _mediatrHandler.SendCommandAsync<IssueEventSearchQuery, List<IssueEventItemViewModel>>(new IssueEventSearchQuery(issueId));

                _logger.LogInformation("Query processed. Returning to caller.");

                return Ok(response);
            }
            catch(Exception ex)
            {
                var message = $"An error occurred while trying to query issue {issueId}";
                _logger.LogError(ex, message);

                return StatusCode((int)HttpStatusCode.NoContent, message);
            }
        }
    }
}
