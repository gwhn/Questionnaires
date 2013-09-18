using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaires.Web.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string SessionId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public Question Question { get; set; }

        [Required]
        public int ChoiceId { get; set; }

        public Choice Choice { get; set; }
    }
}