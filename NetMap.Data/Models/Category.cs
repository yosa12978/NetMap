using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetMap.Data.Models
{
    public class Category
    {
        public long id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public List<Post> posts { get; set; }
    }
}
