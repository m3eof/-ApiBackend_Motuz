using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            ExchangeOwners = new HashSet<Exchange>();
            ExchangeSeekers = new HashSet<Exchange>();
            Favorites = new HashSet<Favorite>();
            Preferences = new HashSet<Preference>();
            ReviewsNavigation = new HashSet<Review>();
            Saveds = new HashSet<Saved>();
            Wishlists = new HashSet<Wishlist>();
            Followers = new HashSet<User>();
            Followings = new HashSet<User>();
            Reviews = new HashSet<Review>();
            Users = new HashSet<User>();
            UsersNavigation = new HashSet<User>();
        }

        public int UsersId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public int? FollowerNumber { get; set; }
        public int? FollowingNumber { get; set; }
        public byte[] CreatedAt { get; set; } = null!;

        public bool AcceptTerms { get; set; }
        public Role Role { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? Verified { get; set;  }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public string? ResetToken {  get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }


        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Exchange> ExchangeOwners { get; set; }
        public virtual ICollection<Exchange> ExchangeSeekers { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Preference> Preferences { get; set; }
        public virtual ICollection<Review> ReviewsNavigation { get; set; }
        public virtual ICollection<Saved> Saveds { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }

        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Followings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<User> UsersNavigation { get; set; }
    }
}
