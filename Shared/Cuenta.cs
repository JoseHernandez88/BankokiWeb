using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakokiWeb.Shared
{
	public class Cuenta
	{
		[Key]
		public string AccountNumber { get; set; } 
			= "";
		public string AccountName { get; set; } 
			= "";
		public bool IsOpen { get; set; }
			=true;
		public Cliente Cliente { get; set; }
			= new Cliente();
		public ICollection<Transacion> Transaciones { get; set; } 
			= new List<Transacion>();

		public Cuenta() { }	

		