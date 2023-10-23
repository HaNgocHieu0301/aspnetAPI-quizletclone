using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public string? Image { get; set; }
        public string Definition { get; set; } = null!;
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; } = null!;
    }
}
