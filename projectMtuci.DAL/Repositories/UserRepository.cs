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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(User entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public bool Delete(User entity)
        {
            _db.Users.Remove(entity);
            _db.SaveChangesAsync();

            return true;
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetByName(string name)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<User>> Select()
        {
            return await _db.Users.ToListAsync();
        }
    }
}
