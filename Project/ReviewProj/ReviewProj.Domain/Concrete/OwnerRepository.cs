using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class OwnerRepository : IOwnerRepository
    {
        private AppDbContext context = new AppDbContext();

        // Get All Owners
        public IQueryable<Owner> Owners
        {
            get
            {
                return context.Owners;
            }
        }

        // Find Owner with the same Email
        public Owner FindByEmail(string email)
        {
            return context.Owners.FirstOrDefault(r => r.Email == email);
        }
    }
}
