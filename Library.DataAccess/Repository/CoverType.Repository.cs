using Library.DataAccess.Repository.IRepository;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly LibraryDBContext _db;

        public CoverTypeRepository(LibraryDBContext db) : base(db)
        {
            _db = db;
        }



        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
