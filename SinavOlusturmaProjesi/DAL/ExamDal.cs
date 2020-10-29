using Microsoft.Data.Sqlite;
using SinavOlusturmaProjesi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaProjesi.DAL
{
    public class ExamDal
    {


		public static bool ExamAdd(Questions Exam)
		{
			DatabaseDal dataconnect = new DatabaseDal();
			using (var connect = dataconnect.GetDatabase())
			{
				int examId = 0;
				connect.Open();
				string exam_sql = "INSERT INTO Exams(examTitle,examDescription,examCreatedDate) VALUES (@title,@description,@createdDate); SELECT last_insert_rowid()";
				SqliteCommand exam_cmd = new SqliteCommand(exam_sql, connect);
				exam_cmd.Parameters.AddWithValue("@title", Exam.Title);
				exam_cmd.Parameters.AddWithValue("@description", Exam.Description);
				DateTime myDateTime = DateTime.Now;
				string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
				exam_cmd.Parameters.AddWithValue("@createdDate", sqlFormattedDate);
				examId = Convert.ToInt32(exam_cmd.ExecuteScalar());
				if (examId > 0)
				{
					int result = 0;
					for (int i = 0; i < 4; i++)
					{
						string question_sql = "INSERT INTO Questions(question,questionAnswerA,questionAnswerB,questionAnswerC,questionAnswerD,questionCorrectAnswer,examId)"
							+ "VALUES (@question,@answerA,@answerB,@answerC,@answerD,@correctAnswer,@examId)";
						SqliteCommand question_cmd = new SqliteCommand(question_sql, connect);
						question_cmd.Parameters.AddWithValue("@question", Exam.Question[i]);
						question_cmd.Parameters.AddWithValue("@answerA", Exam.AnswerA[i]);
						question_cmd.Parameters.AddWithValue("@answerB", Exam.AnswerB[i]);
						question_cmd.Parameters.AddWithValue("@answerC", Exam.AnswerC[i]);
						question_cmd.Parameters.AddWithValue("@answerD", Exam.AnswerD[i]);
						question_cmd.Parameters.AddWithValue("@correctAnswer", Exam.CorrectAnswer[i]);
						question_cmd.Parameters.AddWithValue("@examId", examId);
						result = question_cmd.ExecuteNonQuery();
						if (result <= 0) 
						{
							return false;
						}
					}
						
				}
                else
                {
					return false;
                }
				return true;
			}
		}

		public List<Models.Exam> ExamListRead(List<Models.Exam> examList)
        {
			DatabaseDal dataconnect = new DatabaseDal();
			using (var connect = dataconnect.GetDatabase())
			{
				connect.Open();
				string exam_sql = "SELECT * FROM Exams";
				using (SqliteCommand exam_cmd = new SqliteCommand(exam_sql, connect))
				{
					using (SqliteDataReader reader = exam_cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							examList.Add(new Exam()
							{
								ExamId = Convert.ToInt32(reader["examId"]),
								Title = reader["examTitle"].ToString(),
								PublishDate = reader["examCreatedDate"].ToString(),
							});
						}
					}
				}
				connect.Close();
			}
			return examList;
        }

		public void ExamDelete(int examId)
		{
			DatabaseDal dataconnect = new DatabaseDal();
			using (var connect = dataconnect.GetDatabase())
			{
				connect.Open();
				string exam_sql = "DELETE FROM Questions WHERE examId=@examId;"
					+ "DELETE FROM Exams WHERE examId = @examId";
				using (SqliteCommand exam_cmd = new SqliteCommand(exam_sql, connect))
				{
					exam_cmd.Parameters.AddWithValue("@examId", examId);
					exam_cmd.ExecuteNonQuery();
				}
				connect.Close();
			}
		}

		public static Questions ExamView(int examId)
        {
			Questions examView = new Questions();
			DatabaseDal dataconnect = new DatabaseDal();
			using (var connect = dataconnect.GetDatabase())
			{
				connect.Open();
				string exam_sql = "SELECT * FROM Exams WHERE examId=@examId";
				using (SqliteCommand exam_cmd = new SqliteCommand(exam_sql, connect))
				{
					exam_cmd.Parameters.AddWithValue("@examId", examId);
					using (SqliteDataReader exam_reader = exam_cmd.ExecuteReader())
					{
						while (exam_reader.Read())
						{
							examView.ExamId = Convert.ToInt32(exam_reader["examId"]);
							examView.Title = exam_reader["examTitle"].ToString();
							examView.Description = exam_reader["examDescription"].ToString();
						}
					}
				}

				string exam_question_sql = "SELECT * FROM Questions WHERE examId=@examId";
				using (SqliteCommand exam_question_cmd = new SqliteCommand(exam_question_sql, connect))
				{
					exam_question_cmd.Parameters.AddWithValue("@examId", examId);
					using (SqliteDataReader exam_reader = exam_question_cmd.ExecuteReader())
					{
						int i = 0;
						List<int> questionId = new List<int>();
						List<string> question = new List<string>();
						List<string> answerA = new List<string>();
						List<string> answerB = new List<string>();
						List<string> answerC = new List<string>();
						List<string> answerD = new List<string>();
						List<string> corretctAnswer = new List<string>();
						while (exam_reader.Read())
						{
							questionId.Add(Convert.ToInt32(exam_reader["questionId"]));
							question.Add(exam_reader["question"].ToString());
							answerA.Add(exam_reader["questionAnswerA"].ToString());
							answerB.Add(exam_reader["questionAnswerB"].ToString());
							answerC.Add(exam_reader["questionAnswerC"].ToString());
							answerD.Add(exam_reader["questionAnswerD"].ToString());
							corretctAnswer.Add(exam_reader["questionCorrectAnswer"].ToString());
						}
						examView.QuestionId = questionId;
						examView.Question = question;
						examView.AnswerA = answerA;
						examView.AnswerB = answerB;
						examView.AnswerC = answerC;
						examView.AnswerD = answerD;
						examView.CorrectAnswer = corretctAnswer;
					}
				}
				connect.Close();
			}
			return examView;
        }
		public static List<string> GetQuestionCorrectAnswer(int examId)
		{
			List<string> correctAnswer = new List<string>();
			DatabaseDal dataconnect = new DatabaseDal();
			using (var connect = dataconnect.GetDatabase())
			{
				connect.Open();
				string exam_sql = "SELECT questionCorrectAnswer FROM Questions WHERE examId=@examId";
				using (SqliteCommand exam_cmd = new SqliteCommand(exam_sql, connect))
				{
					exam_cmd.Parameters.AddWithValue("@examId", examId);
					using (SqliteDataReader reader = exam_cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							correctAnswer.Add(reader["questionCorrectAnswer"].ToString());
						}
					}
				}
				connect.Close();
			}
			return correctAnswer;
		}
	}
}
