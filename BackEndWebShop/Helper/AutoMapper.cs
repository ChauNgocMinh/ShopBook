using BackEndWebShop.Data;
using BackEndWebShop.Model;
using AutoMapper;

namespace BackEndWebShop.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
