using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaProjesi.Models
{
    public class Exam
    {
		private int examId;
		private string title;
		private string description;
		private string publishDate;

		
		public int ExamId { get => examId; set => examId = value; }
		public string Title { get => title; set => title = value; }
		public string Description { get => description; set => description = value; }
		public string PublishDate { get => publishDate; set => publishDate = value; }


	}
}
