﻿using ReviewProj.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewProj.Domain.Entities;

namespace ReviewProj.Domain.Concrete
{
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private AppDbContext context = new AppDbContext();

        public IEnumerable<Enterprise> GetByName(string subName)
        {
            return string.IsNullOrEmpty(subName) ? 
                Enterprises : Enterprises.Where(ent => ent.Name.Contains(subName));
        }

        public IEnumerable<Enterprise> GetByType(EnterpriceType type)
        {
            return Enterprises.Where(ent => ent.Type == type);
        }

        public IEnumerable<Enterprise> GetByRating(int rating)
        {
            return Enterprises.Where(ent => ent.Rating == rating);
        }

        public IEnumerable<Enterprise> GetByTypeAndName(EnterpriceType type, string subName)
        {
            return this.GetByType(type).Intersect<Enterprise>(this.GetByName(subName)); 
        }

        public IEnumerable<Enterprise> GetFiltratedByName(IList<int> ratings, IList<EnterpriceType> types, string subName)
        {
            IEnumerable<Enterprise> entWithName = this.GetByName(subName);

            IEnumerable<Enterprise> result;
            if (ratings.Count == 0 && types.Count == 0)
            {
                result = entWithName;
            }
            else if (ratings.Count == 0)
            {
                result = entWithName.Where(ent => types.Contains(ent.Type));
            }
            else if (types.Count == 0)
            {
                result = entWithName.Where(ent => ratings.Contains(Convert.ToInt32(ent.Rating)));
            }
            else
            {
                result = entWithName.Where(ent => ratings.Contains(Convert.ToInt32(ent.Rating)) &&
                    types.Contains(ent.Type));
            }

            return result;
        }

        public IQueryable<Enterprise> Enterprises
        {
            get { return context.Enterprises; }
        }
    }
}