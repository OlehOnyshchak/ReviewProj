using ReviewProj.Domain.Entities;
using System.Linq;

namespace ReviewProj.Domain.Abstract
{
    public interface IEnterpriseRepository
    {
        IQueryable<Enterprise> Enterprises { get; }
    }
}
