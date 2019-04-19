using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightScheduleManager.Graph;
using FlightScheduleManager.Models;

namespace FlightScheduleManager.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET /api/users
        public async Task<ActionResult<ScheduleUser>> GetScheduleUser([FromHeader] string authorization)
        {
            var token = GraphService.ValidateBearerToken(authorization);
            if (string.IsNullOrEmpty(token))
            {
                return new UnauthorizedResult();
            }

            return await GraphService.GetUserInfo(token);
        }
    }
}