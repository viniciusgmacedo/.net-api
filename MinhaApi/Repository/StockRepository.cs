using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhaApi.data;
using MinhaApi.Interfaces;
using MinhaApi.models;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Dtos.Stock;

namespace MinhaApi.Repository
{
    public class StockRepository : IStockRepository
    {

        private readonly applicationDBContext _context;
        public StockRepository(applicationDBContext context)
        {
            _context = context;
        }
        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync();
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (existingStock == null)
            {
                return null;
            }

            if (stockDto.Symbol != null)
                existingStock.Symbol = stockDto.Symbol;

            if (stockDto.CompanyName != null)
                existingStock.CompanyName = stockDto.CompanyName;

            if (stockDto.Purchase.HasValue)
                existingStock.Purchase = stockDto.Purchase;

            if (stockDto.LastDiv.HasValue)
                existingStock.LastDiv = stockDto.LastDiv;

            if (stockDto.Industry != null)
                existingStock.Industry = stockDto.Industry;

            if (stockDto.MarketCap.HasValue)
                existingStock.MarketCap = stockDto.MarketCap;

            _context.Stocks.Update(existingStock);
            await _context.SaveChangesAsync();
            return existingStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}