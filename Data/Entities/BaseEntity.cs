using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoBar.Accounting.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }
    }
}