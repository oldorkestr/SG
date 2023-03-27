using SGLNU.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGLNU.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<University> Universities { get; }
        IRepository<Faculty> Faculties { get; }
        IRepository<Document> Documents { get; }
        IRepository<Event> Events { get; }
        IRepository<News> News { get; }
        IRepository<Voting> Votings { get; }
        IRepository<Vote> Votes { get; }
        IRepository<Candidate> Candidates { get; }
        void Save();
    }
}
