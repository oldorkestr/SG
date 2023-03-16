using Microsoft.EntityFrameworkCore;
using SGLNU.DAL.EF;
using SGLNU.DAL.Enteties;
using SGLNU.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGLNU.DAL.Repositories
{
    class DocumentRepository : IRepository<Document>
    {
        private SuLnuDbContext db;

        public DocumentRepository(SuLnuDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Document> GetAll()
        {
            return db.Documents;
        }

        public Document Get(int id)
        {
            return db.Documents.Find(id);
        }

        public void Create(Document document)
        {
            db.Documents.Add(document);
        }

        public void Update(Document document)
        {
            db.Entry(document).State = EntityState.Modified;
        }

        public IEnumerable<Document> Find(Func<Document, Boolean> predicate)
        {
            return db.Documents.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            Document document = db.Documents.Find(id);
            if (document != null)
                db.Documents.Remove(document);
        }
    }
}
