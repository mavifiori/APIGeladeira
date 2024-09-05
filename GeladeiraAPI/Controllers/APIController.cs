using Application.DTOs;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace APIGeladeira.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        private readonly GeladeiraRepository _repositorio;
        private readonly IMapper _mapper;

        public APIController(GeladeiraRepository repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        // GET: api/API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> ObterItens()
        {
            try
            {
                var itens = await _repositorio.ObterItensAsync();
                if (!itens.Any())
                {
                    return NoContent();
                }
                return Ok(itens);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<ItemModel>> BuscarPorId(int id)
        {
            try
            {
                var itemExistente = await _repositorio.ObterItemPorIdAsync(id);
                if (itemExistente != null)
                {
                    return Ok(itemExistente);
                }
                return NotFound($"O item de ID: {id} não foi encontrado.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/API/adicionar
        [HttpPost("adicionar")]
        public async Task<ActionResult> AdicionarItem([FromBody] AdicionarGeladeiraDTO dto)
        {
            try
            {
                var item = _mapper.Map<ItemModel>(dto);

                var itemAdicionado = await _repositorio.AdicionarItemAsync(item);
                if (itemAdicionado != null)
                {
                    return Ok(itemAdicionado);
                }
                return BadRequest("Erro ao adicionar item.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/API/{id}
        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarItem([FromBody] ItemModel item)
        {
            try
            {
                var itemAtualizado = await _repositorio.AtualizarItemAsync(item);
                if (itemAtualizado != null)
                {
                    return Ok(itemAtualizado);
                }
                return BadRequest("Erro ao atualizar item.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/API/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirItem(int id)
        {
            try
            {
                await _repositorio.RemoverItemAsync(id);
                return Ok("Item removido com sucesso.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}