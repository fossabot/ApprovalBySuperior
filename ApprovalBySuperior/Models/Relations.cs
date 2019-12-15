using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ApprovalBySuperior.Models
{
    public class Relations
    {
        public Relations()
        {
        }

        //Navigation properties
        [ForeignKey("Userid")]
        public virtual Users Users { get; set; }
        [ForeignKey("Superiorid")]
        public virtual Superiors Superiors { get; set; }

    }
}
