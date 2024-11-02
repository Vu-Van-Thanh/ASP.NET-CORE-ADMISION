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
		public virtual DbSet<Person> Persons { get; set; }
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Country>().ToTable("Countries");
			modelBuilder.Entity<Person>().ToTable("Persons");

			//Seed to Countries
			string countriesJson = System.IO.File.ReadAllText("countries.json");
			List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

			foreach (Country country in countries)
				modelBuilder.Entity<Country>().HasData(country);

			
			//Seed to Persons
			string personsJson = System.IO.File.ReadAllText("persons.json");
			List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

			foreach (Person person in persons)
				modelBuilder.Entity<Person>().HasData(person);

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

			//Fluent API
			modelBuilder.Entity<Person>().Property(temp => temp.TIN)
			  .HasColumnName("TaxIdentificationNumber")
			  .HasColumnType("varchar(8)")
			  .HasDefaultValue("ABC12345");

			//modelBuilder.Entity<Person>()
			//  .HasIndex(temp => temp.TIN).IsUnique();

			modelBuilder.Entity<Person>()
			  .HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8");

			//Table Relations
			modelBuilder.Entity<Person>(entity =>
			{
				entity.HasOne<Country>(c => c.Country)
		  .WithMany(p => p.Persons)
		  .HasForeignKey(p => p.CountryID);
			});

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


		}

		public List<Person> sp_GetAllPersons()
		{
			return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
		}

		public int sp_InsertPerson(Person person)
		{
			SqlParameter[] parameters = new SqlParameter[] {
		new SqlParameter("@PersonID", person.PersonID),
		new SqlParameter("@PersonName", person.PersonName),
		new SqlParameter("@Email", person.Email),
		new SqlParameter("@DateOfBirth", person.DateOfBirth),
		new SqlParameter("@Gender", person.Gender),
		new SqlParameter("@CountryID", person.CountryID),
		new SqlParameter("@Address", person.Address),
		new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)
			};

			return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetters", parameters);
		}
	}
}
