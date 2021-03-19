using System.Collections.Generic;

namespace AnuncioWeb.Helpers
{
    public class PaginationList<T> : List<T>
    {
        public Paginacao Paginacao{ get; set; }
    }
}
