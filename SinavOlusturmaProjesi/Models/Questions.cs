using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaProjesi.Models
{
    public class Questions:Exam
    {
        private List<int> questionid;
        private List<string> question;
        private List<string> answera;
        private List<string> answerb;
        private List<string> answerc;
        [Required(ErrorMessage = "Cevap A alanı boş olamaz!")]
        public List<String> AnswerA { get; set; }
        private List<string> answerd;
        private List<string> correctanswer;

        public List<int> QuestionId { get => questionid; set => questionid = value; }
        public List<string> Question { get => question; set => question = value; }
        //public List<string> AnswerA { get => answera; set => answera = value; }
        public List<string> AnswerB { get => answerb; set => answerb = value; }
        public List<string> AnswerC { get => answerc; set => answerc = value; }
        public List<string> AnswerD { get => answerd; set => answerd = value; }
        public List<string> CorrectAnswer { get => correctanswer; set => correctanswer = value; }
    }
}
