using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Questionnaires.Web.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public List<Question> Questions { get; set; }
    }
}