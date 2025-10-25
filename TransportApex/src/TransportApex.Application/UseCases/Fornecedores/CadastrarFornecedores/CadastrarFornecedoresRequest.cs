using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.UseCases.Fornecedores.CadastrarFornecedores
{
    public record CadastrarFornecedoresRequest(string Nome, string Cnpj) : IRequest<Result<FornecedorDto>>;
}
