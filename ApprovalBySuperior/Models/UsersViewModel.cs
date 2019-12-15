using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApprovalBySuperior.Models
{
    public class UsersViewModel
    {
        public UsersViewModel()
        {
        }

        public IEnumerable<UsersViewModel> _Users { get; set; }

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

        [MaxLength(45)]
        [DisplayName("役職")]
        public string Positionname { get; set; }

        [MaxLength(45)]
        [DisplayName("部署")]
        public string Depname { get; set; }

        [MaxLength(45)]
        [DisplayName("課班")]
        public string Groupname { get; set; }
    }
}
