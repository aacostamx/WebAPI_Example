using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class User
    {
        public User()
        {
            Hierarchies = new HashSet<Hierarchy>();
            Tokens = new HashSet<Token>();
        }

        public int Id { get; set; }
        public int? RolId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MotherName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool? Active { get; set; }

        public Rol Rol { get; set; }
        public Student Student { get; set; }
        public ICollection<Hierarchy> Hierarchies { get; set; }
        public ICollection<Token> Tokens { get; set; }
    }
}
