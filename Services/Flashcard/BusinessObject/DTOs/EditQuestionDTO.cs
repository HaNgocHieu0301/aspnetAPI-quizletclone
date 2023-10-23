using System.ComponentModel.DataAnnotations;

namespace BusinessObject.DTOs
{
    public class EditQuestionDTO
    {
        public int LearningStatusId { get; set; }
        [Required]
        public bool IsStarred { get; set; }
        [Required]
        public string Term { get; set; }
    }
}
