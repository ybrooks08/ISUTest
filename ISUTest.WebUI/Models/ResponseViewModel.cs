using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISUTest.WebUI.Models
{
    public class ResponseViewModel<T>
    {
        public bool success { get; set; }

        public string message { get; set; }

        public T data { get; set; }
    }
}
