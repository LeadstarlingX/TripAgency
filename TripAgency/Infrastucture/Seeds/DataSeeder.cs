using Domain.Context;
using Domain.Enum;
using Domain.Common;
using Domain.Entities.ApplicationEntities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Seeds
{
    public class DataSeeder(
        ApplicationDbContext context,
        RoleManager<ApplicationRole> roleManeger,
        IdentityAppDbContext identityAppDbContext,
        UserManager<ApplicationUser> userManager)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IdentityAppDbContext _identityAppDbContext = identityAppDbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManeger = roleManeger;
        public void SeedData()
        {
            var shouldUpdateContext = false;


            if (shouldUpdateContext)
            {
                shouldUpdateContext = false;
                _context.SaveChanges();
            }
            if(!_identityAppDbContext.Roles.Any())
            {
                shouldUpdateContext = true;

                var roleUser = new ApplicationRole()
                {
                    Name = DefaultSetting.UserRoleName,
                };
                var roleEmployee = new ApplicationRole()
                {
                    Name = DefaultSetting.EmployeeRoleName,
                };
                var roleAdmin = new ApplicationRole()
                {
                    Name = DefaultSetting.AdminRoleName
                };
                var roleCustomer = new ApplicationRole()
                {
                    Name = DefaultSetting.CustomerRoleName
                };
                _roleManeger.CreateAsync(roleAdmin).GetAwaiter().GetResult();
                _roleManeger.CreateAsync(roleUser).GetAwaiter().GetResult();
                _roleManeger.CreateAsync(roleEmployee).GetAwaiter().GetResult();
                _roleManeger.CreateAsync(roleCustomer).GetAwaiter().GetResult();

            }

            if (!_identityAppDbContext.Users.Any(u => u.Email == DefaultSetting.DefaultAdminOneEmail))
            {
                var adminUser = new ApplicationUser
                {
                    Email = DefaultSetting.DefaultAdminOneEmail,
                    UserName = DefaultSetting.DefaultAdminOneUserName,
                    PhoneNumber = DefaultSetting.DefaultAdminOnePhone,
                    PhoneNumberConfirmed = true,
                    IsActive = true
                };

                var result = _userManager.CreateAsync(adminUser, DefaultSetting.DefaultAdminPassword).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(adminUser, DefaultSetting.AdminRoleName).GetAwaiter().GetResult();
                    var code = _userManager.GenerateEmailConfirmationTokenAsync(adminUser).GetAwaiter().GetResult();
                    _userManager.ConfirmEmailAsync(adminUser, code).GetAwaiter().GetResult();
                }

                shouldUpdateContext = true;
            }
            if (shouldUpdateContext)
            {
                _identityAppDbContext.SaveChanges();
                shouldUpdateContext = false;
            }
            var identityUser = _identityAppDbContext.Users.FirstOrDefault(u => u.Email == DefaultSetting.DefaultAdminOneEmail);
            if (identityUser == null)
            {
                throw new InvalidOperationException("Admin user not found after seeding.");
            }

            var adminUserId = identityUser.Id;

            // Seed ContactTypes
            if (!_context.ContactTypes.Any())
            {
                var contactTypes = Enum.GetValues<ContactTypeEnum>()
                    .Select(type => new ContactType { Type = type })
                    .ToList();

                _context.ContactTypes.AddRange(contactTypes);
                shouldUpdateContext = true;
            }

            // Seed Customer (linked to Identity User)
            if (!_context.Customers.Any(c => c.UserId == adminUserId))
            {
                var customer = new Customer
                {                    
                    FirstName = DefaultSetting.DefaultAdminOneFName,
                    LastName = DefaultSetting.DefaultAdminOneLName,
                    UserId = adminUserId,
                    Country = "Damascus"
                };

                _context.Customers.Add(customer);
                shouldUpdateContext = true;
            }

            // Seed Employee (linked to Identity User)
            if (!_context.Employees.Any(e => e.UserId == adminUserId))
            {
                var employee = new Employee
                {
                    HireDate = DateTime.Now,
                    UserId = adminUserId
                };

                _context.Employees.Add(employee);
                shouldUpdateContext = true;
            }
            
            if (!_context.Bookings.Any())
            {
                var bookings = new List<Booking>
                {
                    new Booking
                    {
                        CustomerId = adminUserId,
                        Employeeid = adminUserId,
                        BookingType = "Car Rental",
                        StartDateTime = DateTime.Now.AddDays(1),
                        EndDateTime = DateTime.Now.AddDays(2),
                        Status = BookingStatusEnum.Pending,
                        NumOfPassengers = 2
                    },
                    new Booking
                    {
                        CustomerId = 1,
                        BookingType = "Event Booking",
                        StartDateTime = DateTime.Now.AddDays(-5),
                        EndDateTime = DateTime.Now.AddDays(-3),
                        Status = BookingStatusEnum.Completed,
                        NumOfPassengers = 6
                    }
                };

                _context.Bookings.AddRange(bookings);
                shouldUpdateContext = true;
            }

            if (!_context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                       Title = "VIP"
                    },
                    new Category
                    {
                        Title = "Eco"
                    },
                    new Category
                    {
                        Title = "Family"
                    }
                };

                _context.Categories.AddRange(categories);
                shouldUpdateContext = true;
            }

            if (shouldUpdateContext)
            {
                shouldUpdateContext = false;
                _identityAppDbContext.SaveChanges();
                _context.SaveChanges();
            }
        }
    }

}
