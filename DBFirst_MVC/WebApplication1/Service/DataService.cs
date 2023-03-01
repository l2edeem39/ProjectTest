using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class DataService : IDataService
    {
        private readonly AdventureWorkContext _cotext;
        public DataService(AdventureWorkContext context)
        {
            _cotext = context;
        }
        public List<DataModel> GetData()
        {
            var result = new List<DataModel>();
            var a = new Student();
            var setData = new DataModel();
            var ab = _cotext.User.ToList();
            foreach (var item in ab)
            {
                setData = new DataModel();
                setData.UserName = item.Username;
                setData.Password = item.Password;
                setData.Email = item.Email;
                result.Add(setData);
            }
            return result;
        }
        public Guid GetOperationID()
        {
            return Guid.NewGuid();
        }
    }
}
