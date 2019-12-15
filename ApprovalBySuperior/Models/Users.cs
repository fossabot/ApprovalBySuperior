using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovalBySuperior.Models
{
    public class Users
    {
        public Users()
        {
        }
        // Primary key
        [Key]
        [MaxLength(10)]
        [Required]
        [DisplayName("ユーザID")]
        public string Userid { get; set; }

        [MaxLength(30)]
        [Required]
        [DisplayName("氏名")]
        public string Username { get; set; }

        [MaxLength(30)]
        [Required]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        //役職、部署、課班はPostions、Departmentsテーブルからクエリで参照し
        //レコードを都度追加する。
        [MaxLength(30)]
        [Required]
        [DisplayName("役職")]
        public string Users_position { get; set; }

        [MaxLength(30)]
        [Required]
        [DisplayName("部署")]
        public string Users_depname { get; set; }

        [MaxLength(30)]
        [Required]
        [DisplayName("課班")]
        public string Users_depgroupname { get; set; }

        // Navigation Properties
        //外部キー制約を設定すると、Users側からのデータ挿入ができない（DepとPosiテーブルの主キーが
        //自動採番なので、意図したデータが挿入されない。かつ、DepとPosiテーブルに意図しないれこーどが追加される）
        //public Departments Departments { get; set; }
        //public Positions Positions { get; set; }

    }
}
