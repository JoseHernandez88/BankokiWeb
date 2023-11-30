using BakokiWeb.Server.Data;
using BakokiWeb.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakokiWeb.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CuentaController : ControllerBase
	{
		private readonly DataContext _context;
		public CuentaController(DataContext context)
		{
			_context = context;
		}
		[HttpGet]
		public async Task<ActionResult<List<Cuenta>>> GetAllCuentas()
		{
			try
			{
				var list = await _context.Cuentas.ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{email}")]
		public async Task<ActionResult<List<Cuenta>>> GetAllCuentasByCliente(string email)
		{
			try
			{
				var list = await _context.Cuentas.Where
				(
					cnt => cnt.IsOpen &&
					cnt.Cliente.LoggedIn &&
					cnt.Cliente.Email.Equals(email)
				).ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{accountNumber}")]
		public async Task<ActionResult<Cuenta>> GetCuentaByAccountNumber(string accountNumber)
		{
			try
			{
				var list = await _context.Cuentas.Where
				(
					cnt => cnt.IsOpen &&
					cnt.Cliente.LoggedIn &&
					cnt.AccountNumber.Equals(accountNumber)
				).ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPost]
		public async Task<ActionResult<Cliente>> PostCuenta(Cuenta cuenta)
		{
			_context.Cuentas.Add(cuenta);
			await _context.SaveChangesAsync();
			return Ok(cuenta);
		}
		[HttpPut("{accountNumber}/{password}")]
		public async Task<ActionResult<bool>> PutCloseAccount(string accountNumber, string password) 
		{
			var wrapper= GetCuentaByAccountNumber(accountNumber);
			var cuenta = await wrapper;
			if (cuenta.Value != null)
			{
				if (cuenta.Value.Cliente.Password.Equals(password))
				{
					cuenta.Value.IsOpen = false;
					await _context.SaveChangesAsync();
					return true;
				}
				else
				{
					return false;
				}
			}
			
			return BadRequest("No such account."); 
		}

	}
}
