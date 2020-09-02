using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetMap.Web.Dtos
{
    public class PostReadDto
    {
        [Required]
        [MaxLength(75)]
        public string title { get; set; }
        [Required]
        public string address { get; set; }
        public string? preview { get; set; }
        [Required]
        public long category { get; set; }
    }
}
