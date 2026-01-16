using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MinhaApi.data;
using MinhaApi.Mappers.StockMappers;
using MinhaApi.Dtos.Stock;
using MinhaApi.Interfaces;

namespace MinhaApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly applicationDBContext _context; //injeção de dependência do contexto do banco de dados
        private readonly IStockRepository _stockRepo;

        public StockController(applicationDBContext context, IStockRepository stockRepo) //construtor que recebe o contexto do banco de dados
        {
            _stockRepo = stockRepo;
            _context = context; //atribui o contexto ao campo privado
        }

        [HttpGet] //rota para pegar todos os stocks
        public async Task<IActionResult> GetAll() //pega todos os stocks
        {
            var stocks = await _stockRepo.GetAllAsync(); //chama o repositório para pegar todos os stocks
            var stockDTO = stocks.Select(s => s.ToStockDto()); // usa o tolist para converter em lista e pegar todos os stocks
            return Ok(stockDTO); //retorna a lista de stocks com status 200 OK
        }

        [HttpGet("{id}")] //rota para pegar um stock pelo id
        public async Task<IActionResult> GetById([FromRoute] int id) //fromroute para pegar o id da rota
        {
            var stock = await _stockRepo.GetByIdAsync(id); //usa o find para procurar o stock pelo id
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
            await _stockRepo.CreateAsync(stockModel); //adiciona o novo stock ao contexto
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto()); //retorna o stock criado com status 201 Created

        }

        [HttpPatch]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto) //aqii tenho o id e o corpo da requisição
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateStockDto); //procura o stock pelo id
            if (stockModel == null)
            {
                return NotFound(); //se não encontrar, retorna 404 Not Found
            }
            return Ok(stockModel.ToStockDto()); //retorna o stock atualizado com status 200 OK

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) //deleta um stock pelo id
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
