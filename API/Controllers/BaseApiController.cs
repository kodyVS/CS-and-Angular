using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    //Route that goes to this controller I.E https://localhost:5001/api/users. users comes from the Name of the route
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {

    }
}