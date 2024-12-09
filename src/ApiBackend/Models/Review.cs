using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Review
    {
        public Review()
        {
            Comments = new HashSet<Comment>();
            Saveds = new HashSet<Saved>();
            Users = new HashSet<User>();
        }

        public int ReviewId { get; set; }
        public int UsersId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public int? LikesCount { get; set; }
        public byte[] CreatedAt { get; set; } = null!;

        public virtual Book Book { get; set; } = null!;
        public virtual User UsersNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Saved> Saveds { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
