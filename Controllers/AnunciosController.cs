using AnuncioWeb.Contracts;
using AnuncioWeb.Helpers;
using AnuncioWeb.Models;
using AnuncioWeb.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AnuncioWeb.Controllers
{

    [Route("api/anuncios")]
    public class AnunciosController : ControllerBase
    {
        private readonly IAnuncioRepository _repository;        
        
        private readonly IMapper _mapper;


        public AnunciosController(IAnuncioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //APP /api/anuncios
        [HttpGet]
        [Route("")]
        public ActionResult ObterAnuncios([FromQuery] AnuncioUrlQuery query)
        {
            var item = _repository.ObterAnuncios(query);

            if (query.PagNum > item.Paginacao.TotalPaginas)
            {
                return NotFound();
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Paginacao));

            return Ok(item.ToList());
        }

        //WEB -- /api/anuncios/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult ObterAnuncioID(int id)
        {
            var obj = _repository.Obter(id);

            if (obj == null)
                return NotFound();
            //return StatusCode(404);

            AnuncioDTO anuncioDTO = _mapper.Map<Anuncio, AnuncioDTO>(obj);

            anuncioDTO.Links = new List<LinkDTO>();

            anuncioDTO.Links.Add(
                new LinkDTO("self", $"https://localhost:44367/api/anuncios/{anuncioDTO.id}", "GET"));

            return Ok(anuncioDTO);

        }

        // --api/anuncios(POST: marca, modelo, versao, ano, km, obs)
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Anuncio anuncio)
        {
            _repository.Cadastrar(anuncio);

            return Created($"/api/anuncios/{anuncio.id}", anuncio);
        }

        // --api/anuncios/1 (PUT: id, marca, modelo, versao, ano, km, obs)
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody] Anuncio anuncio)
        {

            var obj = _repository.Obter(id);
            if (obj == null)
                return NotFound();
            
            anuncio.id = id;
            _repository.Atualizar(anuncio);
          
            return Ok();
        }

        // --/api/anuncios/1 (DELETE)
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var anuncio = _repository.Obter(id);

            if (anuncio == null)
                return NotFound();

            _repository.Deletar(id);

            return NoContent();
        }

    }

}

