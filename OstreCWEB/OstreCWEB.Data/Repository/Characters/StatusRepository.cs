﻿using Microsoft.EntityFrameworkCore;
using OstreCWEB.Data.DataBase;
using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.Interfaces;

namespace OstreCWEB.Data.Repository.Characters
{
    internal class StatusRepository : IStatusRepository
    {
        private readonly OstreCWebContext _db;
        public StatusRepository(OstreCWebContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Status status)
        {
            _db.Statuses.AddAsync(status);
            await _db.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(Status status)
        { 
            _db.Statuses.Remove(status);
          
            await _db.SaveChangesAsync();
        }

        public async Task<List<Status>> GetAllAsync()
        {
         
            return await _db.Statuses.Include(p => p.CharacterActions).ToListAsync();  
        } 

        public async Task<Status> GetByIdAsync(int id)
        {
            return await _db.Statuses.Include(s => s.StatusId == id).SingleOrDefaultAsync(s => s.StatusId == id);
        }

        public async Task UpdateAsync(Status status)
        {
            _db.Statuses.Update(status);
            await _db.SaveChangesAsync(); 
        }
    }
}
