﻿using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class LessonDTO
    {
        public int LessonId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        [Required]
        public int VisibleId { get; set; }
        public int? FolderId { get; set; }
        public double? Rate { get; set; }
        public string UserId { get; set; }
    }
}
