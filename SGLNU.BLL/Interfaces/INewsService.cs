using Microsoft.AspNetCore.Identity;
using SGLNU.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGLNU.BLL.Interfaces
{
    public interface INewsService
    {
        public IEnumerable<NewsDTO> GetAll();
        public void CreateNews(NewsDTO newsDTO);
        void UpdateImage(int userId, string imageSrc);

    }
}
