using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class KeepsController : ControllerBase
	{
		private readonly KeepsService _ks;

		public KeepsController(KeepsService ks)
		{
			_ks = ks;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Keep>> Get()
		{
			try
			{
				return Ok(_ks.Get());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			;
		}

		[HttpGet("{id}")]
		public ActionResult<Keep> GetById([FromRoute] int id)
		{
			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				return Ok(_ks.GetById(id, userId).Value);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public ActionResult<Keep> Post([FromBody] Keep newKeep)
		{
			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				newKeep.UserId = userId;
				return Ok(_ks.Create(newKeep));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}")]
		[Authorize]
		public ActionResult<string> Edit([FromBody] Keep kep, [FromRoute] int id)
		{
			kep.Id = id;
			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			kep.UserId = userId;
			try
			{
				_ks.Edit(kep);
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
			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			try
			{
				_ks.Increment(id, false, userId);
				return Ok("Was gud, tanks.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut("{id}/keeps")]
		public ActionResult<string> IncrementKeeps([FromRoute] int id)
		{
			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			try
			{
				_ks.Increment(id, true, userId);
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
				return Ok(_ks.Delete(id, userId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}