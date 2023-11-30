using BakokiWeb.Server.Data;
using BakokiWeb.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakokiWeb.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClienteController : ControllerBase
	{
		private readonly DataContext _context;

		public ClienteController(DataContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<Cliente>>> GetAllClient()
		{
			try
			{
				var list = await _context.Clientes.ToListAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{email}")]
		public async Task<ActionResult<Cliente>> GetClienteByEmail(string email)
		{
			try
			{
				var cli = await _context.Clientes.FindAsync(email);
				return Ok(cli);
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpGet("{password}/{email}")]
		public async Task<ActionResult<Cliente>> GetClienteVerifyPassword(string password, string email)
		{
			try
			{
				var cli = await GetClienteByEmail(email);
				bool result = false;
				if (cli.Value != null)
				{
					result = cli.Value.Password.Equals(password);
					if (result)
					{
						await this.LoggedIn(email);
						return Ok(cli.Value);
					}
					else
					{
						return Ok(null);
					}

				}
				else
				{
					throw new Exception($"Cliente Control: Could not find {email}");
				}
			} catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}
		}
		[HttpPut("{email}")]
		public async Task<ActionResult<Cliente>> LoggedIn(string email)
		{
			var cli = await GetClienteByEmail(email);
			if (cli.Value != null)
			{
				cli.Value.LoggedIn = true;
				await _context.SaveChangesAsync();
				return Ok(cli);
			}
			else
			{
				return BadRequest($"Cliente Controller:Could not find {email}");
			}
		}
		[HttpPut("{email}")]
		public async Task<ActionResult<Cliente>> Loggedoff(string email)
		{
			var cli = await GetClienteByEmail(email);
			if (cli.Value != null)
			{
				cli.Value.LoggedIn = false;
				await _context.SaveChangesAsync();
				return Ok(cli);
			}
			else
			{
				return BadRequest($"Cliente Controller:Could not find {email}");
			}
		}
		[HttpPost]
		public async Task<ActionResult<Cuenta>> PostCuenta(Cuenta cuenta)
		{
			_context.Cuentas.Add(cuenta);
			await _context.SaveChangesAsync();

			return Ok(cuenta);
		}

	}
}
