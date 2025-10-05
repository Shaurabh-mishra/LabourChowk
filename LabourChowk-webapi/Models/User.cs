using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace LabourChowk_webapi.Models
{


    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(50)]
        public string Role { get; set; }   // "Worker", "WorkPoster", "Admin"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}