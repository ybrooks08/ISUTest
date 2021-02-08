using System;
using System.Collections.Generic;
using System.Text;

namespace ISUTest.Data.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public string Description { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birth { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactTyppe { get; set; }
        public int Stars { get; set; }
        public bool Favorite { get; set; }
    }
}
