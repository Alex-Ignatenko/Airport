using DAL.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class AirportDbContext : DbContext
	{
		public AirportDbContext(DbContextOptions options) : base(options)
		{

		}
		public virtual DbSet<FlightDto> FlightDtos { get; set; }
	}

}
