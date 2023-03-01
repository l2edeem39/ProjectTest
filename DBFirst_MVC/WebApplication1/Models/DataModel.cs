using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DataModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<DataModel1> DataModel1 { get; set; }
    }
    public class DataModel1
    {
        public List<DataModel2> DataModel2 { get; set; }
    }
    public class DataModel2
    {
        public string UserName { get; set; }
    }
    public class Template
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
