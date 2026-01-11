using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.data;
using MinhaApi.Mappers.StockMappers;
using MinhaApi.Dtos.Stock;

namespace MinhaApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly applicationDBContext _context; //injeção de dependência do contexto do banco de dados

        public StockController(applicationDBContext context) //construtor que recebe o contexto do banco de dados
        {
            _context = context; //atribui o contexto ao campo privado
        }

        [HttpGet] //rota para pegar todos os stocks
        public async Task<IActionResult> GetAll() //pega todos os stocks
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stockDTO = stocks.Select(s => s.ToStockDto()); // usa o tolist para converter em lista e pegar todos os stocks
            return Ok(stockDTO); //retorna a lista de stocks com status 200 OK
        }

        [HttpGet("{id}")] //rota para pegar um stock pelo id
        public async Task<IActionResult> GetById([FromRoute] int id) //fromroute para pegar o id da rota
        {
            var stock = await _context.Stocks.FindAsync(id); //usa o find para procurar o stock pelo id
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto()); //retorna o stock convertido para StockDto com status 200 OK
        }

        [HttpPost] //rota para criar um novo stock

        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO(); //converte o CreateStockRequestDto para Stock
            await _context.Stocks.AddAsync(stockModel); //adiciona o novo stock ao contexto
            await _context.SaveChangesAsync(); //salva as mudanças no banco de dados
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto()); //retorna o stock criado com status 201 Created

        }

        [HttpPatch] //rota para criar um novo stock
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto) //aqii tenho o id e o corpo da requisição
        {
            var stockModel = await _context.Stocks.FindAsync(id); //procura o stock pelo id
            if (stockModel == null)
            {
                return NotFound(); //se não encontrar, retorna 404 Not Found
            }
            if (updateStockDto.Symbol is not null) stockModel.Symbol = updateStockDto.Symbol; //atualiza o símbolo se fornecido
            if (updateStockDto.CompanyName is not null) stockModel.CompanyName = updateStockDto.CompanyName; //atualiza o nome da empresa se fornecido
            if (updateStockDto.Purchase.HasValue) stockModel.Purchase = updateStockDto.Purchase.Value; //atualiza o valor de compra se fornecido
            if (updateStockDto.LastDiv.HasValue) stockModel.LastDiv = updateStockDto.LastDiv.Value; //atualiza o último dividendo se fornecido
            if (updateStockDto.Industry is not null) stockModel.Industry = updateStockDto.Industry; //atualiza a indústria se fornecido
            if (updateStockDto.MarketCap.HasValue) stockModel.MarketCap = updateStockDto.MarketCap.Value; //atualiza o valor de mercado se fornecido

            await _context.SaveChangesAsync(); //salva as mudanças no banco de dados
            return Ok(stockModel.ToStockDto()); //retorna o stock atualizado com status 200 OK

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) //deleta um stock pelo id
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id); //procura o stock pelo id
            if (stockModel == null)
            {
                return NotFound(); //se não encontrar, retorna 404 Not Found
            }
            _context.Stocks.Remove(stockModel); //remove o stock do contexto
            await _context.SaveChangesAsync(); //salva as mudanças no banco de dados
            return NoContent(); //retorna 204 No Content
        }




    }
}
