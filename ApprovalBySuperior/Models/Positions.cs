using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApprovalBySuperior.Models
{
    public class Positions
    {
        public Positions()
        {
        }

        [Key]
        [DisplayName("役職ID")]
        public int Positionid { get; set; }

        [MaxLength(45)]
        [DisplayName("役職")]
        public string Positionname { get; set; }

        //外部キー制約は不要
        //Navigation properties
        //public virtual ICollection<Users> Users { get; set; }

    }
}
