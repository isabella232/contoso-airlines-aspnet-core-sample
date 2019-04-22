using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightScheduleManager.Graph;
using FlightScheduleManager.Models;
using Microsoft.Graph;

namespace FlightScheduleManager.Controllers
{
    [Route("api/[controller]")]
    public class SchedulesController : Controller
    {
        // GET /api/flights
        [HttpGet]
        public async Task<ActionResult<List<Schedule>>> GetSchedules(
          [FromHeader] string authorization,
          [FromQuery] string start,
          [FromQuery] string end)
        {
            var token = GraphService.ValidateBearerToken(authorization);
            if (string.IsNullOrEmpty(token))
            {
                return new UnauthorizedResult();
            }

            var scheduleInfos = await GraphService.GetSchedules(token, start, end);

            if (scheduleInfos != null)
            {
                var schedules = new List<Schedule>();

                foreach (var schedule in scheduleInfos.CurrentPage)
                {
                    schedules.Add(new Schedule(schedule));
                }

                return schedules;
            }

            return null;
        }
    }
}
