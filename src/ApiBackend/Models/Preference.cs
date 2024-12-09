using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Preference
    {
        public int PrefId { get; set; }
        public int UsersId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
