using System;
using System.Collections.Generic;

namespace ApiBackend.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        public int UsersId { get; set; }
        public string TextComment { get; set; } = null!;
        public byte[] CreatedAt { get; set; } = null!;

        public virtual Review Review { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
