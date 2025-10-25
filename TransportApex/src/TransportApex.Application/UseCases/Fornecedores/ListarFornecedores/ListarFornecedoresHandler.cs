using Apex.Shared.Interfaces;
using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.UseCases.Fornecedores.ListarFornecedores
{
    public class ListarFornecedoresHandler(IFornecedorService fornecedorService, ICurrentUserService currentUserService) 
        : IRequestHandler<ListarFornecedoresRequest, Result<IEnumerable<ListaFornecedorDto>>>
    {
        private readonly IFornecedorService _fornecedorService = fornecedorService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<IEnumerable<ListaFornecedorDto>>> Handle(ListarFornecedoresRequest request, CancellationToken cancellationToken)
        {
            return await _fornecedorService.ListarAsync(_currentUserService.IdUsuario, _currentUserService.Role);
        }
    }
}
