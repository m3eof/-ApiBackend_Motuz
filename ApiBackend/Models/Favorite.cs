using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Favorite
    {
        public int FavId { get; set; }
        public int UsersId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
