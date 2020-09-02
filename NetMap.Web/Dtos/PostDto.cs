using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMap.Web.Dtos
{
    public class PostDto
    {
        public long id { get; set; }
        public string title { get; set; }
        public string redirect_url { get; set; }
        public string? preview { get; set; }
        public long views { get; set; }
        public string host { get; set; }
        public DateTime pubDate { get; set; }
        public CategoryDto category { get; set; }
        public UserDto author { get; set; }
    }
}
