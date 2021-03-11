using AnuncioWeb.Contracts;
using AnuncioWeb.Database;
using AnuncioWeb.Helpers;
using AnuncioWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnuncioWeb.Repositories
{
    public class AnuncioRepository : IAnuncioRepository
    {
        private readonly AnuncioContext _banco;

        public AnuncioRepository(AnuncioContext banco)
        {
            _banco = banco;
        }

        public PaginationList<Anuncio> ObterAnuncios(AnuncioUrlQuery query)
        {
            var lista = new PaginationList<Anuncio>();

            var item = _banco.Anuncios.AsNoTracking().AsQueryable();

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

                lista.Paginacao = paginacao;

            }

            lista.AddRange(item.ToList());

            return lista; 
        }

        public Anuncio Obter(int id)
        {
            return _banco.Anuncios.AsNoTracking().FirstOrDefault(a => a.id == id);
        }

        public void Cadastrar(Anuncio anuncio)
        {
            _banco.Anuncios.Add(anuncio);
            _banco.SaveChanges();
        }

        public void Atualizar(Anuncio anuncio)
        {
            _banco.Anuncios.Update(anuncio);
            _banco.SaveChanges();
        }          

        public void Deletar(int id)
        {
            var anuncio = Obter(id);
            anuncio.Ativo = false;
            _banco.Anuncios.Update(anuncio);
            _banco.SaveChanges();
        }        
    }
}
