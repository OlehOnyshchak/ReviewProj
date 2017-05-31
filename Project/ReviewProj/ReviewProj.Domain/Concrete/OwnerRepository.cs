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

        public IQueryable<Owner> Owners
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void BanOwnerById(string id, string admId)
        {
            Ban ban = new Ban
            {
                Admin = context.Admins.FirstOrDefault(a => a.Id == admId),
                BanDuration = new TimeSpan(14, 0, 0, 0),
                StartTime = DateTime.Now,
                User = context.Owners.FirstOrDefault(o => o.Id == id)
            };

            context.Bans.Add(ban);
            context.SaveChanges();
        }

        public Owner FindByEmail(string email)
        {
            return context.Owners.FirstOrDefault(r => r.Email == email);
        }
    }
}
