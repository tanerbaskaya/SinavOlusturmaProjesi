using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaProjesi.DAL
{
    public class LoginDal
    {
		DatabaseDal data = new DatabaseDal();
		public bool UserControl(string userName, string password)
		{
			Int64 result = 0;
			using (var dbconnect = data.GetDatabase())
			{
				dbconnect.Open();
				var tableCmd = dbconnect.CreateCommand();
				tableCmd.CommandText = $"select Count(*) from Users where userName='{userName}' AND userPassword='{password}'";
				result = (Int64)tableCmd.ExecuteScalar();
			}
			if (result > 0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
	}
}
