using System;
using System.Collections.Generic;
using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public virtual DbSet<Country> Countries { get; set; }
		public virtual DbSet<HighSchool> HighSchools { get; set; }
		public virtual DbSet<Student> Students { get; set; }
		public virtual DbSet<InformationOfApplied> InformationOfApplieds { get; set; }
		public virtual DbSet<School> Schools { get; set; }
		public virtual DbSet<Major> Majors { get; set; }
		public virtual DbSet<Media> Medias { get; set; }
		public virtual DbSet<Article> Articles { get; set; }
		public virtual DbSet<Result> Results { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<Post> Posts { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<Group> Groups { get; set; }
		public virtual DbSet<PostMedia> PostMedias { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Relative> Relatives { get; set; }
        public virtual DbSet<StudentMedia> StudentMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Country>().ToTable("Countries");

			//Seed to Countries
			string countriesJson = System.IO.File.ReadAllText("countries.json");
			List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

			foreach (Country country in countries)
				modelBuilder.Entity<Country>().HasData(country);

			

			// Seed to Article
			string articleJson = System.IO.File.ReadAllText("Article.json");
			List<Article>? articles = System.Text.Json.JsonSerializer.Deserialize<List<Article>>(articleJson);
			foreach(Article article in articles)
				modelBuilder.Entity<Article>().HasData(article);

			//Seed to Media
			string mediaJson = System.IO.File.ReadAllText("Media.json");
			List<Media>? medias = System.Text.Json.JsonSerializer.Deserialize<List<Media>>(mediaJson);

			foreach(Media media in medias)
				modelBuilder.Entity<Media>().HasData(media);

			//Seed to HighSchool
			string highschoolJson = System.IO.File.ReadAllText("HighSchool.json");
			List<HighSchool>? highSchools = System.Text.Json.JsonSerializer.Deserialize<List<HighSchool>>(highschoolJson);
			foreach(HighSchool highSchool in highSchools)
				modelBuilder.Entity<HighSchool>().HasData(highSchool);

			// Seed to Group
			string groupJson = System.IO.File.ReadAllText("Group.json");
			List<Group>? groups = System.Text.Json.JsonSerializer.Deserialize<List<Group>>(groupJson);
			foreach(Group group in groups)
				modelBuilder.Entity<Group>().HasData(group);


			

			// Cấu hình mối quan hệ giữa Comment và Student
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Student)  // Comment có một Student
				.WithMany(s => s.Comments)  // Student có nhiều Comment
				.HasForeignKey(c => c.AuthorID)  // Khóa ngoại trong Comment
				.HasPrincipalKey(s => s.StudentID)  // Khóa chính trong Student
				.OnDelete(DeleteBehavior.NoAction);  // Cấu hình hành động xóa

			modelBuilder.Entity<Student>()
				.HasOne(s => s.ApplicationUser)
				.WithOne()
				.HasForeignKey<Student>(s => s.Id);

			modelBuilder.Entity<PostMedia>()
				.HasKey(pm => new { pm.PostID, pm.MediaID });
            modelBuilder.Entity<StudentMedia>()
                .HasKey(sm => new { sm.StudentID, sm.StudentMediaID });
            modelBuilder.Entity<Student>()
				.HasOne(s => s.Country)
				.WithMany(c => c.Students)
				.HasForeignKey(s => s.CountryID)
				.OnDelete(DeleteBehavior.NoAction); // Hoặc DeleteBehavior.Restrict

        }

    }
}
