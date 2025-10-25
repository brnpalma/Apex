using Apex.Shared.Interfaces;
using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.UseCases.Fornecedores.CadastrarFornecedores
{
    public class CadastrarFornecedorHandler(IFornecedorService authService, ICurrentUserService currentUserService) 
        : IRequestHandler<CadastrarFornecedoresRequest, Result<FornecedorDto>>
    {
        private readonly IFornecedorService _fornecedorService = authService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Result<FornecedorDto>> Handle(CadastrarFornecedoresRequest request, CancellationToken cancellationToken)
        {
            return await _fornecedorService.CadastrarAsync(request.Nome, request.Cnpj, 
                _currentUserService.IdUsuario, _currentUserService.Role);
        }
    }
}
