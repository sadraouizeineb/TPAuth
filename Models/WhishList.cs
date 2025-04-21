using System;
using System.Collections.Generic;

namespace tpAuth.Models
{
    public class WhishList
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public Guid MovieId { get; set; }
        public ApplicationUser? User { get; set; }  // Relation avec l'utilisateur

        // Relation avec Movie (un seul film)
        public Movie Movie { get; set; } 
    }
}
