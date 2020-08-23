using System;
using System.Collections.Generic;
using System.Text;

namespace NetMap.Data.Models
{
    public class PostViewModel
    {
        public IEnumerable<Post> psots { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
