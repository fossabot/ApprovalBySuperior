using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ApprovalBySuperior.Models
{
    public class Departments
    {
        public Departments()
        {
        }

        [Key]
        public int Depid { get; set; }

        [MaxLength(45)]
        [DisplayName("部署")]
        public string Depname { get; set; }
        [MaxLength(45)]
        [DisplayName("課班")]
        public string Groupname { get; set; }

        //外部キー制約は不要
        //Navigation properties
        //public virtual ICollection<Users> Users { get; set; } 

    }
}
