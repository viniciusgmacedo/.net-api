using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinhaApi.models;
using MinhaApi.Dtos.Stock;

namespace MinhaApi.Mappers.StockMappers
{
    public static class StockMappers // estatico para nao precisar instanciar
    {

        public static MinhaApi.Dtos.Stock.StockDto ToStockDto(this Stock stockModel)
 //método de extensão que converte Stock para StockDto
        {
            return new MinhaApi.Dtos.Stock.StockDto
{
    Id = stockModel.Id,
    Symbol = stockModel.Symbol,
    CompanyName = stockModel.CompanyName,
    Purchase = stockModel.Purchase,
    LastDiv = stockModel.LastDiv,
    Industry = stockModel.Industry,
    MarketCap = stockModel.MarketCap
};

        }
        
    }
}