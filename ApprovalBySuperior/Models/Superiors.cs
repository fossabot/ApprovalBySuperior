using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ApprovalBySuperior.Models
{
    public class Superiors
    {
        public Superiors()
        {
        }

        [Key]
        public string Superiorid { get; set; }
        public string UseridRef { get; set; }


        // Navigation Properties
        [ForeignKey("Userid")]
        public virtual Users Users { get; set; }
        public virtual ICollection<Relations> Relations { get; set; }

    }
}
