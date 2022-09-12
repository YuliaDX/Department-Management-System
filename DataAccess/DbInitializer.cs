using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDbInitializer
    {
        public void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        readonly EFDbContext _dataContext;
        public DbInitializer(EFDbContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
