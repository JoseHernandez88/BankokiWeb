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
		public async Task<ActionResult<List<Cuenta>>> GetAllClienteCuentas(string email)
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
		[HttpPost]
		public async Task<ActionResult<Cliente>> PostCuenta(Cuenta cuenta)
		{
			_context.Cuentas.Add(cuenta);
			await _context.SaveChangesAsync();
			return Ok(cuenta);
		}

	}
}
