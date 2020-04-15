using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerDiplom.Models
{
    public class LessonInfoModel
    {
        [Key]
        public int LessonInfoId { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public string LessName { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public int AuditionNum { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public int DayWeek { get; set; }
        public string WeekDateNumbers { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Пропущено поле!")]
        public DateTime EndTime { get; set; }
    }
}
