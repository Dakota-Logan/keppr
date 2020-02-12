using System;
using System.Collections;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
	public class VaultsService
	{
		private readonly VaultsRepository _repo;

		public VaultsService(VaultsRepository repo)
		{
			_repo = repo;
		}
		public IEnumerable<Vault> Get(in string userId)
		{
			return _repo.Get(userId);
		}

		public Vault GetById(in int id, string userId)
		{
			Vault vlat = _repo.GetById(id);
			if (vlat == null || vlat.UserId != userId)
				throw new Exception("Can't do that - bad VaultId.");
			else
				return vlat;
		}

		public Vault Create(Vault newVault)
		{
			int vlatId = _repo.Create(newVault);
			return _repo.GetById(vlatId);
		}

		public void Delete(in int id, string userId)
		{
			_repo.Delete(id, userId);
		}
	}
}