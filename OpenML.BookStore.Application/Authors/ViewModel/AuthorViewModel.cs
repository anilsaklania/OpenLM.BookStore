using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using OpenML.BookStore.Application.Common.Mappings;
using OpenML.BookStore.Domain.Entities;

namespace OpenML.BookStore.Application.Authors.ViewModel
{
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PortalURL { get; set; }
        public string ContactNo { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
    }
    public class AuthorValidator : AbstractValidator<AuthorViewModel>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.PortalURL).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.ContactNo).NotEmpty().WithMessage("{PropertyName} should be not empty.");
        }
    }
}
