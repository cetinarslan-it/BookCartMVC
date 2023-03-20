using Library.DataAccess.Repository.IRepository;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company> ,ICompanyRepository
    {
        private readonly LibraryDBContext _db;
        public CompanyRepository(LibraryDBContext db) : base(db)
        {

            _db = db;

        }

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
