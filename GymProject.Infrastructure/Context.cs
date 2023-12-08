using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace GymProject.Infrastructure
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
            Database.SetInitializer<Context>(null);
        }

        public virtual DbSet<ClientEntity> Clients { get; set; }
        public virtual DbSet<DiscountEntity> Discounts { get; set; }
        public virtual DbSet<EmployeeEntity> Employees { get; set; }
        public virtual DbSet<GymEntity> Gyms { get; set; }
        public virtual DbSet<HallEntity> Halls { get; set; }
        public virtual DbSet<LessonEntity> Lessons { get; set; }
        public virtual DbSet<LessonProgramEntity> LessonPrograms { get; set; }
        public virtual DbSet<PositionEntity> Positions { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        public virtual DbSet<StatusEntity> Statuses { get; set; }
        public virtual DbSet<SubscriptionEntity> Subscriptions { get; set; }
        public virtual DbSet<SubscriptionTypeEntity> SubscriptionTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ClientEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ClientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscountEntity>()
                .Property(e => e.Value)
                .HasPrecision(53, 0);

            modelBuilder.Entity<DiscountEntity>()
                .HasMany(e => e.Client)
                .WithRequired(e => e.Discount)
                .HasForeignKey(e => e.DiscountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeEntity>()
                .Property(e => e.LengthOfService)
                .HasPrecision(53, 0);

            modelBuilder.Entity<GymEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Gym)
                .HasForeignKey(e => e.GymId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HallEntity>()
                .Property(e => e.HallNumber)
                .HasPrecision(53, 0);

            modelBuilder.Entity<HallEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Hall)
                .HasForeignKey(e => e.HallId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LessonProgramEntity>()
                .Property(e => e.ProgramDuration)
                .HasPrecision(53, 0);

            modelBuilder.Entity<LessonProgramEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Lesson_programs)
                .HasForeignKey(e => e.ProgramId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PositionEntity>()
                .Property(e => e.Salary)
                .HasPrecision(53, 0);

            modelBuilder.Entity<PositionEntity>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.PositionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PositionEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.TrainerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PositionEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.TrainerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductEntity>()
                .Property(e => e.Cost)
                .HasPrecision(53, 0);

            modelBuilder.Entity<ProductEntity>()
                .Property(e => e.Quantity)
                .HasPrecision(53, 0);

            modelBuilder.Entity<ProductCategoryEntity>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Product_category)
                .HasForeignKey(e => e.ProductCategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StatusEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubscriptionEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Subscription)
                .HasForeignKey(e => e.SubscriptionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .Property(e => e.Cost)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .Property(e => e.Duration)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .Property(e => e.NumberOfClasses)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Subscription_type)
                .HasForeignKey(e => e.SubscriptionTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
