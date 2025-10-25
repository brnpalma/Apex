# 🏎️ Apex Transport API

Assim como no automobilismo, a Apex busca o ponto ideal entre **velocidade, precisão e estabilidade**, oferecendo uma estrutura **limpa, eficiente, testável e altamente manutenível** para o domínio de entregas. 

Uma solução modular voltada para **gestão de entregas e transportadores**, desenvolvida com **Clean Architecture em .NET**, projetada para escalar com **camadas independentes e dependências invertidas**.

## 🔗 Rotas Importantes
- Swagger (UI padrão): [https://localhost:5001/swagger](https://localhost:5001/swagger)
- (Opcional) UI alternativa Scalar: [https://localhost:5001/scalar](https://localhost:5001/scalar)

---

## 🛠️ Setup automático

Ao executar a aplicação (`dotnet run`), o banco de dados e suas tabelas são **criados automaticamente** via Entity Framework Core. Não é necessário rodar comandos manuais como `dotnet ef database update` — as migrações são aplicadas na inicialização, facilitando o processo de clonagem e execução do projeto sem esforço adicional.

---

## 🧭 Visão Geral  

A API fornece uma base sólida para operações de **autenticação, cadastro de usuários, gerenciamento de fornecedores e controle logístico**, garantindo **segurança, performance e confiabilidade** em cada requisição.

O projeto é dividido em **microsserviços independentes** — como:
- **AuthApex API** → responsável por autenticação e geração de tokens JWT  
- **TransportApex API** → responsável por fornecedores, rotas e entregas  

Essa separação permite **evolução contínua** sem comprometer a integridade da aplicação.

---

## 🧩 Requisitos e Tecnologias Principais  
- 🖥️ **Linguagem:** C# (.NET 9.0)  
- 🧱 **Estrutura da solução:** arquivo `Apex.sln` no nível raiz  
- 🧮 **Organização modular:** múltiplos projetos representando as camadas da arquitetura limpa  

---
7
## 🏗️ Arquitetura Limpa (Clean Architecture)

### ⚙️ Princípios adotados  
- **Dependências voltadas para dentro:** camadas externas conhecem as internas, mas não o contrário.  
- **Domínio isolado:** entidades e regras de negócio independentes de frameworks, UI ou banco de dados.  
- **Camada de aplicação:** casos de uso orquestram a lógica e dependem apenas de abstrações.  
- **Infraestrutura:** implementa detalhes concretos (persistência, serviços externos, etc).  
- **Transporte (API/UI):** depende apenas da camada de aplicação e domínio, não da infraestrutura.

### 🧠 Estrutura aplicada no projeto  
- **Domínio:** entidades e regras puras de negócio.  
- **Aplicação:** casos de uso, DTOs e interfaces de repositório.  
- **Infraestrutura:** implementações concretas de persistência e serviços externos.  
- **Transporte/API/UI:** controllers, endpoints e modelos de entrada/saída.  
- **Inversão de dependência:** o núcleo define as interfaces, e as camadas externas as implementam.

### 💡 Benefícios  
- Testes de unidade simplificados.  
- Manutenção facilitada e desacoplada da infraestrutura.  
- Escalabilidade e adaptação a novas tecnologias.  
- Clareza e separação de responsabilidades entre camadas.  

---

## 🗂️ Estrutura de Pastas  

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

> 💡 Recomenda-se detalhar internamente cada microsserviço (ex: Domain, Application, Infrastructure, UI).  

**Exemplo completo:**
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
│   └─ API/
│       ├─ Controllers/
│       └─ Models/
├─ TransportApex/
│   ├─ Domain/
│   ├─ Application/
│   ├─ Infrastructure/
│   └─ API/
└─ Apex.sln
```

---

## 🏃‍♂️ Como Executar  

1. **Clone o repositório:**  
   ```bash
   git clone https://github.com/brnpalma/Apex.git
   ```  
2. **Abra a solução** `Apex.sln` no Visual Studio ou IDE compatível.  
3. **Defina o projeto de inicialização** (por exemplo, `AuthApex.API`).  
4. **Restaure pacotes NuGet**, compile e execute.  
5. **Configure** strings de conexão e variáveis de ambiente conforme o ambiente.  
6. **Acesse o Swagger UI ou Scalar** no navegador para testar os endpoints.  

---

## 📐 Convenções e Boas Práticas  

- ✳️ **Nomeação clara:** `Entidades` no Domínio e `UseCases` na Aplicação.  
- 🧩 **Interfaces** iniciam com `I` (ex: `IUserRepository`).  
- 🚫 **Domínio nunca depende** diretamente de infraestrutura.  
- 🔄 **Use DTOs** para mapeamento entre UI e Domínio.  
- 🗃️ **Organização rígida por responsabilidade:** mantenha separação de camadas.  

---

## 🔗 Dependências Externas  
> Liste as principais bibliotecas aqui (ex: Entity Framework Core, AutoMapper, MediatR, etc).

---

## 🤝 Contribuindo  

Contribuições são bem-vindas!  
1. Faça um **Fork** do repositório.  
2. Crie um branch: `feature/nova-funcionalidade`.  
3. Faça commits claros e limpos.  
4. Abra um **Pull Request** descrevendo suas alterações.

---

## 📜 Licença  
Este projeto está licenciado sob os termos da **MIT License**.  
> Consulte o arquivo `LICENSE` para mais detalhes.

---

## 👤 Contato  
🔧 **Desenvolvedor:** Bruno Palma  
📧 **E-mail:** seu-email@exemplo.com  
🌐 **GitHub:** [@brnpalma](https://github.com/brnpalma)

---
