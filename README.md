# Apex Transport API - O ponto ideal entre velocidade, precisão e entrega.

A Apex Transport API é uma solução modular voltada para gestão de entregas e transportadores, desenvolvida com Clean Architecture em .NET projetada para evoluir com composição de camadas e dependências invertidas.

Assim como no automobilismo, a Apex busca o ponto ideal entre velocidade, precisão e estabilidade, oferecendo uma estrutura limpa, eficiente, testável, escalável e ótima manutenibilidade para o domínio de entregas.

---

## Visão Geral  
.

Ela fornece uma base sólida e escalável para operações de autenticação, cadastro de usuários, gerenciamento de fornecedores e controle logístico, garantindo segurança, performance e precisão em cada requisição.

O projeto é dividido em microsserviços independentes — como o AuthApex API (responsável por autenticação e geração de tokens JWT) e o TransportApex API (responsável por fornecedores, rotas e entregas) — permitindo evolução contínua sem comprometer a integridade da aplicação.

---

## Tecnologias Principais  
- Linguagem: C# (.NET)  
- Estrutura de solução: Arquivo `Apex.sln` no nível raiz.  
- Projeto modularizado em várias pastas/projetos, refletindo camadas da arquitetura limpa.

---

## Arquitetura Limpa (Clean Architecture)  
### Princípios adotados  
- Dependências voltadas **para dentro**: camadas externas conhecem as internas, mas não o contrário.  
- Entidades de domínio independentes de frameworks, UI, banco de dados ou infraestrutura.  
- Casos de uso (Application layer) que orquestram lógica de negócios e dependem apenas de abstrações das camadas externas.  
- Infraestrutura que implementa detalhes concretos (persistência, envio de e‑mail, serviço externo) e que é dependida por abstrações definidas nas camadas mais internas.  
- Transporte (API, UI) que depende das camadas de aplicação, não a infraestrutura de dados concreta.

### Como isso está refletido no projeto  
- **Domínio**: aqui ficam as entidades, agregados, regras de negócio puras. Não dependem de nada externo.  
- **Aplicação**: casos de uso, interfaces de repositório, DTOs, orquestração de lógica. Depende de Domínio, mas não de Infraestrutura.  
- **Infraestrutura**: implementação concreta de interfaces de repositório, clientes externos, DB context, etc. Depende de Aplicação + Domínio.  
- **Transporte / API / UI**: camada de exposição — controllers, endpoints, modelos de input/output, mapeamentos. Depende de Aplicação e Domínio.  
- Inversão de dependências: as camadas internas expõem interfaces que as camadas externas implementam. Dessa forma, o núcleo de negócio permanece desacoplado.

### Benefícios esperados  
- Testes de unidade facilitados, pois lógica de negócio está isolada.  
- Facilidade de manutenção e evolução: muda-se infraestrutura sem mexer no domínio.  
- Escalabilidade e adaptabilidade: trocar banco, UI ou mecanismo de envio não impacta o núcleo.  
- Clareza de responsabilidades entre camadas.

---

## Estrutura de Pastas  
```text
Apex/
├─ AuthApex/
│  └─ … (projeto relacionado à autenticação)
├─ TransportApex/
│  └─ … (projeto relacionado ao transporte ou API)
├─ Apex.sln
├─ Apex.slnLaunch
├─ .gitignore
└─ .gitattributes
```
> Obs.: A estrutura acima está reduzida — recomendo expandir conforme os projetos dentro de cada pasta (Domínio, Aplicação, Infraestrutura, etc).  
> Por exemplo, dentro de `AuthApex` pode haver pastas como `Domain`, `Application`, `Infrastructure`, etc.

Se fôssemos mapear uma estrutura mais típica com Clean Architecture, ficaria assim (exemplo):  
```text
Apex/
├─ AuthApex/
│   ├─ Domain/
│   │   └─ Entities/
│   ├─ Application/
│   │   ├─ Interfaces/
│   │   └─ UseCases/
│   ├─ Infrastructure/
│   │   ├─ Repositories/
│   │   └─ DataContext/
│   └─ UI/ (ou API/)
│       ├─ Controllers/
│       └─ Models/
├─ TransportApex/
│   ├─ Domain/
│   ├─ Application/
│   ├─ Infrastructure/
│   └─ UI/
├─ Apex.sln
└─ ...
```

---

## Como Executar  
1. Clone o repositório:  
   ```bash
   git clone https://github.com/brnpalma/Apex.git
   ```  
2. Abra a solução `Apex.sln` no Visual Studio ou outro IDE compatível com .NET.  
3. Verifique os projetos de start‑up definidos (por exemplo `TransportApex` se for API).  
4. Restaure pacotes NuGet, compile e execute.  
5. Configure strings de conexão, variáveis de ambiente ou arquivos de configuração conforme cada projeto (por exemplo Infraestrutura).  
6. Rode os casos de uso, controllers ou endpoints para verificar funcionamento.

---

## Convenções e Boas Práticas  
- Nomeação: Entidades no Domínio, UseCases na Aplicação.  
- Interfaces começam com `I`, por exemplo `IUserRepository` na Application.  
- Não injetar diretamente dependências de infraestrutura no Domínio. Sempre use abstrações.  
- Use DTOs ou Models de input/output na camada UI, mapeando para entidades de domínio ou casos de uso.  
- Mantenha os projetos e pastas organizados conforme responsabilidade — evite misturar infra‑detalhes com domínio.

---

## Dependências Externas  
Liste aqui as principais bibliotecas usadas (ex: Entity Framework, AutoMapper, MediatR, etc) se houverem.

---

## Contribuindo  
Contribuições são bem‑vindas!  
Siga os passos:  
1. Fork do repositório  
2. Crie um branch: `feature/nova‑funcionalidade`  
3. Commit com boas mensagens e padrões de código limpos  
4. Abra um Pull Request e descreva claramente a mudança proposta.

---

## Licença  
(Indique aqui a licença caso haja: MIT, Apache, etc).  
Exemplo:  
Este projeto está licenciado sob os termos da licença MIT.

---

## Contato  
🔧 Desenvolvedor: brnpalma  
📧 E‑mail: seu‑email@exemplo.com (ou link para perfil GitHub)
