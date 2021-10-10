using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using OpenML.BookStore.Application.Common.Mappings;
using OpenML.BookStore.Domain.Entities;

namespace OpenML.BookStore.Application.Books.ViewModel
{
   public class BookViewModel
    {
        public string ISBN { get; set; }
        public string PublisherName { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Pub_Id { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
    }
    public class BookValidator : AbstractValidator<BookViewModel>
    {
        public BookValidator()
        {
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.PublisherName).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.Year).NotEmpty().WithMessage("{PropertyName} should be not empty.");
        }
    }
}
