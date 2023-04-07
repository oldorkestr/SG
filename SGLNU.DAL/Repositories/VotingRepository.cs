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
    public class VotingRepository : IRepository<Voting>
    {
        private SuLnuDbContext db;

        public VotingRepository(SuLnuDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Voting> GetAll()
        {
            return db
                .Votings
                .Include(v => v.Faculty)
                .Include(v => v.Candidates)
                .ThenInclude(c=>c.Votes);
        }

        public Voting Get(int id)
        {
            return db
                .Votings
                .Include(v => v.Faculty)
                .Include(v => v.Candidates)
                .ThenInclude(c=>c.Votes)
                .FirstOrDefault(v => v.Id == id);
        }

        public Voting Create(Voting voting)
        {
            return db.Votings.Add(voting).Entity;
        }

        public void Update(Voting voting)
        {
            db.Entry(voting).State = EntityState.Modified;
        }

        public IEnumerable<Voting> Find(Func<Voting, Boolean> predicate)
        {
            return db
                .Votings
                .Include(v => v.Faculty)
                .Include(v => v.Candidates)
                .Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Voting voting = db.Votings.Find(id);
            if (voting != null)
                db.Votings.Remove(voting);
        }
    }
}
