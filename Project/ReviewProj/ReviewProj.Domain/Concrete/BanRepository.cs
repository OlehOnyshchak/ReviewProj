using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class BanRepository : IBanRepository
    {
        private AppDbContext context = new AppDbContext();

        public IQueryable<Ban> Bans
        {
            get
            {
                return context.Bans;
            }
        }

        public void BanUserById(string userId, string admId)
        {
            Ban ban = new Ban
            {
                Admin = context.Admins.FirstOrDefault(a => a.Id == admId),
                EndTime = DateTime.Now + new TimeSpan(14, 0, 0, 0), // now + 14 days
                StartTime = DateTime.Now,
                User = context.Users.FirstOrDefault(o => o.Id == userId)
            };

            context.Bans.Add(ban);
            context.SaveChanges();
        }

        public bool IsUserBanned(string userEmail)
        {
            foreach(Ban ban in Bans)
            {
                if (ban.User.Email == userEmail && ban.EndTime > DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }

        // how long before the end of ban
        public TimeSpan TimeToEndOfBun(string userId)
        {
            return context.Bans.Where(b => b.User.Id == userId)
                .Max(b => b.EndTime) - DateTime.Now;
        }
    }
}
