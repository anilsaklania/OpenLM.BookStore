﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using OpenML.BookStore.Application.Common.Mappings;
using OpenML.BookStore.Domain.Entities;

namespace OpenML.BookStore.Application.Publishers.ViewModel
{
    public class PublisherViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string URL { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
    }
    public class PublisherValidator : AbstractValidator<PublisherViewModel>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.URL).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("{PropertyName} should be not empty.");
        }
    }
}
