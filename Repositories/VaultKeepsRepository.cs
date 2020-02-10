using System.Collections.Generic;
using System.Data;
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

		public IEnumerable<VaultKeeps> Get(string userId)
		{
			string sql = "SELECT * FROM vaultkeeps WHERE userId = @userId";
			return _db.Query<VaultKeeps>(sql, new {userId});
		}

		public VaultKeeps GetById(in int id)
		{
			string sql = "SELECT * FROM vaultkeeps WHERE id = @id";
			return _db.QueryFirstOrDefault<VaultKeeps>(sql, new {id});
		}

		public int Create(VaultKeeps vk)
		{
			string sql = @"INSERT INTO vaultkeeps (vaultId, keepId, userId)
					VALUES (@VaultId, @KeepId, @UserId);
					SELECT LAST_INSERT_ID()";
			return _db.ExecuteScalar<int>(sql, vk);
		}

		public void Delete(in int id, string userId)
		{
			string sql = @"DELETE FROM vaultkeeps WHERE id = @id AND userId = @userId";
			_db.Execute(sql, new {id, userId});
		}
	}
}