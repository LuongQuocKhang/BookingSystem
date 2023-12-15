using BookingSystem.Stay.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingSystem.Stay.Domain.Entities
{
    [Table("StayWishLists", Schema = "Stay")]
    public class StayWishList : EntityBase
    {
        public int StaysId { get; set; }
        public int UserId { get; set; }
    }
}
