using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetMap.Data.Models
{
    public class Image
    {
        public long id { get; set; }
        [Required]
        public Uri image { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public User user { get; set; }
    }
}
