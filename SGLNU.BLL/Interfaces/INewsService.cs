using SGLNU.BLL.DTO;
using System.Collections.Generic;

namespace SGLNU.BLL.Interfaces
{
    public interface INewsService
    {
        public IEnumerable<NewsDTO> GetAll();
        public void CreateNews(NewsDTO newsDTO);
        void UpdateImage(int userId, string imageSrc);

    }
}
