using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            Questions = new HashSet<Question>();
        }

        public int LessonId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int VisibleId { get; set; }
        public int? FolderId { get; set; }
        public double? Rate { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
