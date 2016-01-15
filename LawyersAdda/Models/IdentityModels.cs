﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LawyersAdda.Entities;
using System.ComponentModel.DataAnnotations;
using System;

namespace LawyersAdda.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        [Required]
        public bool isLawyer { get; set; }
        public string FullName { get; set; }
        public string ProfilePicURL { get; set; }
        //public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CityId { get; set; }
        public virtual City City { get; set; }
        public virtual Lawyer Lawyer { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Lawyer> Lawyers { get; set; }

        public virtual DbSet<Court> Courts { get; set; }

        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
        public virtual DbSet<LawyerImage> LawyerImages { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }

        public System.Data.Entity.DbSet<LawyersAdda.Entities.City> Cities { get; set; }

       
    }
}