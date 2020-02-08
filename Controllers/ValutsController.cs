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
	[Route("api/[controller]")]
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
				return Ok(_vs.Get());
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
				var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				return Ok(_vs.GetById(id, userId).Value);
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

		[HttpPut("{id}")]
		[Authorize]
		public ActionResult<string> Edit([FromBody] Vault kep, [FromRoute] int id)
		{
			kep.Id = id;
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			kep.UserId = userId;
			try
			{
				_vs.Edit(kep);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok("Was gud edit, thanks fren!");
		}

		[HttpPut("{id}/views")]
		public ActionResult<string> IncrementViews([FromRoute] int id)
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			try
			{
				_vs.Increment(id, false, userId);
				return Ok("Was gud, tanks.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}/keeps")]
		public ActionResult<string> IncrementVaults([FromRoute] int id)
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			try
			{
				_vs.Increment(id, true, userId);
				return Ok("Was gud, tanks.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public ActionResult Delete([FromRoute] int id)
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			try
			{
				return Ok(_vs.Delete(id, userId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}