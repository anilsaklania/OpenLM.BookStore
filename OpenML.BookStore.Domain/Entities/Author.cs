using OpenML.BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenML.BookStore.Domain.Entities
{
    public class Author: AuditableEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PortalURL { get; set; }
        public string ContactNo { get; set; }
    }
}
