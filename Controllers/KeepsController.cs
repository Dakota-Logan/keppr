using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
	[Route("api/keeps")]
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
				string userId;
				var ctx = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
				if (ctx == null)
					userId = "null";
				else
					userId = ctx.Value;
				return Ok(_ks.GetById(id, userId));
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
				var ctx = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
				if(ctx==null)
					throw new Exception("You need to be logged in to do that!");
				string userId = ctx.Value;
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

		[HttpPut("{id}/shares")]
		public ActionResult<string> IncrementShares([FromRoute] int id)
		{
			string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			try
			{
				_ks.IncrementShare(id, userId);
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