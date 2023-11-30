using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakokiWeb.Shared
{
	public class Transacion
	{
		[Key]
		public Int64 TransactionID { get; set; }
		public Int64 Amount { get; set; }
		public bool IsCredit { get; set; }
		public DateTime FilledAt { get; set; }
		public string Origin { get; set; } 
			= "";
		public Transacion() { }
	}
}
