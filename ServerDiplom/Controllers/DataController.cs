using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerDiplom.Models;
using ServerDiplom.Models.DataModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerDiplom.Controllers
{
    public class DataController : Controller
    {
        private LessonsDataBase LessonsDb;
        private IEnumerable<IGrouping<int, LessonModel>> CurrentLessons;
        public DataController(LessonContext context)
        {
            LessonsDb = new LessonsDataBase(context);
        }

        public IActionResult Shedule(int? groupid)
        {
            ViewBag.Groups = LessonsDb.GetGroupsGroupedByFac();

            if (groupid != null && groupid != 0)
            {
                CurrentLessons = LessonsDb.GetWeekSortedLessonsById((int)groupid);
                return View(CurrentLessons);
            }
            return View();
        }

        public IActionResult CheckData()
        {
            ViewBag.Teachers = LessonsDb.data.Teachers;
            ViewBag.Groups = LessonsDb.data.Groups;
            return View(LessonsDb.AllLessons);
        }


        [HttpPost]
        public IActionResult AddGroup(GroupModel group)
        {
            if (ModelState.IsValid)
            {
                LessonsDb.data.Groups.Add(group);
                LessonsDb.data.SaveChanges();
                return View();
            }
            return View(group);
        }

        public IActionResult AddGroup()
        {
            return View();
        }

        public IActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTeacher(TeacherModel teacher)
        {
            if (ModelState.IsValid)
            {
                LessonsDb.data.Teachers.Add(teacher);
                LessonsDb.data.SaveChanges();
                return View();
            }
            return View(teacher);
        }
        [HttpPost]
        public IActionResult AddLesson(LessonInfoModel info, int groupid, int teachid)
        {
            ViewBag.Teachers = LessonsDb.data.Teachers;
            ViewBag.Groups = LessonsDb.GetGroupsGroupedByFac();
            if (ModelState.IsValid)
            {
                LessonModel lesson = new LessonModel()
                {
                    LessonInfo = info,
                    GroupId = groupid,
                    TeacherId = teachid,
                };
                LessonsDb.data.Lessons.Add(lesson);
                LessonsDb.data.SaveChanges();
                return View();
            }
            return View(info);
        }

        public IActionResult AddLesson()
        {
            ViewBag.Teachers = LessonsDb.data.Teachers;
            ViewBag.Groups = LessonsDb.GetGroupsGroupedByFac();
            return View();
        }
    }
}
