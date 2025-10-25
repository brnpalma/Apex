using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.UseCases.Fornecedores.ListarFornecedores
{
    public record ListarFornecedoresRequest() : IRequest<Result<IEnumerable<FornecedorDto>>>;
}
