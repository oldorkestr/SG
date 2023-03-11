using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Enteties;
using SGLNU.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGLNU.BLL.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService() {}

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NewsDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<News>, List<NewsDTO>>(_unitOfWork.News.GetAll());
        }

        public void CreateNews(NewsDTO newsDTO)
        {
            if (newsDTO != null)
            {
                News news = _mapper.Map<News>(newsDTO);
                _unitOfWork.News.Create(news);
                _unitOfWork.Save();
                //questionDTO.Id = question.Id;
                //_logger.LogInformation("Created new question.");
            }
            else
            {
                //_logger.LogWarning("Could not create new question.");
                throw new ArgumentNullException("questionDTO");
            }
        }
        public void UpdateImage(int newsId, string imageSrc)
        {
            var news = _unitOfWork.News.Get(newsId);
            news.PhotoFilePath = imageSrc;
            _unitOfWork.News.Update(news);
            _unitOfWork.Save();
        }

    }
}
