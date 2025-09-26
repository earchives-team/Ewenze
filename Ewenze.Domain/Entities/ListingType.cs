using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Domain.Entities
{
    public class ListingType
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public DateTime ModifiedDate { get; set; }

        public DateTime CreationDate { get; set; }
         
    }
}
