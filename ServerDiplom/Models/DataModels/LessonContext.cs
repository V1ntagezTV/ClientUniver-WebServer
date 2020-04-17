using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerDiplom.Models.DataModels
{
    public class LessonContext: DbContext
    {
        public DbSet<LessonModel> Lessons { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<LessonInfoModel> Informations { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public LessonContext(DbContextOptions<LessonContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupModel>();
            modelBuilder.Entity<TeacherModel>();
            modelBuilder.Entity<LessonInfoModel>();
            modelBuilder.Entity<UserModel>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
