using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
	public class KeepsService
	{
		private readonly KeepsRepository _repo;

		public KeepsService(KeepsRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<Keep> Get()
		{
			return _repo.Get();
		}

		public Keep Create(Keep newKeep)
		{
			newKeep.Views = 0;
			newKeep.Keeps = 0;
			newKeep.Shares = 0;

			int newId = _repo.Create(newKeep);
			return _repo.GetById(newId);
		}

		public Keep GetById(in int id, in string userId)
		{
			Keep kep2 = _repo.GetById(id);
			if (kep2 == null)
				throw new Exception("Invalid Id");
			else if (kep2.IsPrivate == true && kep2.UserId != userId)
				throw new UnauthorizedAccessException("Cannot access");
			else
				return kep2;
		}

		public void Edit(in Keep kep)
		{
			_repo.Edit(kep);
		}

		public ActionResult<string> Delete(in int id, in string userId)
		{
			return _repo.Delete(id, userId);
		}

		public void Increment(int id, bool b, string userId)
		{
			Keep testKep = _repo.GetById(id);
			if (testKep.UserId != userId)
				throw new UnauthorizedAccessException("Cannot edit a keep you do not own.");
			_repo.Increment(id, b);
		}

		public void IncrementShare(in int id, string userId)
		{
			Keep testKep = _repo.GetById(id);
			if (testKep.UserId != userId)
				throw new UnauthorizedAccessException("Cannot edit a keep you do not own.");
			_repo.IncrementShare(id);
		}
	}
}