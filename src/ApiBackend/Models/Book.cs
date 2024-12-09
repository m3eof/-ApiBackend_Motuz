using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Book
    {
        public Book()
        {
            Exchanges = new HashSet<Exchange>();
            Favorites = new HashSet<Favorite>();
            Reviews = new HashSet<Review>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public int PublishedYear { get; set; }
        public int GenreId { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual ICollection<Exchange> Exchanges { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
