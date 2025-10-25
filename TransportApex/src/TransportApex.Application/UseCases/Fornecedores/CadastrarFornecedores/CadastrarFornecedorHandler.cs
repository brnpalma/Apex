using Apex.Shared.Results;
using MediatR;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Fornecedores;

namespace TransportApex.Application.UseCases.Fornecedores.CadastrarFornecedores
{
    public class CadastrarFornecedorHandler(IFornecedorService authService) : IRequestHandler<CadastrarFornecedoresRequest, Result<FornecedorDto>>
    {
        private readonly IFornecedorService _fornecedorService = authService;

        public async Task<Result<FornecedorDto>> Handle(CadastrarFornecedoresRequest request, CancellationToken cancellationToken)
        {
            return await _fornecedorService.CadastrarAsync(request.Nome, request.Cnpj);
        }
    }
}
