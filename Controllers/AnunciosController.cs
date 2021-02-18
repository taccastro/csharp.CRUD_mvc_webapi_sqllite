using AnuncioWeb.Database;
using AnuncioWeb.Models;
using Microsoft.AspNetCore.Mvc;

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

        //APP /api/anuncios/
        [HttpGet]
        [Route("")]
        public ActionResult ObterAnuncios()
        {

            return new JsonResult(_banco.Anuncios);
        }

        //WEB -- /api/anuncios/1
        [Route("{id}")]
        [HttpGet]
        public ActionResult ObterAnuncioID(int id)
        {           
            return Ok(_banco.Anuncios.Find(id));
        }


        // --api/anuncios(POST: marca, modelo, versao, ano, km, obs)
        [Route("")]
        [HttpPost]
        public ActionResult Cadastrar([FromBody]Anuncio anuncio)
        {
            _banco.Anuncios.Add(anuncio);
            _banco.SaveChanges();

            return Ok();
        }

        // --api/anuncios/1 (PUT: id, marca, modelo, versao, ano, km, obs)
        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody]Anuncio anuncio)
        {
      
            _banco.Anuncios.Update(anuncio);

            return Ok();
        }

        // --/api/anuncios/1 (DELETE)
        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            _banco.Anuncios.Remove(_banco.Anuncios.Find(id));

            return Ok();
        }

    }

}

