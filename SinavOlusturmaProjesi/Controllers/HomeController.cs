using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SinavOlusturmaProjesi.DAL;
using SinavOlusturmaProjesi.Models;

namespace SinavOlusturmaProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        static List<Models.Wired> wiredPosts;
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "User");
            }
            // Wireddan bilgileri çekiyor
            WiredDal getWired= new WiredDal();
            wiredPosts = getWired.GetRSS();

            // wiredpostlarını viewbag' e gömüyoruz dropdown içerisine aktarma yapacağız
            ViewBag.wiredPosts = new SelectList(wiredPosts, "Id", "Title");

            if (Request.Query["id"].Count > 0)
            {
                // Eğer wired başlığı seçildiyse title ve description bilgisi dolduruluyor
                ViewBag.WiredTitle = wiredPosts[Convert.ToInt32(Request.Query["id"])].Title;
                ViewBag.Description = wiredPosts[Convert.ToInt32(Request.Query["id"])].Description;
                ViewBag.Id = Request.Query["id"];
            }

            return View();
        }

        public IActionResult List()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "User");
            }
            ExamDal examDal = new ExamDal();
            List<Models.Exam> examList = new List<Models.Exam>();
            examList = examDal.ExamListRead(examList);
            

            return View(examList);
        }

        [HttpPost]
        public IActionResult List(string examId, string submit)
        {
            if (submit == "Delete")
            {
                if (ModelState.IsValid)
                {
                    ExamDal deleteExamDal = new ExamDal();
                    if (Convert.ToInt32(examId) != 0)
                    {
                        deleteExamDal.ExamDelete(Convert.ToInt32(examId));
                    }
                    return RedirectToAction("List");
                }
            }
            else if (submit == "View")
            {
                Questions sorular = ExamDal.ExamView(Convert.ToInt32(examId));
                return RedirectToAction($"ExamView", new { examId = examId });
            }
            ExamDal examDal = new ExamDal();
            List<Models.Exam> examList = new List<Models.Exam>();
            examList = examDal.ExamListRead(examList);


            return View(examList);
        }
        public IActionResult ExamView()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (Request.Query["examId"].Count>0)
            {
                Questions sorular = ExamDal.ExamView(Convert.ToInt32(Request.Query["examId"]));
                return View(sorular);
            }
            else
            {
                return RedirectToAction($"List");
            }
            
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddExam(Questions examModel)
        {
            if (ModelState.IsValid)
            {
                Questions exam = examModel;
                if(ExamDal.ExamAdd(exam))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult ExamAnswerCheck(int examId)
        {
            ExamDal examDAL = new ExamDal();
            List<string> correctAnswer = ExamDal.GetQuestionCorrectAnswer(examId); // Doğru şıkları listeliyoruz
            CorrectAnswerControl answers = new CorrectAnswerControl();
            // Burada hangi soru şıkkının doğru olduğunu kontrol ediyoruz
            answers.questionsCorrectAnswer = correctAnswer;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(answers); // Listeyi json' a çeviriyoruz js tarafında parse edip json üzerinden kontrollerini yapacak


            // var data = new { status = "ok", result = result };
            json = json.Replace("'\'", "");
            return Json(json);
        }
    }
}
