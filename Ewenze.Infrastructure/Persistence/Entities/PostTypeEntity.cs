using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Ewenze.Infrastructure.Persistence.Entities
{
    public class PostTypeEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("post_title")]
        public string PostTitle { get; set; }

        [Column("post_content")]
        public string PostContent { get; set; }

        [Column("post_status")]
        public string PostStatus { get; set; }

        [Column("post_modified")]
        public DateTime PostModified { get; set; }

        [Column("post_date")]
        public DateTime PostDate { get; set; }

        [Column("post_type")]
        public string PostType { get; set; }
    }
}
