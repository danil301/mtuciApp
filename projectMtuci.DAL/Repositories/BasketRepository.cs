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
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _db;

        public BasketRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Basket entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public bool Delete(Basket entity)
        {
            _db.Baskets.Remove(entity);
            _db.SaveChangesAsync();

            return true;
        }

        public async Task<Basket> Get(int id)
        {
            return await _db.Baskets.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Basket>> Select()
        {
            return await _db.Baskets.ToListAsync();
        }
    }
}
