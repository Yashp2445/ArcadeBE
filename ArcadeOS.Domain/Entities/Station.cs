using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeOS.Domain.Entities
{
    public class Station
    {
        public long Id { get; set; }

        public long StationTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public string CreatedAt { get; set; } = string.Empty;
    }
}
