using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Swimlane.Core.Entities
{
    [Table("Tests")]
    public class Test : BaseEntity
    {
        [Key]
        [Column("Test_id")]
        public Guid Id { get; set; }
        [Column("Test_name")]
        public string TestName { get; set; } = string.Empty;

    }
}
