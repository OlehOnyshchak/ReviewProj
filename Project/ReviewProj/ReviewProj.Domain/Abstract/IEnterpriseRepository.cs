using ReviewProj.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewProj.Domain.Abstract
{
    public interface IEnterpriseRepository
    {
        IEnumerable<Enterprise> GetByName(string subName);

        IEnumerable<Enterprise> GetByType(EnterpriceType type);

        IEnumerable<Enterprise> GetByRating(int rating);

        IEnumerable<Enterprise> GetByTypeAndName(EnterpriceType type, string subName);

        IEnumerable<Enterprise> GetFiltratedByName(IList<int> ratings, IList<EnterpriceType> types, string subName);

        IQueryable<Enterprise> Enterprises { get; }
    }
}
