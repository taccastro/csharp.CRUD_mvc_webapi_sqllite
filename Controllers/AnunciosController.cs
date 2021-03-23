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

            if (item.Count == 0)
                return NotFound();

            if(item.Paginacao != null)
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Paginacao));

            var lista = _mapper.Map<PaginationList<Anuncio>, PaginationList<AnuncioDTO>>(item);

            foreach (var anuncio in lista)
            {
                anuncio.Links = new List<LinkDTO>();
                anuncio.Links.Add(new LinkDTO("self", Url.Link("ObterAnuncio", new { id = anuncio.id }), "GET"));
            }

            return Ok(lista);
        }

        //WEB -- /api/anuncios/1     
        [HttpGet("{id}", Name = "ObterAnuncio")]
        public ActionResult ObterAnuncioID(int id)
        {
            var obj = _repository.Obter(id);

            if (obj == null)
                return NotFound();
            //return StatusCode(404);

            AnuncioDTO anuncioDTO = _mapper.Map<Anuncio, AnuncioDTO>(obj);

            anuncioDTO.Links = new List<LinkDTO>();

            anuncioDTO.Links.Add(
                new LinkDTO("self", Url.Link("ObterAnuncio", new { id = anuncioDTO.id }), "GET"));

            anuncioDTO.Links.Add(
               new LinkDTO("update", Url.Link("AtualizarAnuncio", new { id = anuncioDTO.id }), "UPDATE"));

            anuncioDTO.Links.Add(
               new LinkDTO("delete", Url.Link("ExcluirAnuncio", new { id = anuncioDTO.id }), "DELETE"));

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
        [HttpPut("{id}", Name = "AtualizarAnuncio")]
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
        [HttpDelete("{id}", Name = "ExcluirAnuncio")]
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

