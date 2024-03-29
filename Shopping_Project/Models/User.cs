﻿namespace Shopping_Project.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public Guid RoleId { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<BIll> Bills { get;set; }
        public virtual Role Role { get; set; }

    }
}
