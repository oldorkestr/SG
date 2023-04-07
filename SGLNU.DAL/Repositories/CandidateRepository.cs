using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGLNU.DAL.EF;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;

namespace SGLNU.DAL.Repositories
{
    public class CandidateRepository : IRepository<Candidate>
    {
        private SuLnuDbContext db;

        public CandidateRepository(SuLnuDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Candidate> GetAll()
        {
            return db.Candidates.Include(v => v.Votes);
        }

        public Candidate Get(int id)
        {
            return db
                .Candidates
                .Include(v => v.Votes)
                .FirstOrDefault(c => c.Id == id);
        }
        
        public Candidate Create(Candidate candidate)
        {
            return db.Candidates.Add(candidate).Entity;
        }

        public void Update(Candidate candidate)
        {
            db.Entry(candidate).State = EntityState.Modified;
        }

        public IEnumerable<Candidate> Find(Func<Candidate, Boolean> predicate)
        {
            return db.Candidates.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            if (candidate != null)
                db.Candidates.Remove(candidate);
        }
    }
}
