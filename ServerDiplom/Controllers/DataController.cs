﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using ServerDiplom.Models;
using ServerDiplom.Models.DataModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerDiplom.Controllers
{
    public class DataController : Controller
    {
        private LessonsDataBase LessonsDb;

        public DataController(LessonContext context)
        {
            LessonsDb = new LessonsDataBase(context);
        }

        public IActionResult Shedule(int? groupid)
        {
            IEnumerable<IGrouping<int, LessonModel>> CurrentLessons;
            ViewBag.Groups = LessonsDb.GetGroupsGroupedByFac();

            if (groupid != null && groupid != 0)
            {
                CurrentLessons = LessonsDb.GetWeekSortedLessonsByIdGroup((int)groupid);
                return View(CurrentLessons);
            }
            return View();
        }

        public IActionResult SheduleTeacher(int? teacherid)
        {
            IEnumerable<IGrouping<int, LessonModel>> lessons;
            ViewBag.Teachers = LessonsDb.data.Teachers as IEnumerable<TeacherModel>;
            
            if (teacherid != null && teacherid != 0)
            {
                lessons = LessonsDb.GetWeekSortedLessonsByIdTeacher((int)teacherid);
                return View(lessons);
            }
            return View();
        }

        [Authorize]
        public IActionResult CheckData()
        {
            ViewBag.Teachers = LessonsDb.data.Teachers;
            ViewBag.Groups = LessonsDb.data.Groups;
            ViewBag.Informations = LessonsDb.data.Informations;
            return View(LessonsDb.AllLessons);
        }
        [Authorize]
        public IActionResult AddGroup()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddGroup(GroupModel group)
        {
            if (ModelState.IsValid)
            {
                GroupModel _ = LessonsDb.data.Groups.FirstOrDefault(g => g.TeachProfile == group.TeachProfile);
                if (_ == null)
                {
                    LessonsDb.data.Groups.Add(group);
                    LessonsDb.data.SaveChanges();
                    return View();
                }
                ModelState.AddModelError("", "Группа с таким именем уже существует!");
            }
            return View(group);
        }
        [Authorize]
        public IActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddTeacher(TeacherModel teacher)
        {
            if (ModelState.IsValid)
            {
                TeacherModel _ = LessonsDb.data.Teachers
                    .FirstOrDefault(t =>
                    t.FirstName == teacher.FirstName &&
                    t.MiddleName == teacher.MiddleName &&
                    t.LastName == teacher.LastName);
                if (_ == null)
                {
                    LessonsDb.data.Teachers.Add(teacher);
                    LessonsDb.data.SaveChanges();
                    return View();
                }
                ModelState.AddModelError("", "Такой преподаватель уже существует!");
            }
            return View(teacher);
        }

        [Authorize]
        public IActionResult AddLesson()
        {
            ViewBag.Teachers = LessonsDb.data.Teachers;
            ViewBag.Groups = LessonsDb.GetGroupsGroupedByFac();
            return View();
        }

        [HttpPost]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        public IActionResult DeleteGroup(int id)
        {
            var lesson = LessonsDb.data.Groups.FirstOrDefault(l => l.GroupId == id);
            if (lesson != null)
            {
                LessonsDb.data.Groups.Remove(lesson);
                LessonsDb.data.SaveChanges();
            }
            return RedirectToAction("CheckData", "Data");
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = LessonsDb.data.Teachers.FirstOrDefault(l => l.TeacherId == id);
            if (teacher != null)
            {
                LessonsDb.data.Teachers.Remove(teacher);
                LessonsDb.data.SaveChanges();
            }
            return RedirectToAction("CheckData", "Data");
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteInformation(int id)
        {
            var info = LessonsDb.data.Informations.FirstOrDefault(l => l.LessonInfoId == id);
            if (info != null)
            {
                LessonsDb.data.Informations.Remove(info);
                LessonsDb.data.SaveChanges();
            }
            return RedirectToAction("CheckData", "Data");
        }

        [HttpGet]
        public string GetSheduleById(int id, string type)
        {
            IEnumerable<IGrouping<int, LessonModel>> result;

            switch (type)
            {
                case ("teacher"):
                    result = LessonsDb.GetWeekSortedLessonsByIdTeacher(id);
                    break;
                case ("student"):
                    result = LessonsDb.GetWeekSortedLessonsByIdGroup(id);
                    break;

                default:
                    return "not found";
            }
            return JsonConvert.SerializeObject(result);

        }

        [HttpGet]
        public string GetAllGroups()
        {
            return JsonConvert.SerializeObject(LessonsDb.data.Groups);
        }

        [HttpGet]
        public string GetAllTeachers()
        {
            return JsonConvert.SerializeObject(LessonsDb.data.Teachers);
        }

        public FileResult DownloadFile()
        {
            IFileProvider provider = new PhysicalFileProvider("C:/inetpub/vhosts/u1018142.plsk.regruhosting.ru/httpdocs/mysibsu.ru/wwwroot/Files");
            IFileInfo fileInfo = provider.GetFileInfo("MySibgu.apk");
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.android.package-archive";
            return File(readStream, mimeType, "MySibgu.apk");
        }
    }
}
