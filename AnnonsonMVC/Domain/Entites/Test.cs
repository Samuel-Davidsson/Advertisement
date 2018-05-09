using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class Test
    {
        public int TestId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageOriginalUrl { get; set; }
        public string ImageSmallUrl { get; set; }
        public string ImageMediumUrl { get; set; }
        public string ImageLargeUrl { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }
}
