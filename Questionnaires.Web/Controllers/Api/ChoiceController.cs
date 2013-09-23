using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Questionnaires.Web.Models;

namespace Questionnaires.Web.Controllers.Api
{
    public class ChoiceController : ApiController
    {
        private QuestionnaireContext db = new QuestionnaireContext();

        // GET api/Choice
        public IEnumerable<Choice> GetChoices()
        {
            var choices = db.Choices.Include(c => c.Question);
            return choices.AsEnumerable();
        }

        // GET api/Choice/5
        public Choice GetChoice(int id)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return choice;
        }

        // PUT api/Choice/5
        public HttpResponseMessage PutChoice(int id, Choice choice)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != choice.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(choice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Choice
        public HttpResponseMessage PostChoice(Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, choice);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = choice.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Choice/5
        public HttpResponseMessage DeleteChoice(int id)
        {
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Choices.Remove(choice);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, choice);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}