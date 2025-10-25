using Apex.Shared.Enums;
using Apex.Shared.Results;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.Common.Interfaces
{
    public interface IFornecedorService
    {
        Task<Result<FornecedorDto>> CadastrarAsync(string nome, string cnpj, string idUsuario, Role? role);
        Task<Result<IEnumerable<ListaFornecedorDto>>> ListarAsync(string idUsuario, Role? role);
    }
}
