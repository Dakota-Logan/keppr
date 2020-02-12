using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
	public class VaultKeepsService
	{
		private readonly VaultKeepsRepository _vkr;

		public VaultKeepsService(VaultKeepsRepository vkr)

		{
			_vkr = vkr;
		}
		public IEnumerable<VaultKeeps> Get(string userId, int vaultId)
		{
			return _vkr.Get(userId, vaultId);
		}

		public VaultKeeps GetById(in int id, string userId)
		{
			VaultKeeps valk = _vkr.GetById(id);
			if(valk == null || valk.UserId!=userId)
				throw new Exception("Bad id yo!");
			return valk;
		}

		public string Create(in VaultKeeps vk)
		{
			VaultKeeps vk1 = _vkr.CheckExists(vk);
			if (vk1 != null)
				return "VaultKeep";

			_vkr.Create(vk);
			return "VaultKeep";
		}

		public IEnumerable<Keep> GetKeeps(int id,string userId)
		{
			return _vkr.GetKeeps(id, userId);
		}

		public void Delete(in int vaultId, in int keepId, string userId)
		{
			_vkr.Delete(vaultId, keepId, userId);
		}
	}
}