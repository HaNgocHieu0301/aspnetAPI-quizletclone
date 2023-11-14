using System;
using System.Collections.Generic;

namespace BussinessObject.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string UserId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
