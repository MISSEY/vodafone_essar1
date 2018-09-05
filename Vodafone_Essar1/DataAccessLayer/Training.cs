using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Vodafone_Essar1.Models;
namespace Vodafone_Essar1.Data_Access_Layer
{
    public class Training:DbContext

    {
        public Training()
            : base("Training")
        {
        }
   
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
        public DbSet<Mobile_numbers> MobileNumberLists { get; set; }
        public DbSet<Sim_Numbers> SimNumberLists { get; set; }
        public DbSet<Email_Fromat> Email_Format_Lists { get; set; }
        public DbSet<Plans> Plans_Lists { get; set; }
        public DbSet<Billed_to> Billed_To_Lists { get; set; }
        public DbSet<Essar_Users_Bill> Essar_users_bill { get; set; }
        public DbSet<Reports> Reports_Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>().ToTable("Essar_users1");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserCredential>().ToTable("Essar_User_credentials");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Mobile_numbers>().ToTable("Essar_users_Mobile_no");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sim_Numbers>().ToTable("Essar_users_Sim_Numbers");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Email_Fromat>().ToTable("Essar_users_Email_Temp");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plans>().ToTable("Essar_users_Plans");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Billed_to>().ToTable("Essar_users_Billing_Sheet");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Essar_Users_Bill>().ToTable("Essar_Users_Bill");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reports>().ToTable("Essar_users_Logs");
            base.OnModelCreating(modelBuilder);

        }
        
       

    }
}