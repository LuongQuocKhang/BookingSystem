using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Promotion.Domain.Common;

public abstract class EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
}