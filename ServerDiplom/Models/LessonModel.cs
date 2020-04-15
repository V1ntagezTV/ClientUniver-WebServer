using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerDiplom.Models
{
    public class LessonModel
    {
        [Key]
        public int LessonId { get; set; }

        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        [Required]
        public GroupModel Group { get; set; }

        [ForeignKey("TeacherId")]
        public int TeacherId { get; set; }
        [Required]
        public TeacherModel Teacher { get; set; }

        [ForeignKey("LessonInfoId")]
        public int LessonInfoId { get; set; }
        [Required]
        public LessonInfoModel LessonInfo { get; set; }
    }
}
