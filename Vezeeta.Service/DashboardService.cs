using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Service;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.Specifications.AppUser;
using Vezeeta.Core.UnitOfWork;
using Vezeeta.Repository;
using Vezeeta.Repository.Data;

namespace Vezeeta.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(AppDbContext dbContext , IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllDoctorsAsync(IList<ApplicationUser> users, SpecificationsParams specParams)
        {
            var spec = new DashboardDoctorSpec(users, specParams);
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<ApplicationUser> GetDoctorByIdAsync(string id)
        {
            var spec = new DashboardDoctorSpec(id);
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllPatientsAsync(IList<ApplicationUser> users, SpecificationsParams specParams)
        {
            var spec = new DashboardPatientSpec(users, specParams);
            return await ApplySpecification(spec).ToListAsync();
        }


        public async Task<IReadOnlyList<Booking>> GetPatientByIdAsync(string id)
        {
            var spec = new BookingsPatientSpecifications(id);
            var bookings = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
            return bookings;
        }


        private IQueryable<ApplicationUser> ApplySpecification(ISpecificationAppUser<ApplicationUser> spec)
        {
            return SpecificationEvaluatorAppUser<ApplicationUser>.GetQuery(_dbContext.Set<ApplicationUser>(), spec);
        }
    }
}
