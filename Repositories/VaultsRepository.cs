using Keepr.Models;
using Dapper;
using System;
using System.Data;
using System.Collections.Generic;

namespace Keepr.Repositories
{
	public class VaultsRepository
	{
		private readonly IDbConnection _db;

		public VaultsRepository(IDbConnection db)
		{
			_db = db;
		}

		public IEnumerable<Vault> Get(in string userId)
		{
			string sql = "SELECT * FROM vaults WHERE userId = @userId";
			return _db.Query<Vault>(sql, new {userId});
		}

		public Vault GetById(in int id)
		{
			string sql = "SELECT * FROM vaults where id = @id";
			return _db.QueryFirstOrDefault<Vault>(sql, new {id});
		}

		public int Create(Vault newVault)
		{
			string sql = @"INSERT INTO vaults (name, description, userId) 
				VALUES (@Name, @Description, @UserID);
				SELECT LAST_INSERT_ID()";
			return _db.ExecuteScalar<int>(sql, newVault);
		}

		public void Delete(in int id, in string userId)
		{
			string sql = "DELETE FROM vaults WHERE id = @id AND userId = @userId";
			_db.Execute(sql, new {id, userId});
		}
	}
}