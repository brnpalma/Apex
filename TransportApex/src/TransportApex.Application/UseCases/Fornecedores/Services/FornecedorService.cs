using Apex.Shared.Results;
using TransportApex.Application.Common.Interfaces;
using TransportApex.Application.Dtos.Fornecedores;
using TransportApex.Domain.Entities;
using TransportApex.Domain.ValueObjects;

namespace TransportApex.Application.UseCases.Fornecedores.Services
{
    public class FornecedorService(IFornecedorRepository fornecedorRepository) : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;

        public async Task<Result<FornecedorDto>> CadastrarAsync(string nome, string cnpj)
        {
            if (await _fornecedorRepository.ExistePorCnpjAsync(cnpj))
                return Result<FornecedorDto>.Fail(400, "Já existe um fornecedor com este CNPJ.", null);

            if (!Cnpj.TryCreate(cnpj, out var cnpjVo, out var error))
            {
                return Result<FornecedorDto>.Fail(400, error ?? "CNPJ inválido.", null);
            }

            var fornecedor = new Fornecedor(nome, cnpjVo);
            await _fornecedorRepository.AdicionarAsync(fornecedor);

            var fornecedorDto = new FornecedorDto
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                Cnpj = fornecedor.Cnpj.Numero,
                Mensagem = "Fornecedor cadastrado com sucesso",
                Sucesso = true
            };

            return Result<FornecedorDto>.Ok(fornecedorDto, 201);
        }

        public async Task<Result<IEnumerable<ListaFornecedorDto>>> ListarAsync()
        {
            var fornecedores = await _fornecedorRepository.ObterTodosAsync();

            var fornecedoresDto = fornecedores
                .Select(f => new ListaFornecedorDto 
                {
                    Id = f.Id, 
                    Nome = f.Nome, 
                    Cnpj = f.Cnpj.Numero
                })
                .ToList();

            return Result<IEnumerable<ListaFornecedorDto>>.Ok(fornecedoresDto, 200);
        }
    }
}
