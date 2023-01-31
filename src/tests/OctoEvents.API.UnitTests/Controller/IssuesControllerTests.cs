using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OctoEvents.API.Controllers;
using OctoEvents.CrossCutting.Interfaces.Mediatr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OctoEvents.API.UnitTests.Controller
{
    public class IssuesControllerTests
    {
        private readonly Mock<IMediatrHandler> _mediatrHandlerMock = new Mock<IMediatrHandler>();
        private readonly Mock<ILogger<IssuesController>> _loggerMock = new Mock<ILogger<IssuesController>>();
        private readonly IssuesController _controller;

        public IssuesControllerTests()
        {
            _controller = new IssuesController(_mediatrHandlerMock.Object, _loggerMock.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        [InlineData(-3000)]
        public async Task ItShouldNotProcessIssueQueryWhenIssueIdParameterIsInvalid(long issueId)
        {
            var response = await _controller.GetIssueEvents(issueId);

            _loggerMock.Verify(x => x.LogInformation("Issue ID cannot be less than or equal to 0."), Times.Once());
            _controller.HttpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
