using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using OpenML.BookStore.Application.Common.Mappings;
using OpenML.BookStore.Domain.Entities;

namespace OpenML.BookStore.Application.Warehouses.ViewModel
{
    public class WarehouseViewModel
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
    }
}
