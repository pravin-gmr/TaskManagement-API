using API.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("TaskDetail", Schema = "dbo")]
    public class TaskDetail : BaseColumn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TaskId")]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Title", TypeName = "nvarchar(50)")]
        public required string Title { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("Description", TypeName = "nvarchar(500)")]
        public required string Description { get; set; }

        [Required]
        [Column("DueDate", TypeName = "date")]
        public required DateTime DueDate { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Status", TypeName = "varchar(20)")]
        public required string Status { get; set; }
    }
}
