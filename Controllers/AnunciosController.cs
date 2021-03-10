using AnuncioWeb.Database;
using AnuncioWeb.Helpers;
using AnuncioWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace AnuncioWeb.Controllers
{

    [Route("api/anuncios")]
    public class AnunciosController : ControllerBase
    {
        private readonly AnuncioContext _banco;
        public AnunciosController(AnuncioContext banco)
        {
            _banco = banco;
        }

        //APP /api/anuncios
        [HttpGet]
        [Route("")]
        public ActionResult ObterAnuncios([FromQuery] AnuncioUrlQuery query)
        {
            var item = _banco.Anuncios.AsQueryable();

            if (query.Data.HasValue)
            {
                item = item.Where(a => a.Criado > query.Data.Value || a.Atualizado > query.Data.Value);
            }

            if (query.PagNum.HasValue)
            {
                var quantidadeTotalRegistros = item.Count();
                item = item.Skip((query.PagNum.Value - 1) * query.PagRegistro.Value).Take(query.PagRegistro.Value);

                var paginacao = new Paginacao();
                paginacao.NumeroPagina = query.PagNum.Value;
                paginacao.RegistroPorPagina = query.PagRegistro.Value;
                paginacao.TotalRegistros = quantidadeTotalRegistros;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistros / query.PagRegistro.Value);

                Response.Headers.Add("X-PAgination", JsonConvert.SerializeObject(paginacao));

                if (query.PagNum > paginacao.TotalPaginas)
                {
                    return NotFound();
                }
            }
            return Ok(item);
        }

        //WEB -- /api/anuncios/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult ObterAnuncioID(int id)
        {
            var obj = _banco.Anuncios.Find(id);

            if (obj == null)
                return NotFound();
            //return StatusCode(404);
            return Ok();
        }


        // --api/anuncios(POST: marca, modelo, versao, ano, km, obs)
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody] Anuncio anuncio)
        {
            _banco.Anuncios.Add(anuncio);
            _banco.SaveChanges();

            return Created($"/api/anuncios/{anuncio.id}", anuncio);
        }

        // --api/anuncios/1 (PUT: id, marca, modelo, versao, ano, km, obs)
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody] Anuncio anuncio)
        {

            var obj = _banco.Anuncios.AsNoTracking().FirstOrDefault(a => a.id == id);

            if (obj == null)
                return NotFound();

            anuncio.id = id;
            anuncio.Ativo = true;


            _banco.Anuncios.Update(anuncio);
            _banco.SaveChanges();
            return Ok();
        }

        // --/api/anuncios/1 (DELETE)
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var anuncio = _banco.Anuncios.Find(id);

            if (anuncio == null)
                return NotFound();

            anuncio.Ativo = false;
            _banco.Anuncios.Update(anuncio);
            _banco.SaveChanges();

            return NoContent();
        }

    }

}

