using Microsoft.EntityFrameworkCore;
using SGLNU.DAL.EF;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGLNU.DAL.Repositories
{
    class FacultyRepository : IRepository<Faculty>
    {
        private SuLnuDbContext db;

        public FacultyRepository(SuLnuDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Faculty> GetAll()
        {
            return db.Faculties;
        }

        public Faculty Get(int id)
        {
            return db.Faculties.Find(id);
        }

        public Faculty Create(Faculty faculty)
        {
            return db.Faculties.Add(faculty).Entity;
        }

        public void Update(Faculty faculty)
        {
            db.Entry(faculty).State = EntityState.Modified;
        }

        public IEnumerable<Faculty> Find(Func<Faculty, Boolean> predicate)
        {
            return db.Faculties.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
            if (faculty != null)
                db.Faculties.Remove(faculty);
        }
    }
}
