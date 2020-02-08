using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Keepr.Repositories
{
	public class KeepsRepository
	{
		private readonly IDbConnection _db;

		public KeepsRepository(IDbConnection db)
		{
			_db = db;
		}

		internal IEnumerable<Keep> Get()
		{
			string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
			return _db.Query<Keep>(sql);
		}

		internal Keep GetById(in int id)
		{
			string sql = "SELECT * FROM keeps WHERE id = @id";
			return _db.QueryFirstOrDefault<Keep>(sql, new {id});
		}

		internal int Create(Keep keepData)
		{
			string sql = @"INSERT INTO keeps (userId, name, description, img, isPrivate, views, shares, keeps) 
    				VALUES (@UserId, @Name, @Description, @Img, @IsPrivate, @Views, @Shares, @Keeps);
    				SELECT LAST_INSERT_ID()";
			return _db.ExecuteScalar<int>(sql, keepData);
		}

		internal void Edit(in Keep kep)
		{
			string sql = "SELECT * FROM keeps WHERE id = @id";
			Keep kep2 = _db.QueryFirstOrDefault<Keep>(sql, new {id = kep.Id});
			if (kep2 == null)
				throw new Exception("Bad keep id.");
			else if (kep2.UserId != kep.UserId)
				throw new UnauthorizedAccessException("Cannot edit a keep you do not own");

			sql = @"UPDATE keeps 
				SET	name = @Name,
					description = @Description,
					img = @Img,
					isPrivate = @IsPrivate,
					views = @Views,
					keeps = @Keeps
				WHERE id = @Id";

			_db.ExecuteScalar(sql, kep);
		}

		internal void Increment(in int id, in bool b)
		{
			if (b)
			{
				string sql = "UPDATE keeps SET keeps = keeps+1 WHERE id = @id";
				_db.QueryFirstOrDefault<Keep>(sql, new {id});
			}
			else
			{
				string sql = "UPDATE keeps SET views = views+1 WHERE id = @id";
				_db.QueryFirstOrDefault<Keep>(sql, new {id});
			}
		}

		internal string Delete(in int id, in string userId)
		{
			Keep kep2 =_db.QueryFirstOrDefault<Keep>("SELECT * FROM keeps WHERE id = @id", new {id});
			if (kep2 == null)
				throw new Exception("Bad keep Id");
			if (kep2.UserId != userId)
				throw new Exception("Can not edit a keep you do not own.");

			string sql = "DELETE FROM keeps WHERE id = @Id";
			_db.Execute(sql, new {Id = id});
			return "Deleted";
		}
	}
}