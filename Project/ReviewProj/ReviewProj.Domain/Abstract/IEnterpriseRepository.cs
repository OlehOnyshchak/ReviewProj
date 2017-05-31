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

   //     void ChangeRating(Enterprise ent, double rating);

        IEnumerable<Enterprise> GetByType(EnterpriceType type);

        IEnumerable<Enterprise> GetByRating(int rating);

        IEnumerable<Enterprise> GetByTypeAndName(EnterpriceType type, string subName);

        IEnumerable<Enterprise> GetFiltratedByName(IList<int> ratings, IList<EnterpriceType> types, string subName);

        Enterprise GetEnterpriseById(int id);

        void AddReview(int entId, string reviewerEmail, Review review);

        void DeleteEnterprise(int id);

        void UpdateMainPhoto(Enterprise enterprise, Resource fileName);
        void RemoveMainPhoto(Enterprise enterprise);

        IQueryable<Enterprise> Enterprises { get; }

    }
}
