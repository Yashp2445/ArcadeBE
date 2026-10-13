using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeOS.Domain.Entities
{
    public class StationRate
    {
        public long Id { get; set; }

        public long StationTypeId { get; set; }

        public int? ControllerCount { get; set; }

        public long HourlyRatePaise { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
