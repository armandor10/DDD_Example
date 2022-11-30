using System;
using System.Reflection;
using hey_url_challenge_code_dotnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace hey_url_challenge_code_dotnet.Infra.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<VisitsBinnacle> VisitsBinnacle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var urlId = new Guid("348a325a-993e-45e7-862c-e7e319530cfa");
            var visitId = new Guid("348a325a-993e-45e7-862c-e7e319530cfb");
            modelBuilder.Entity<Url>()
                         .HasData(
                            new {
                                Id = urlId,
                                ShortUrl = "HRTHG",
                                OriginalUrl = "https://www.linkedin.com/in/diego-prens/",
                                CreatedOn = DateTime.Now,
                                UpdateOn = DateTime.Now
                            }
                          );

            modelBuilder.Entity<Visits>()
                        .HasData(
                            new
                            {
                                Id = visitId,
                                UrlId = urlId,
                                VisitDay = DateTime.Now.AddDays(-1),
                                Counter = 2,
                                CreatedOn = DateTime.Now,
                                UpdateOn = DateTime.Now
                            }
                        );

            modelBuilder.Entity<VisitsBinnacle>()
                        .HasData(
                            new[] {
                                new
                                {
                                    Id = new Guid("348a325a-993e-45e7-862c-e7e319530cfc"),
                                    VisiId = visitId,
                                    Browser = "Chrome",
                                    OS = "Windows",
                                    CreatedOn = DateTime.Now,
                                    UpdateOn = DateTime.Now
                                },
                                new
                                {
                                    Id = new Guid("348a325a-993e-45e7-862c-e7e319530cfd"),
                                    VisiId = visitId,
                                    Browser = "Chrome",
                                    OS = "Ubuntu",
                                    CreatedOn = DateTime.Now,
                                    UpdateOn = DateTime.Now
                                }
                            }
                        );
        }
    }
}

