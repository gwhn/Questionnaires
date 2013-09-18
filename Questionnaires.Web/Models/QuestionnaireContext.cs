using System.Data.Entity;

namespace Questionnaires.Web.Models
{
    public class QuestionnaireContext : DbContext
    {
        public QuestionnaireContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}