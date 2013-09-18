using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaires.Web.Models
{
    public class Choice
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public Question Question { get; set; }
    }
}