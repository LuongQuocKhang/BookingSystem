using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities
{
    [Table("StayWishLists", Schema = "Stay")]
    public class StayWishListEntity : EntityBase
    {
        public int UserId { get; set; }
        public int StayId { get; set; }
    }
}
