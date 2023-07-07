using BL.Interfaces;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AirportController : ControllerBase
	{
		private readonly ISimulator _simulator;
        private const string _homePageString = "Airport Server is Up";

        public AirportController(ISimulator simulator)
		{
			_simulator = simulator;
		}

        [HttpGet("home")]
        public string GetHome()
        {
            return _homePageString;
        }

        [HttpGet("start")]
		public void GetStart()
		{
			 _simulator.Start();
		}

        [HttpGet("status")]
        public bool GetStatus()
        {
			return _simulator.isStarted;
        }
    }
}
