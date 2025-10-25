using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.UseCases.Fornecedores.ListarFornecedores
{
    public class ListarFornecedoresHandler(IFornecedorService fornecedorService) : IRequestHandler<ListarFornecedoresRequest, Result<IEnumerable<FornecedorDto>>>
    {
        private readonly IFornecedorService _fornecedorService = fornecedorService;

        public async Task<Result<IEnumerable<FornecedorDto>>> Handle(ListarFornecedoresRequest request, CancellationToken cancellationToken)
        {
            return await _fornecedorService.ListarAsync();
        }
    }
}
