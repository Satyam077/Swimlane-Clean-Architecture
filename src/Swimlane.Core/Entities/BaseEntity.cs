using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Swimlane.Core.Entities
{
    public abstract class BaseEntity
    {
        [Column("Created_by")]
        public Guid? CreatedBy { get; set; }
        [Column("Created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("Updated_by")]
        public Guid? UpdatedBy { get; set; }
        [Column("Updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
