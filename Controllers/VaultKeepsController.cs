using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
	[ApiController]
	[Route("api/vaultkeeps")]
	public class VaultKeepsController : ControllerBase
	{
		private readonly VaultKeepsService _vks;
		private readonly KeepsService _ks;

		public VaultKeepsController(VaultKeepsService vks, KeepsService ks)
		{
			_vks = vks;
			_ks = ks;
		}

		[HttpGet("{vaultId}")]
		[Authorize]
		public ActionResult<IEnumerable<Keep>> Get([FromRoute]int vaultId)
		{

			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				IEnumerable<VaultKeeps> vks = _vks.Get(userId, vaultId);
				List<Keep> keps = new List<Keep>();
				foreach (VaultKeeps vk in vks)
				{
					keps.Add(_ks.GetById(vk.KeepId, userId));
				}
				return keps;
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}/keeps")]
		[Authorize]
		public ActionResult<VaultKeeps> GetById([FromRoute] int id)
		{
			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				return Ok(_vks.GetKeeps(id, userId));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public ActionResult<string> Post([FromBody] VaultKeeps vk)
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

		[HttpDelete("{vaultId}/keeps/{keepId}")]
		[Authorize]
		public ActionResult<string> Delete([FromRoute] int vaultId, [FromRoute] int keepId)
		{
			try
			{
				string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
				_vks.Delete(vaultId, keepId, userId);
				return Ok("VaultKeep");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
