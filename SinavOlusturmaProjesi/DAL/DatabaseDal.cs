using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaProjesi.DAL
{
    public class DatabaseDal
    {
		

		// DB' yi çağırdığımız yer db yaratılmadıysa derleyip çalıştırdığında otomatik çalışır.
		public SqliteConnection GetDatabase()
		{
			SqliteConnectionStringBuilder sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder();
			sqliteConnectionStringBuilder.DataSource = $"sınavOlusturmaDB.db";
			SqliteConnection con = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString);
			return con;
		}

	}
}
