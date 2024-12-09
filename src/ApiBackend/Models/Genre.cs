using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
            Preferences = new HashSet<Preference>();
        }

        public int GenreId { get; set; }
        public string NameOfGenre { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Preference> Preferences { get; set; }
    }
}
