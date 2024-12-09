using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Exchange
    {
        public int ExchangeId { get; set; }
        public int BookId { get; set; }
        public int OwnerId { get; set; }
        public int SeekerId { get; set; }
        public string StatusOfExchange { get; set; } = null!;
        public byte[] CreatedAt { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
        public virtual User Owner { get; set; } = null!;
        public virtual User Seeker { get; set; } = null!;
    }
}
