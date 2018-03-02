using System;
using System.Collections.Generic;

namespace DateLayer.Models
{
    public partial class Manager
    {
        public Manager()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
