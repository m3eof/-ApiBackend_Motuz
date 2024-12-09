using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Wishlist
    {
        public int WishlistId { get; set; }
        public int UsersId { get; set; }
        public int BookId { get; set; }
        public byte[] CreatedAt { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
