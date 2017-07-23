using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace z_quiz.api.Models
{
    public partial class TesterQuestion
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TesterId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionId { get; set; }

        public int? AnswerId { get; set; }

        public virtual Choice Choice { get; set; }

        public virtual Question Question { get; set; }

        public virtual Tester Tester { get; set; }
    }
}