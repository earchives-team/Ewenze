using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ewenze.Domain.Entities
{
    [Table("wpu0_usermeta")]
    public class UserMeta
    {
        [Key]
        [Column("umeta_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("meta_key")]
        public string MetaKey { get; set; }

        [Column("meta_value")]
        public string? MetaValue { get; set; }

    }
}
