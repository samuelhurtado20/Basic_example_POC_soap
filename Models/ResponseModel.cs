using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Basic_example_POC_soap.Models
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public int resultCode { get; set; }
        public string message { get; set; }
    }
}