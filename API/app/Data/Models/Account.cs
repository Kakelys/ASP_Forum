﻿namespace app.Data.Models
{

    public partial class Account
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? Email { get; set; }

        public DateTime? RegisterDate { get; set; }

        public string? ImagePath { get; set; }

        public virtual ICollection<Post> Posts { get; } = new List<Post>();

        public virtual Role Role { get; set; } = null!;

        public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

        public virtual ICollection<Topic> Topics { get; } = new List<Topic>();
    }
}