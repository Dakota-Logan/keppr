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
	[Route("api/vaultkeeps")]
	public class VaultKeepsController : ControllerBase
	{
		private readonly VaultKeepsService _vks;

		public VaultKeepsController(VaultKeepsService vks)
		{
			_vks = vks;
		}

		[HttpGet]
		[Authorize]
		public ActionResult<IEnumerable<VaultKeeps>> Get()
		{

			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				return Ok(_vks.Get(userId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}")]
		[Authorize]
		public ActionResult<VaultKeeps> GetById([FromRoute] in int id)
		{
			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				return Ok(_vks.GetById(id, userId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public ActionResult<VaultKeeps> Post([FromBody] VaultKeeps vk)
		{
			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				vk.UserId = userId;
				return Ok(_vks.Create(vk));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public ActionResult<string> Delete([FromRoute] in int id)
		{
			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				_vks.Delete(id, userId);
				return Ok("Alls good, thing was blownsed up");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
