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
    class NewsRepository : IRepository<News>
    {
        private SuLnuDbContext db;

        public NewsRepository(SuLnuDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<News> GetAll()
        {
            return db.News;
        }

        public News Get(int id)
        {
            return db.News.Find(id);
        }

        public News Create(News news)
        {
            return db.News.Add(news).Entity;
        }

        public void Update(News news)
        {
            db.Entry(news).State = EntityState.Modified;
        }

        public IEnumerable<News> Find(Func<News, Boolean> predicate)
        {
            return db.News.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            News news = db.News.Find(id);
            if (news != null)
                db.News.Remove(news);
        }
    }
}
