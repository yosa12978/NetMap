using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetMap.Data.Models
{
    public class User
    {
        public long id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public bool isSubscribed { get; set; } = false;
        [Required]
        public bool isValidated { get; set; } = false;
        [Required]
        public string token { get; set; }
        [Required]
        public string emailToken { get; set; }
        [Required]
        public string role { get; set; } = Role.USER;
        [Required]
        public DateTime regDate { get; set; } = DateTime.Now;
        public List<Post> posts { get; set; }
    }
}
