using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetoLoja.Api
{
    public interface ICepApiService
    {
        [Get("/ws/{cep}/json")]
        Task<CepRespons> GetAddressAsnyc(string cep);
    }
}
