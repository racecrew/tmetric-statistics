using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tmetricstatistics.Model;
using tmetricstatistics.Data;
using tmetricstatistics.Services;
using Microsoft.Extensions.Configuration;

namespace tmetricstatistics.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class TMetricRawDataController : ControllerBase
    {
        private readonly ITMetricRawDataServices rawDataServices;
        private readonly IConfiguration configuration;

        public TMetricRawDataController(ApplicationDbContext dataContext, ITMetricRawDataServices rawDataServices, IConfiguration configuration)
        {
            this.rawDataServices = rawDataServices;
            this.configuration = configuration;
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetAllProjects(int accountId)
        {
            if (accountId == 0)
            {
                accountId = int.Parse(configuration["TMetricAccountId"]);
            }

            List<Project> projects = await rawDataServices.GetAllProjectsAsync(accountId);

            if (projects != null)
            {
                return Ok(projects);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("accounts")]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            List<Account> accounts = await rawDataServices.GetAllAccountsAsync();

            if (accounts != null)
            {
                return Ok(accounts);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("calendarweekdata")]
        public async Task<IActionResult> GetCalendarWeekDataAsync(int accountId, int userProfileId, DateTime startDateTime, DateTime endDateTime)
        {
            CalendarWeekData calendarWeekData = await rawDataServices.GetCalendarWeekDataAsync(accountId, userProfileId, startDateTime, endDateTime);

            if (calendarWeekData != null)
            {
                return Ok(calendarWeekData);
            }
            else
            {
                return NotFound();
            }
        }

    }
}