using Apex.Shared.Results;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IFornecedorService
    {
        Task<Result<FornecedorDto>> CadastrarAsync(string nome, string cnpj);
        Task<Result<IEnumerable<ListaFornecedorDto>>> ListarAsync();
    }
}
