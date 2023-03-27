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
    public class VoteRepository : IRepository<Vote>
    {
        private SuLnuDbContext db;

        public VoteRepository(SuLnuDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Vote> GetAll()
        {
            return db.Votes;
        }

        public Vote Get(int id)
        {
            return db.Votes.Find(id);
        }

        public Vote Create(Vote vote)
        {
            return db.Votes.Add(vote).Entity;
        }

        public void Update(Vote vote)
        {
            db.Entry(vote).State = EntityState.Modified;
        }

        public IEnumerable<Vote> Find(Func<Vote, Boolean> predicate)
        {
            return db.Votes.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Vote vote = db.Votes.Find(id);
            if (vote != null)
                db.Votes.Remove(vote);
        }
    }
}
