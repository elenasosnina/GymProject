using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GymProject.Infrastructure
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<ClientEntity> Client { get; set; }
        public virtual DbSet<DiscountEntity> Discount { get; set; }
        public virtual DbSet<EmployeeEntity> Employee { get; set; }
        public virtual DbSet<GymEntity> Gym { get; set; }
        public virtual DbSet<HallEntity> Hall { get; set; }
        public virtual DbSet<LessonEntity> Lesson { get; set; }
        public virtual DbSet<LessonProgramEntity> Lesson_programs { get; set; }
        public virtual DbSet<PositionEntity> Position { get; set; }
        public virtual DbSet<ProductEntity> Product { get; set; }
        public virtual DbSet<ProductCategoryEntity> Product_category { get; set; }
        public virtual DbSet<StatusEntity> Status { get; set; }
        public virtual DbSet<SubscriptionEntity> Subscription { get; set; }
        public virtual DbSet<SubscriptionTypeEntity> Subscription_type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientEntity>()
                .Property(e => e.date_of_birth)
                .HasPrecision(53, 0);

            modelBuilder.Entity<ClientEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ID_client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiscountEntity>()
                .Property(e => e.value)
                .HasPrecision(53, 0);

            modelBuilder.Entity<DiscountEntity>()
                .HasMany(e => e.Client)
                .WithRequired(e => e.Discount)
                .HasForeignKey(e => e.ID_discount)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeEntity>()
                .Property(e => e.date_of_birth)
                .HasPrecision(53, 0);

            modelBuilder.Entity<EmployeeEntity>()
                .Property(e => e.gender)
                .HasPrecision(53, 0);

            modelBuilder.Entity<EmployeeEntity>()
                .Property(e => e.length_of_service)
                .HasPrecision(53, 0);

            modelBuilder.Entity<GymEntity>()
                .Property(e => e.start_time)
                .HasPrecision(53, 0);

            modelBuilder.Entity<GymEntity>()
                .Property(e => e.end_time)
                .HasPrecision(53, 0);

            modelBuilder.Entity<GymEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Gym)
                .HasForeignKey(e => e.ID_gym)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HallEntity>()
                .Property(e => e.hall_number)
                .HasPrecision(53, 0);

            modelBuilder.Entity<HallEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Hall)
                .HasForeignKey(e => e.ID_hall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LessonProgramEntity>()
                .Property(e => e.program_duration)
                .HasPrecision(53, 0);

            modelBuilder.Entity<LessonProgramEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Lesson_programs)
                .HasForeignKey(e => e.ID_program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PositionEntity>()
                .Property(e => e.salary)
                .HasPrecision(53, 0);

            modelBuilder.Entity<PositionEntity>()
                .Property(e => e.work_schedule)
                .HasPrecision(53, 0);

            modelBuilder.Entity<PositionEntity>()
                .HasMany(e => e.Employee)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.ID_position)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PositionEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.ID_trainer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PositionEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.ID_trainer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductEntity>()
                .Property(e => e.cost)
                .HasPrecision(53, 0);

            modelBuilder.Entity<ProductEntity>()
                .Property(e => e.quantity)
                .HasPrecision(53, 0);

            modelBuilder.Entity<ProductEntity>()
                .Property(e => e.expiration_date)
                .HasPrecision(53, 0);

            modelBuilder.Entity<ProductCategoryEntity>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Product_category)
                .HasForeignKey(e => e.ID_product_category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StatusEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.ID_status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubscriptionEntity>()
                .Property(e => e.validity_start_date)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionEntity>()
                .Property(e => e.validity_expiration_date)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionEntity>()
                .HasMany(e => e.Lesson)
                .WithRequired(e => e.Subscription)
                .HasForeignKey(e => e.ID_subscription)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .Property(e => e.cost)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .Property(e => e.duration)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .Property(e => e.number_of_classes)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .Property(e => e.date_and_time_of_purchase)
                .HasPrecision(53, 0);

            modelBuilder.Entity<SubscriptionTypeEntity>()
                .HasMany(e => e.Subscription)
                .WithRequired(e => e.Subscription_type)
                .HasForeignKey(e => e.ID_subscription_type)
                .WillCascadeOnDelete(false);
        }
    }
}
