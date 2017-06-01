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

        void ChangeRating(Enterprise ent, double rating);

        IEnumerable<Enterprise> GetByType(EnterpriceType type);

        IEnumerable<Enterprise> GetByRating(int rating);

        IEnumerable<Enterprise> GetByTypeAndName(EnterpriceType type, string subName);

        IEnumerable<Enterprise> GetFiltratedByName(IList<int> ratings, IList<EnterpriceType> types, string subName);

        Enterprise GetEnterpriseById(int id);

        void AddReview(int entId, string reviewerEmail, Review review);

        void DeleteEnterprise(int id);

        void UpdateMainPhoto(Enterprise enterprise, Resource fileName);

        // INTEGRATION
        void AddContact(Enterprise enterprise, string emailOrPhone);
        void RemoveContact(Enterprise enterprise, int idCont);
        void EditContact(Enterprise enterprise, string emailOrPhone, int idCont);
        //void AddListContacts(Enterprise ent);
        //List<string> GetEnterpriseContacts(Enterprise enterprise);
        void RemovePhoto(Enterprise enterprise, int id);
        void AppointMain(Enterprise enterprise, int id);
        void ChangeData(Enterprise entInDb, Enterprise entNewData);

        IQueryable<Enterprise> Enterprises { get; }

    }
}
