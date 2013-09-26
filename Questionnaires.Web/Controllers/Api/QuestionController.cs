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
    public class QuestionController : ApiController
    {
        private QuestionnaireContext db = new QuestionnaireContext();

        // GET api/Question
        public IEnumerable<Question> GetQuestions()
        {
            var questions = db.Questions.Include(q => q.Questionnaire);
            return questions.AsEnumerable();
        }

        // GET api/Question/5
        public Question GetQuestion(int id)
        {
            Question question = db.Questions
                .Include("Choices")
                .SingleOrDefault(x => x.Id == id);
            if (question == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return new Question
                {
                    Id = question.Id,
                    Text = question.Text,
                    QuestionnaireId = question.QuestionnaireId,
                    Choices = question.Choices.Select(x => new Choice
                        {
                            Id = x.Id,
                            Text = x.Text,
                            QuestionId = x.QuestionId
                        }).ToList()
                };
        }

        // PUT api/Question/5
        public HttpResponseMessage PutQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != question.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(question).State = EntityState.Modified;

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

        // POST api/Question
        public HttpResponseMessage PostQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, question);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = question.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Question/5
        public HttpResponseMessage DeleteQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Questions.Remove(question);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, question);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}