using System;
using System.Collections.Generic;
using System.Text;

namespace NetMap.Data.Models
{
    public class ImageViewModel
    {
        public IEnumerable<Image> images { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
