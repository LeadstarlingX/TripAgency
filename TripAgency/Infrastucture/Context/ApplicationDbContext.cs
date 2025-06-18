using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.Marshalling;
using Domain.Enum;
using Domain.Entities.ApplicationEntities;

namespace Domain.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)// IdentityDbContext<User, IdentityRole<long>, long>(options)
    {

        #region Tables
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<ImageShot> ImageShots { get; set; } 
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarBooking> CarsBookings { get; set; }
        public virtual DbSet<TripBooking> TripBookings { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<TripPlan> TripPlans { get; set; }
        public virtual DbSet<TripPlanCar> TripPlanCars { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<CustomerContact> CustomersContacts { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<SEOMetaData> SEOMetaDatas { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Booking
            modelBuilder.Entity<Booking>(b =>
            {
                b.ToTable("Bookings")
                .HasKey(b => b.Id);

                b.Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                b.HasOne(c => c.Customer)
                .WithMany(b => b.Bookings)
                .HasForeignKey(c => c.CustomerId);

                b.HasOne(c => c.Employee)
                .WithMany(b => b.Bookings)
                .HasForeignKey(c => c.Employeeid);

                b.Property(b => b.CustomerId)
                .HasColumnName("customer_id");

                b.Property(b => b.Employeeid)
                .HasColumnName("employee_id")                
                .IsRequired(false);

                b.Property(b => b.StartDateTime)
                .HasColumnName("start_date_time")
                .HasColumnType("datetime2(7)")
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

                b.Property(b => b.EndDateTime)
                .HasColumnName("end_date_time")
                .HasColumnType("datetime2(7)")
                .IsRequired();

                b.Property(b => b.Status)
                .HasConversion<string>()
                .HasColumnType("nvarchar(10)")
                .HasColumnName("status")
                .IsRequired();

                b.Property(b => b.NumOfPassengers)
                .HasColumnName("numOfPassengers")
                .IsRequired();
            });
            #endregion

            #region ImageShots
            modelBuilder.Entity<ImageShot>(i =>
            {
                i.ToTable("ImageShots")
                .HasKey(i => i.Id);

                i.HasOne(i => i.CarBooking)
                .WithMany(cb => cb.ImageShots)
                .HasForeignKey(i => i.CarBookingId);

                i.Property(i => i.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                i.Property(i => i.CarBookingId)
                .HasColumnName("carBookingId");

                i.Property(i => i.Path)
                .HasColumnName("path")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                i.Property(i => i.Type)
                .HasColumnName("type")
                .HasColumnType("nvarchar(10)")
                .IsRequired();                
            });
            #endregion

            #region Payment
            modelBuilder.Entity<Payment>(p =>
            {
                p.ToTable("Payments");

                p.HasKey(p => p.Id);
                p.Property(p => p.Id)
                .ValueGeneratedOnAdd();

                p.HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(b => b.BookingId);

                p.Property(p => p.Id)
                .HasColumnName("id");

                p.Property(p => p.BookingId)
                .HasColumnName("bookingId");

                p.Property(p => p.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                p.Property(p => p.AmountPaid)
                .HasColumnName("amountPaid")
                .HasColumnType("decimal(12,2)")                
                .IsRequired();

                p.Property(p => p.AmountDue)
                .HasColumnName("amountDue")
                .HasColumnType("decimal(12,2)")
                .IsRequired();

                p.Property(p => p.PaymentDate)
                .HasColumnName("paymentDate")
                .HasColumnType("datetime2(7)")
                .IsRequired();

                p.Property(p => p.Notes)
                .HasColumnName("notes")
                .HasDefaultValue("");
            });
            #endregion

            #region PaymentTransactions
            modelBuilder.Entity<PaymentTransaction>(t =>
                {
                    t.ToTable("PaymentTransactions");

                    t.HasKey(pt => pt.Id);
                    t.Property(pt => pt.Id)
                    .ValueGeneratedOnAdd();

                    t.HasOne(pt => pt.PaymentMethod)
                    .WithMany(pm => pm.PaymentTransctions)
                    .HasForeignKey(pt => pt.PaymentMethodId);

                    t.HasOne(pt => pt.Payment)
                    .WithMany(p => p.PaymentTransactions)
                    .HasForeignKey(pt => pt.PaymentId);

                    t.HasIndex(t => new { t.PaymentId, t.PaymentMethodId, t.TransactionDate })
                    .IsUnique();

                    t.Property(pt => pt.Id)
                    .HasColumnName("id");

                    t.Property(pt => pt.TransactionType)
                    .HasConversion<string>()
                    .HasColumnType("nvarchar(10)")
                    .HasColumnName("transaction_type")
                    .IsRequired();

                    t.Property(pt => pt.Amount)
                    .IsRequired()
                    .HasColumnName("amount")
                    .HasColumnType("decimal(12,2)");

                    t.Property(pt => pt.TransactionDate)
                    .IsRequired()
                    .HasColumnType("datetime2(7)")
                    .HasColumnName("transactionDate");

                    t.Property(i => i.PaymentId)
                     .HasColumnName("paymentId");

                    t.Property(i => i.PaymentMethodId)
                     .HasColumnName("paymentMethodId");
                });
            #endregion

            #region PaymentMethods
            modelBuilder.Entity<PaymentMethod>(pm =>
            {
                pm.ToTable("PaymentMethods");
                pm.HasKey(pt => pt.Id);
                pm.Property(pt => pt.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                pm.HasIndex(pm => pm.Method)
                .IsUnique();

                pm.Property(pm => pm.Method)
                .HasColumnName("method")
                .HasColumnType("nvarchar(10)")
                .IsRequired();
                                
                pm.Property(pm => pm.Icon)
                .HasColumnName("icon")
                .HasColumnType("nvarchar(10)")
                .IsRequired();
            });
            #endregion

            #region Category
            modelBuilder.Entity<Category>(c =>
            {
                c.ToTable("Categories");
                c.HasKey(pt => pt.Id);
                c.Property(pt => pt.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                c.Property(pt => pt.Title)
                .HasColumnName("title")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                c.HasIndex(c => c.Title)
                .IsUnique();
            });
            #endregion

            #region Tag
            modelBuilder.Entity<Tag>(c =>
            {
                c.ToTable("Tags");
                c.HasKey(pt => pt.Id);
                c.Property(pt => pt.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                c.Property(pt => pt.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                c.HasIndex(c => c.Name)
                .IsUnique();
            });
            #endregion

            #region Car
            modelBuilder.Entity<Car>(c =>
            {
                c.ToTable("Cars");
                c.HasKey(pt => pt.Id);
                c.Property(pt => pt.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                c.HasOne(c => c.Category)
                .WithMany(ct => ct.Cars)
                .HasForeignKey(c => c.CategoryId);

                c.Property(c => c.Model)
                .HasColumnName("model")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

                c.Property(c => c.Color)
                .HasColumnName("color")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                c.Property(c => c.Pph)
                .HasColumnName("pph")
                .HasColumnType("decimal(12,2)")
                .IsRequired();

                c.Property(c => c.Ppd)
                .HasColumnName("price_per_day")
                .HasColumnType("decimal(12,2)")
                .IsRequired();

                c.Property(c => c.Mbw)
                .HasColumnName("mbw")
                .HasColumnType("decimal(12,2)")
                .IsRequired();

                c.Property(c => c.Image)
                .HasColumnName("image")
                .HasColumnType("nvarchar(max)")
                .IsRequired();

                c.Property(c => c.CategoryId)
                .HasColumnName("categoryId");

                c.Property(c => c.Capacity)
                .HasColumnName("capacity");

                c.Property(c => c.CarStatus)
                .HasConversion<string>()
                .HasColumnType("nvarchar(max)")
                .HasColumnName("status")
                .HasDefaultValue(CarStatusEnum.Available)
                .IsRequired();
            });
            #endregion

            #region CarBooking
            modelBuilder.Entity<CarBooking>(cb =>
            {
                cb.ToTable("CarBookings");
                cb.HasKey(c => c.BookingId);
                cb.Property(cb => cb.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("booking_id");

                cb.HasOne(cb => cb.Car)
                .WithMany(c => c.CarBookings)
                .HasForeignKey(c => c.CarId);

                cb.HasOne(cb => cb.Booking)
                .WithOne(b => b.CarBooking)
                .HasForeignKey<CarBooking>(cb => cb.BookingId);

                cb.Property(cb => cb.CarId)
                .HasColumnName("car_id");

                cb.Property(cb => cb.PickupLocation)
                .HasColumnType("nvarchar(10)")
                .HasColumnName("pickUpLocation")
                .HasMaxLength(100)
                .IsRequired();

                cb.Property(cb => cb.DropoffLocation)
                .HasColumnType("nvarchar(10)")
                .HasColumnName("dropOffLocation")
                .HasMaxLength(100)
                .IsRequired();

                cb.Property(cb => cb.WithDriver)
                .HasColumnName("withDriver")
                .HasColumnType("bit")
                .IsRequired();

                cb.HasIndex(cb => new { cb.CarId, cb.BookingId })
                .IsUnique() ;
            });
            #endregion

            #region TripBooking

            modelBuilder.Entity<TripBooking>(t =>
            {
                t.ToTable("TripBookings");
                t.HasKey(c => c.BookingId);
                t.Property(cb => cb.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("booking_id");

                t.HasOne(cb => cb.TripPlan)
                .WithMany(c => c.TripBookings)
                .HasForeignKey(c => c.TripPlanId);

                t.HasOne(cb => cb.Booking)
                .WithOne(b => b.TripBooking)
                .HasForeignKey<TripBooking>(cb => cb.BookingId);

                t.Property(t => t.TripPlanId)
                .HasColumnName("tripPlanId");

                t.Property(t => t.WithGuide)
                .HasColumnName("withGuide")
                .HasColumnType("bit")
                .IsRequired();

                t.HasIndex(cb => new { cb.TripPlanId, cb.BookingId })
                .IsUnique();
            });
            #endregion

            #region Regions
            modelBuilder.Entity<Region>(r => 
            {
                r.ToTable("Regions")
                .HasKey(r => r.Id);
                r.Property(r => r.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                r.Property(r => r.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(100)
                .IsRequired();

                r.HasIndex(r => r.Name)
                .IsUnique();

            });
            #endregion

            #region  TripPlan
            modelBuilder.Entity<TripPlan>(t =>
            {
                t.ToTable("TripPlans")
                .HasKey(t => t.Id);

                t.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                t.HasOne(t => t.Region)
                .WithMany(r => r.TripPlans)
                .HasForeignKey(t => t.RegionId);

                t.HasOne(t => t.Trip)
                .WithMany(r => r.TripPlans)
                .HasForeignKey(t => t.TripId);

                t.Property(t => t.TripId)
                .HasColumnName("tripId");

                t.Property(t => t.RegionId)
                .HasColumnName("regionId");

                t.Property(t => t.StartDateTime)
                .HasColumnType("datetime2(7)")
                .HasColumnName("start_date_time")
                .IsRequired();

                t.Property(t => t.EndDateTime)
                .HasColumnType("datetime2(7)")
                .HasColumnName("end_date_time")
                .IsRequired();

                //This duration is decimal the integer part is for the number of days and the fraction part is for the number of hours and minuite
                t.Property(t => t.Duration)
                .HasColumnType("decimal(5,2)")
                .HasColumnName("duration")
                .IsRequired();

                //Here it may be null if there is no sevices 
                t.Property(t => t.IndudedServices)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("induded_services")
                .HasDefaultValue("");

                //Here it may be null if there is no meals in the trip
                t.Property(t => t.MealsPlan)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("meals_plan")
                .HasDefaultValue("");

                //Here it may be nulll if the trip is for one day for example so there is no need to stay in a hotel
                t.Property(t => t.HotelsStays)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("hotels_stays")
                .HasDefaultValue("");
            });
            #endregion

            #region TripPlanCar
            modelBuilder.Entity<TripPlanCar>(t =>
            {
                t.ToTable("TripPlanCars")
                .HasKey(t => t.Id);

                t.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                t.HasOne(t => t.Car)
                .WithMany(c => c.TripPlanCars)
                .HasForeignKey(t => t.CarId);

                t.HasOne(t => t.TripPlan)
                .WithMany(c => c.TripPlanCars)
                .HasForeignKey(t => t.TripPlanId);

                t.Property(t => t.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(12,2)")
                .IsRequired();

                t.Property(t => t.TripPlanId)
                .HasColumnName("trip_plan_id");

                t.Property(t => t.CarId)
                .HasColumnName("car_id");

                t.HasIndex(t => new { t.CarId, t.TripPlanId })
                .IsUnique();
            });
            #endregion

            #region Trip
            modelBuilder.Entity<Trip>(t =>
            {
                t.ToTable("Trips")
                .HasKey(t => t.Id);

                t.Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                t.Property(t => t.Slug)
                .HasColumnName("slug")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                t.Property(t => t.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(50)
                .HasDefaultValue("Trip");

                t.Property(t => t.Slug)
                .HasColumnName("slug")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                t.Property(t => t.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(10)")
                .HasDefaultValue("");

                t.Property(t => t.IsAvailable)
                .HasColumnName("isAvailable")
                .HasColumnType("bit")
                .IsRequired();

                t.Property(t => t.IsPrivate)
               .HasColumnName("isPrivate")
               .HasColumnType("bit")
               .IsRequired();
            });
            #endregion

            #region Contacts
            modelBuilder.Entity<ContactType>(c =>
            {
                c.ToTable("ContactTypes")
                .HasKey(c => c.Id);

                c.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                c.Property(c => c.Type)
                .HasColumnName("type")
                .HasConversion<string>()
                .HasColumnType("nvarchar(10)")
                .IsRequired();

                c.HasIndex(c => c.Type)
                .IsUnique();
            });
            #endregion

            #region Customer
            modelBuilder.Entity<Customer>(c =>
            {
                c.ToTable("Customers");
                c.HasKey(c => c.UserId);

                c.Property(c => c.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");

                c.Property(c => c.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(50)
                .IsRequired();

                c.Property(c => c.LastName)
                .HasColumnName("last_name")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(50)
                .IsRequired();

                c.Property(c => c.Country)
                .HasColumnName("country")
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(50)
                .IsRequired();
            });

            modelBuilder.Entity<CustomerContact>(c =>
            {
                c.ToTable("CustomerContacts")
                .HasKey(c => c.Id);

                c.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                c.HasOne(c => c.Customer)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CustomerId);

                c.HasOne(c => c.ContactType)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.ContactTypeId);

                c.Property(c => c.Value)
                .HasColumnName("Value")
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256)
                .IsRequired();

                c.Property(c => c.ContactTypeId)
                .HasColumnName("contact_type_id");

                c.Property(c => c.CustomerId)
                .HasColumnName("customer_id");

                c.HasIndex(c => new { c.Value, c.CustomerId, c.ContactTypeId }).IsUnique();
            });
            #endregion

            #region Employee
            modelBuilder.Entity<Employee>(e => 
            {
                e.ToTable("Employees")
                .HasKey(e => e.UserId);

                e.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");

                e.Property(e => e.HireDate)
                .HasColumnType("datetime2(7)")
                .HasColumnName("hier_date")
                .IsRequired();
            });

            #endregion

            #region Post
            modelBuilder.Entity<PostTag>(p => 
            {
                p.ToTable("PostTags")
                .HasKey(t => new {t.PostId, t.TagId});

                p.HasOne(p => p.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(t => t.TagId);

                p.HasOne(p => p.Post)
                .WithMany(t => t.Tags)
                .HasForeignKey(t => t.PostId);

                p.Property(t => t.PostId)
                .HasColumnName("post_id");

                p.Property(p => p.TagId)
                .HasColumnName("tag_id");
            });

            modelBuilder.Entity<Post>(p =>
            {
                p.ToTable("Posts")
                .HasKey(p => p.Id);

                p.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                p.HasOne(p => p.PostType)
                .WithMany(pt => pt.Posts)
                .HasForeignKey(p => p.PostTypeId);

                p.HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AuthorId);                

                p.Property(p => p.AuthorId)
                .HasColumnName("author_id");

                p.Property(p => p.PostTypeId)
               .HasColumnName("post_type_id");

                p.Property(p => p.Title)
                .HasColumnName("title")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100)
                .IsRequired();

                p.Property(p => p.Body)
                .HasColumnName("body")
                .HasColumnType("nvarchar(256)")
                .IsRequired();

                p.Property(p => p.Slug)
                .HasColumnName("slug")
                .HasColumnType("nvarchar(256)")
                .IsRequired();

                p.Property(p => p.Image)
                .HasColumnName("image")
                .HasColumnType("nvarchar(256)")
                .IsRequired();

                p.Property(p => p.Summary)
                .HasColumnName("sammary")
                .HasColumnType("nvarchar(max)");

                p.Property(p => p.Views)
                .HasColumnName("views")
                .HasDefaultValue(0);

                p.Property(p => p.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasColumnType("nvarchar(100)")
                .IsRequired();

                p.Property(p => p.PublishDate)
                .HasColumnName("publish_date")
                .HasColumnType("datetime2(7)")
                .HasDefaultValue(DateTime.Now)
                .IsRequired();
            });
            #endregion

            #region PostType
            modelBuilder.Entity<PostType>(p =>
            {
                p.ToTable("PostTypes")
                .HasKey(p => p.Id);

                p.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

                p.Property(p => p.Description)
                .HasColumnType("nvarchar(256)")
                .HasColumnName("description")
                .HasDefaultValue("");

                p.Property(p => p.Title)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("title")
                .IsRequired();
            });
            #endregion

            #region SEO
            modelBuilder.Entity<SEOMetaData>(p =>
            {
                p.ToTable("SEOMetaDatas")
                .HasKey(p => p.Id);

                p.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

                p.HasOne(p => p.Post)
                .WithMany(p => p.MetaDatas)
                .HasForeignKey(p => p.PostId);

                p.Property(p => p.PostId)
                .HasColumnName("post_id");

                p.Property(p => p.Description)
                .HasColumnType("nvarchar(256)")
                .HasColumnName("description")
                .HasDefaultValue("");

                p.Property(p => p.Title)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("title")
                .IsRequired();

                p.Property(p => p.UrlSlug)
                .HasColumnType("nvarchar(256)")
                .HasColumnName("url_slug")
                .IsRequired();

                p.Property(p => p.KeyWords)
                .HasColumnType("nvarchar(256)")
                .HasColumnName("keywords")
                .IsRequired();
            });
            #endregion

            #region Credit
            modelBuilder.Entity<Credit>(c =>
            {
                c.ToTable("Credits")
                .HasKey(c => c.Id);

                c.Property(c => c.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

                c.HasOne(c => c.Customer)
                .WithMany(c => c.Credits)
                .HasForeignKey(c => c.CustomerId);

                c.HasOne(c => c.PaymentMethod)
                .WithMany(pm => pm.Credits)
                .HasForeignKey(c => c.PaymentMethodId);

                c.Property(c => c.CustomerId)
                .HasColumnName("customer_id")
                .IsRequired();

                c.Property(c => c.PaymentMethodId)
                .HasColumnName("payment_method_id")
                .IsRequired();

                c.Property(c => c.CreditAmount)
                .HasColumnName("credit_amount")
                .HasColumnType("decimal(12,2)")
                .IsRequired();

                c.Property(c => c.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true)
                .IsRequired();
            });
            #endregion
        }
    }
}