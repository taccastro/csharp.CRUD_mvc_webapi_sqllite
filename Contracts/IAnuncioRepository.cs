using AnuncioWeb.Helpers;
using AnuncioWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnuncioWeb.Contracts
{
    public interface IAnuncioRepository
    {
        PaginationList<Anuncio> ObterAnuncios(AnuncioUrlQuery query);

        Anuncio Obter(int id);

        void Cadastrar(Anuncio anuncio);

        void Atualizar(Anuncio anuncio);

        void Deletar(int id);



    }

}
