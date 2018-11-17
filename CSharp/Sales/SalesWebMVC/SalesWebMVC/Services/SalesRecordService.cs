using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj 
                         in _context.SalesRecord
                         select obj;

            if (minDate.HasValue)
            {
                result = result.Where(a => a.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(a => a.Date <= maxDate.Value);
            }

            return await result.Include(a => a.Seller)
                .Include(b => b.Seller.Department)
                .OrderByDescending(c => c.Date)
                .ToListAsync();
        }
    }
}
