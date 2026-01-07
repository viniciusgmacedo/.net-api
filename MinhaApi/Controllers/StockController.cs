using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult GetAll() //pega todos os stocks
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto()); // usa o tolist para converter em lista e pegar todos os stocks
            return Ok(stocks); //retorna a lista de stocks com status 200 OK
        }

        [HttpGet("{id}")] //rota para pegar um stock pelo id
        public IActionResult GetById([FromRoute] int id) //fromroute para pegar o id da rota
        {
            var stock = _context.Stocks.Find(id); //usa o find para procurar o stock pelo id
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto()); //retorna o stock convertido para StockDto com status 200 OK
        }

        [HttpPost] //rota para criar um novo stock

        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO(); //converte o CreateStockRequestDto para Stock
            _context.Stocks.Add(stockModel); //adiciona o novo stock ao contexto
            _context.SaveChanges(); //salva as mudanças no banco de dados
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto()); //retorna o stock criado com status 201 Created

        }



    }
}
