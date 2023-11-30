using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BakokiWeb.Shared
{
	public class Cliente
	{
		[Key]
		public string Email { get; set; }
			= "";
		public string Password { get; set; } 
			= "";
		public string FirstName { get; set; }
			= "";
		public string LastName { get; set; }
			= "";
		public bool LoggedIn { get; set; }
			=false;
		public string PhoneNumber { get; set; }
			= "";
		public string AddressLine1 { get; set; } 
			= "";
		public string AddressLine2 { get; set; }
			= "";
		public ICollection<Cuenta> Cuentas { get; set; }
			= new List<Cuenta>();
		public Cliente() { }

	}
}
