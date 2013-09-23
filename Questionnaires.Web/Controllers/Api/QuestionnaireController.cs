using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Questionnaires.Web.Models;

namespace Questionnaires.Web.Controllers.Api
{
    public class QuestionnaireController : ApiController
    {
        private QuestionnaireContext db = new QuestionnaireContext();

        // GET api/Questionnaire
        public IEnumerable<Questionnaire> GetQuestionnaires()
        {
            return db.Questionnaires.AsEnumerable();
        }

        // GET api/Questionnaire/5
        public Questionnaire GetQuestionnaire(int id)
        {
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return questionnaire;
        }

        // PUT api/Questionnaire/5
        public HttpResponseMessage PutQuestionnaire(int id, Questionnaire questionnaire)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != questionnaire.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(questionnaire).State = EntityState.Modified;

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

        // POST api/Questionnaire
        public HttpResponseMessage PostQuestionnaire(Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Questionnaires.Add(questionnaire);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, questionnaire);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = questionnaire.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Questionnaire/5
        public HttpResponseMessage DeleteQuestionnaire(int id)
        {
            Questionnaire questionnaire = db.Questionnaires.Find(id);
            if (questionnaire == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Questionnaires.Remove(questionnaire);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, questionnaire);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}