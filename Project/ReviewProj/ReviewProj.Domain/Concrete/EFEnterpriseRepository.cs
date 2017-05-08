using ReviewProj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class EFEnterpriseRepository : IEnterpriseRepository
    {
        private AppDbContext context = new AppDbContext();
        public IQueryable<Enterprise> Enterprises
        {
            get { return context.Enterprises; }
        }
    }
}
