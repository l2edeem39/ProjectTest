using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface IDataService
    {
        public List<DataModel> GetData();
        public Guid GetOperationID();
    }
}
