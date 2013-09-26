using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Questionnaires.Web.Models;

namespace Questionnaires.Web.Controllers
{
    public class QuestionnaireController : Controller
    {
        private readonly QuestionnaireContext db = new QuestionnaireContext();

        //
        // GET: /Questionnaire/

        public ActionResult Index()
        {
            return View(db.Questionnaires.ToList());
        }

        //
        // GET: /Questionnaire/Backbone

        public ActionResult Backbone()
        {
            return View();
        }

        //
        // GET: /Questionnaire/Details/5

        public ActionResult Details(int id = 0)
        {
            Questionnaire questionnaire = db.Questionnaires
                                            .Include(x => x.Questions)
                                            .SingleOrDefault(x => x.Id == id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        //
        // GET: /Questionnaire/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Questionnaire/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Questionnaires.Add(questionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionnaire);
        }

        //
        // GET: /Questionnaire/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        //
        // POST: /Questionnaire/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionnaire);
        }

        //
        // GET: /Questionnaire/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        //
        // POST: /Questionnaire/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            db.Questionnaires.Remove(questionnaire);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Questionnaire/Answer/5

        public ActionResult Answer(int id = 0)
        {
            var questionnaire = db.Questionnaires
                                  .Include("Questions")
                                  .Include("Questions.Choices")
                                  .SingleOrDefault();
            return View(questionnaire);
        }

        //
        // POST: /Questionnaire/Answer/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Answer(Questionnaire questionnaire)
        {
            var sessionId = Session.SessionID;
            foreach (var question in questionnaire.Questions)
            {
                db.Answers.Add(new Answer
                    {
                        SessionId = sessionId,
                        QuestionId = question.Id,
                        ChoiceId = question.Choices.First().Id
                    });
            }
            db.SaveChanges();
            TempData["Message"] = string.Format("Thank you for answering {0}",
                                                db.Questionnaires.Find(questionnaire.Id).Title);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}