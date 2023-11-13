using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Folder
    {
        public int FolderId { get; set; }
        public string UserId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
