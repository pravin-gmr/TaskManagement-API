using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Base
{
    public class BaseColumn : IBaseColumn
    {
        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        [Column("SavedOn")]
        public DateTime SavedOn { get; set; } = DateTime.Now;

        [Column("SavedBy")]
        public long? SavedBy { get; set; }

        [Column("ModifiedOn")]
        public DateTime? ModifiedOn { get; set; }

        [Column("ModifiedBy")]
        public long? ModifiedBy { get; set; }
    }
}
