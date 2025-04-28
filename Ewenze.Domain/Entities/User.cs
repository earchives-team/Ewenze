using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Domain.Entities
{
    [Table("wpu0_users")]
    public class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("user_login")]
        [Required]
        [MaxLength(60)]
        public string LoginName { get; set; }

        [Column("user_pass")]
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Column("user_nicename")]
        [Required]
        [MaxLength(50)]
        public string NiceName { get; set; }

        [Column("user_email")]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("user_url")]
        [Required]
        [MaxLength(100)]
        public string Url { get; set; }

        [Column("user_registered")]
        [Required]
        public DateTime RegisteredDate { get; set; }

        [Column("user_activation_key")]
        [Required]
        [MaxLength(255)]
        public string ActivationKey { get; set; }

        [Column("user_status")]
        [Required]
        public int UserStatus { get; set; }

        [Column("display_name")]
        [Required]
        [MaxLength(250)]
        public string DisplayName { get; set; }
    }
}
