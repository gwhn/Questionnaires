using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaires.Web.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public List<Choice> Choices { get; set; }

        [Required]
        public int QuestionnaireId { get; set; }

        public Questionnaire Questionnaire { get; set; }
    }
}