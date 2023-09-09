using Microsoft.EntityFrameworkCore;
using projectMtuci.DAL.Interfaces;
using projectMtuci.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.DAL.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _db;

        public SubjectRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Subject entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public bool Delete(Subject entity)
        {
            _db.Subjects.Remove(entity);
            _db.SaveChangesAsync();

            return true;
        }

        public async Task<Subject> Get(int id)
        {
            return await _db.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Subject>> Select()
        {
            return await _db.Subjects.ToListAsync();
        }

        public IQueryable<Subject> GetByName(string name)
        {
            var x = from b in _db.Subjects where b.Name == name select b;
            return x;
            //return await _db.Subjects.FirstOrDefaultAsync(x => x.Name == name);
        }


    }
}
