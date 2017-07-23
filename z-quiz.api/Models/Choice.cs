using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace z_quiz.api.Models
{
    public partial class Choice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Choice()
        { }

        public int ChoiceId { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [JsonIgnore]
        public int Score { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}