using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetMap.Data.Models
{
    public class Post
    {
        public long id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public Uri uri { get; set; }
        [Required]
        public string preview { get; set; }
        [Required]
        public DateTime pubDate { get; set; }
        [Required]
        public Category category { get; set; }
        [Required]
        public User author { get; set; }
    }
}
