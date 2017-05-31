using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Abstract
{
    public interface IBanRepository
    {
        IQueryable<Ban> Bans { get; }
        bool IsUserBanned(string userEmail);
        void BanUserById(string userId, string admId);
        TimeSpan TimeToEndOfBun(string userId);
    }
}
