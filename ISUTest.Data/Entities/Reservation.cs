using System;
using System.Collections.Generic;

#nullable disable

namespace ISUTest.Data.Entities
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public short Stars { get; set; }
        public bool Favorite { get; set; }

        public virtual Contact ContactNameNavigation { get; set; }
    }
}
