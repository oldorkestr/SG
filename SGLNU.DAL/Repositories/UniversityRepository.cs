using System;
using System.Collections.Generic;
using System.Text;
using SGLNU.DAL.EF;
using SGLNU.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SGLNU.DAL.Entities;

namespace SGLNU.DAL.Repositories
{
    class UniversityRepository : IRepository<University>
    {
        private SuLnuDbContext db;

        public UniversityRepository(SuLnuDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<University> GetAll()
        {
            return db.Universities;
        }

        public University Get(int id)
        {
            return db.Universities.Find(id);
        }

        public University Create(University university)
        {
            return db.Universities.Add(university).Entity;
        }

        public void Update(University university)
        {
            db.Entry(university).State = EntityState.Modified;
        }

        public IEnumerable<University> Find(Func<University, Boolean> predicate)
        {
            return db.Universities.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            University university = db.Universities.Find(id);
            if (university != null)
                db.Universities.Remove(university);
        }
    }
}
