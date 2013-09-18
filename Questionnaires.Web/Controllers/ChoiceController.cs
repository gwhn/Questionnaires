using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Questionnaires.Web.Models;

namespace Questionnaires.Web.Controllers
{
    public class ChoiceController : Controller
    {
        private readonly QuestionnaireContext db = new QuestionnaireContext();

        //
        // GET: /Choice/Create

        public ActionResult Create(int questionId)
        {
            return View(new Choice {QuestionId = questionId});
        }

        //
        // POST: /Choice/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("Details", "Question", new {id = choice.QuestionId});
            }

            return View(choice);
        }

        //
        // GET: /Choice/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        //
        // POST: /Choice/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Question", new {id = choice.QuestionId});
            }
            return View(choice);
        }

        //
        // GET: /Choice/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        //
        // POST: /Choice/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
            db.SaveChanges();
            return RedirectToAction("Details", "Question", new {id = choice.QuestionId});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}