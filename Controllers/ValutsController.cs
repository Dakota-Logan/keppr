using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
	[ApiController]
	[Route("api/vaults")]
	public class VaultsController : ControllerBase
	{
		private readonly VaultsService _vs;

		public VaultsController(VaultsService vs)
		{
			_vs = vs;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Vault>> Get()
		{
			try
			{
				var ctx = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
				if(ctx == null)
					return BadRequest("You need to be logged in to do that!");
				var userId = ctx.Value;
				return Ok(_vs.Get(userId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			;
		}

		[HttpGet("{id}")]
		public ActionResult<Vault> GetById([FromRoute] int id)
		{
			try
			{
				var ctx = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
				if(ctx == null)
					return BadRequest("You need to be logged in to do that!");
				var userId = ctx.Value;
				return Ok(_vs.GetById(id, userId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public ActionResult<Vault> Post([FromBody] Vault newVault)
		{
			try
			{
				var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				newVault.UserId = userId;
				return Ok(_vs.Create(newVault));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public ActionResult<string> Delete([FromRoute] int id)
		{
			var ctx = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
			if(ctx == null)
				return BadRequest("You need to be logged in to do that!");
			var userId = ctx.Value;
			try
			{
				_vs.Delete(id, userId);
				return Ok("Successfully deleted!");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}