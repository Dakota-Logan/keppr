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
		public IEnumerable<VaultKeeps> Get(string userId)
		{
			return _vkr.Get(userId);
		}

		public VaultKeeps GetById(in int id, string userId)
		{
			VaultKeeps valk = _vkr.GetById(id);
			if(valk == null || valk.UserId!=userId)
				throw new Exception("Bad id yo!");
			return valk;
		}

		public VaultKeeps Create(in VaultKeeps vk)
		{
				int id =_vkr.Create(vk);
				return _vkr.GetById(id);
		}

		public void Delete(in int id, string userId)
		{
			_vkr.Delete(id, userId);
		}
	}
}