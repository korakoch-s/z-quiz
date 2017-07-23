using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace z_quiz.api.Models
{
    public partial class Tester
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tester()
        {
            TesterQuestions = new HashSet<TesterQuestion>();
        }

        public int TesterId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public bool IsCompleted { get; set; }

        public int Score { get; set; }

        public int TotalScore { get; set; }

        [NotMapped]
        public int Rank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TesterQuestion> TesterQuestions { get; set; }
    }
}