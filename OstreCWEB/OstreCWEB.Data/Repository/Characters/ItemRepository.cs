﻿using Microsoft.EntityFrameworkCore;
using OstreCWEB.Data.DataBase;
using OstreCWEB.Data.Repository.Characters.CharacterModels;
using OstreCWEB.Data.Repository.Characters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OstreCWEB.Data.Repository.Characters
{
    internal class ItemRepository : IItemRepository
    {
        private OstreCWebContext _context;
        public ItemRepository(OstreCWebContext context)
        {
           _context = context;
        }

        public OstreCWebContext Context { get; }

        public async Task CreateAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _context.Items
                .Include(i => i.ActionToTrigger)
                .Include(i=>i.PlayableClass)
                .ToListAsync();
        }

        public async Task<Item> GetByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.ItemId == id);
        }

        public async Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}