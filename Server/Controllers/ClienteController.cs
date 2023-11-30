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
		public async Task<ActionResult<List<Cliente?>>> GetAllClient()
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
		public async Task<ActionResult<List<Cliente?>>> GetClienteByEmail(string email)
		{
			var cli = await _context.Clientes.FindAsync(email);
			if (cli == null)
			{
				return Ok(new List<Cliente>() );
			}
			return Ok(new List<Cliente>() { cli });
		}
		[HttpGet("{password}/{email}")]
		public async Task<ActionResult<List<Cliente?>>> GetClienteVerifyPassword(string password, string email)
		{
			try
			{
				var wrapper = await GetClienteByEmail(email);
				List<Cliente?> list = new();
				if (wrapper != null)
				{
					if (wrapper.Value != null)
					{
						list = wrapper.Value.ToList();
					}
					
				}
				else
				{
					throw new Exception("Cliente Control: Verify password lost a list.");
				}

				bool result = false;
				if (list.Any())
				{
					var cli = list.FirstOrDefault();
					if (cli != null)
					{
						result = cli.Password.Equals(password);
						if (result)
						{
							await this.LoggedIn(email);
							return Ok(new List<Cliente?>() { cli });
						}
					}
				}
				return Ok(new List<Cliente?>() { });
			}
			catch (Exception ex)
			{
				return BadRequest(ex.ToString());
			}
		}
		[HttpPut("put/1/{email}")]
		public async Task<ActionResult<Cliente?>> LoggedIn(string email)
		{
			List<Cliente?> list = new();
			var wrapper = await GetClienteByEmail(email);
			if (wrapper.Value != null)
			{
				list=wrapper.Value.ToList();
			}
			else
			{
				throw new Exception("Cliente Controller: LoggedIn lost a list.");
			}
			var cli=list.FirstOrDefault();
			if (cli != null)
			{
				cli.LoggedIn = true;
				await _context.SaveChangesAsync();
				return Ok(cli);
			}
			else
			{
				return BadRequest($"Cliente Controller:Could not find {email}");
			}
		}
		[HttpPut("put/0/{email}")]
		public async Task<ActionResult<Cliente?>> Loggedoff(string email)
		{
			List<Cliente?> list = new();
			var wrapper = await GetClienteByEmail(email);
			if (wrapper.Value != null)
			{
				list = wrapper.Value.ToList();
			}
			else
			{
				throw new Exception("Cliente Controller: LoggedIn lost a list.");
			}
			var cli = list.FirstOrDefault();
			
			if (cli != null)
			{
				cli.LoggedIn = false;
				await _context.SaveChangesAsync();
				return Ok(cli);
			}
			else
			{
				return BadRequest($"Cliente Controller:Could not find {email}");
			}
		}
		[HttpPost]
		public async Task<ActionResult<Cliente?>> PostCuenta(Cliente cliente)
		{
			_context.Clientes.Add(cliente);
			await _context.SaveChangesAsync();

			return Ok(cliente);
		}

	}
}
