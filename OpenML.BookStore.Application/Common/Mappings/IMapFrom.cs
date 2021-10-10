using AutoMapper;
using OpenML.BookStore.Application.Authors.ViewModel;
using OpenML.BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Application.Common.Mappings
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<AuthorViewModel, Author>();
            CreateMap<Author, AuthorViewModel>();
        }
    }
}
