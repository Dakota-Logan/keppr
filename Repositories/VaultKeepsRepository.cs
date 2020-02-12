using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
	public class VaultKeepsRepository
	{
		private readonly IDbConnection _db;

		public VaultKeepsRepository(IDbConnection db)
		{
			_db = db;
		}

		public IEnumerable<VaultKeeps> Get(string userId, int vaultId)
		{
			throw new NotImplementedException();
		}

		public VaultKeeps GetById(in int id)
		{
			string sql = "SELECT * FROM vaultkeeps WHERE id = @id";
			return _db.QueryFirstOrDefault<VaultKeeps>(sql, new {id});
		}

		public IEnumerable<Keep> GetKeeps(int id, string userId)
		{
			string sql = @"SELECT k.* FROM vaultkeeps vk
			INNER JOIN keeps k ON k.id = vk.keepId
			WHERE (vaultId = @vaultId AND vk.userId = @userId)";

			return _db.Query<Keep>(sql, new {vaultId=id, userId});
		}

		public void Create(VaultKeeps vk)
		{
			string sql = "INSERT INTO vaultkeeps (vaultId, keepId, userId) VALUES (@VaultId, @KeepId, @UserId);";
			_db.Execute(sql, vk);
		}

		public void Delete(in int vaultId, in int keepId, in string userId)
		{
			string sql = "DELETE FROM vaultkeeps WHERE vaultId = @vaultId AND keepId = @keepId AND userId = @userId";
			_db.Execute(sql, new {vaultId, keepId, userId});
		}

		public VaultKeeps CheckExists(in VaultKeeps vk)
		{
			string sql = "SELECT * FROM vaultkeeps WHERE vaultId = @VaultId AND keepId = @KeepId AND userId = @UserId";
			return _db.QueryFirstOrDefault<VaultKeeps>(sql, vk);
		}
	}
}