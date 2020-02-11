using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tmetricstatistics.Services;

namespace tmetricstatistics.Controller
{
    [Route("api/v1/")]
    [ApiController]
    public class TMetricDatabaseController : ControllerBase
    {
        private readonly ITMetricDatabaseServices databaseServices;

        public TMetricDatabaseController(ITMetricDatabaseServices databaseServices)
        {
            this.databaseServices = databaseServices;
        }

        [HttpGet("get-projects-from-db")]
        public ActionResult GetProjectsFromDb()
        {
            return Ok(databaseServices.GetAllProjects());
        }
    }
}