using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Stay.Application.Dtos.Stay
{
    public class HostDto
    {
        public int Id { get; set; }

        public int StayId { get; set; }

        public string? Name { get; set; } = string.Empty;

        public int TotalPlace { get; set; }

        public float ResponeRate { get; set; }

        public string? Note { get; set; }
    }
}
