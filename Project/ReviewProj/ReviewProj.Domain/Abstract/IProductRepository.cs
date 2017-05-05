using ReviewProj.Domain.Entities;
using System.Linq;

namespace ReviewProj.Domain.Abstract
{
    interface IEnterpriseRepository
    {
        IQueryable<Enterprise> Enterprises { get; }
    }
}
