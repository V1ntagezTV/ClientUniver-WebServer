using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ServerDiplom.Models.DataModels
{
    class LessonsDataBase
    {
        public LessonContext data;
        public IEnumerable<LessonModel> AllLessons;
        public LessonsDataBase(LessonContext dbContext)
        {
            data = dbContext;
            AllLessons = dbContext.Lessons
                .Include(l => l.Group)
                .Include(l => l.Teacher)
                .Include(l => l.LessonInfo);
        }

        public IEnumerable<IGrouping<int, LessonModel>> GetWeekSortedLessonsByIdGroup(int groupId)
        {
            return AllLessons
                        .Where(l => l.Group.GroupId == groupId)
                        .GroupBy(l => l.LessonInfo.DayWeek);
        }

        public IEnumerable<IGrouping<string, GroupModel>> GetGroupsGroupedByFac()
        {
            return data.Groups
                    .ToArray()
                    .GroupBy(l => l.Faculty);
        }

        public IEnumerable<IGrouping<int, LessonModel>> GetWeekSortedLessonsByIdTeacher(int teacherid)
        {
            return AllLessons
                .Where(l => l.TeacherId == teacherid)
                .GroupBy(l => l.LessonInfo.DayWeek);
        }
    }
}
