<<<<<<< HEAD
# MotosScan API - Projeto DevOps & Cloud Computing

## Link externo : http://localhost:5000/swagger/index.html

## 📋 Descrição do Projeto
MotosScan é uma API RESTful para gerenciamento de uma frota de motocicletas da Mottu, desenvolvida com ASP.NET Core e implantada em ambiente de nuvem Azure. A API permite o controle de entrada e saída de motos, gerenciamento de informações da frota e persistência de dados.

## 👥 Membros da Equipe
- **Larissa de Freitas Moura** - RM555136
- **Guilherme Francisco Silva** - RM557648

## 🚀 Tecnologias Utilizadas
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core** - ORM para acesso a dados
- **SQLite** - Banco de dados local para desenvolvimento
- **Oracle Database** - Banco de dados em produção
- **Docker** - Containerização da aplicação
- **Azure Cloud** - Plataforma de nuvem
- **Swagger/OpenAPI** - Documentação da API
- **Git/GitHub** - Controle de versão

## 🏗️ Arquitetura da Solução

### Modelo de Dados
A entidade principal `Moto` possui os seguintes atributos:
- `Id` (int): Identificador único da moto
- `Modelo` (string): Modelo da moto (ex: Honda CG 160)
- `Placa` (string): Placa da moto (ex: ABC1234) - Índice único
- `Estado` (string): Estado de conservação (Bom, Regular, Excelente)
- `Localizacao` (string): Localização atual da moto (Pátio A, Saída, etc)
- `UltimoCheckIn` (DateTime?): Data e hora do último check-in
- `UltimoCheckOut` (DateTime?): Data e hora do último check-out
- `ImagemUrl` (string): URL para a imagem da moto (se disponível)

### Estrutura do Projeto
```
MotosScan/
├── Controllers/
│   └── MotosController.cs      # Controller principal com endpoints
├── Data/
│   ├── AppDbContext.cs         # Contexto do Entity Framework
│   └── DbInitializer.cs        # Inicialização do banco com dados
├── Models/
│   └── Moto.cs                 # Modelo de dados principal
├── Services/
│   └── ImagemService.cs        # Serviço para manipulação de imagens
├── Properties/
│   └── launchSettings.json     # Configurações de execução
├── wwwroot/
│   └── Imagens/                # Armazenamento de imagens
├── Dockerfile                  # Containerização
├── docker-build-run.sh         # Script para Docker local
├── deploy-azure.sh             # Script para deploy no Azure
├── cleanup-azure.sh            # Script para limpeza do Azure
└── Program.cs                  # Ponto de entrada da aplicação
```

## 🔌 Endpoints da API

### Operações CRUD Básicas
- `GET /api/Motos` - Lista todas as motos cadastradas
- `GET /api/Motos/{id}` - Busca moto pelo ID numérico
- `GET /api/Motos/placa/{placa}` - Busca moto pela placa (ex: ABC1234)
- `POST /api/Motos` - Adiciona nova moto à frota
- `PUT /api/Motos/{id}` - Atualiza informações de uma moto existente
- `DELETE /api/Motos/{id}` - Remove uma moto do sistema

### Operações de Check-in/Check-out com Imagens
- `POST /api/Motos/checkin?placa={placa}` - Registra entrada de moto com upload de imagem
- `POST /api/Motos/checkout?placa={placa}` - Registra saída de moto com upload de imagem

### Endpoints Utilitários
- `GET /` - Informações da API e lista de endpoints
- `GET /health` - Health check da aplicação
- `GET /swagger` - Documentação interativa da API

## 🛠️ Instruções de Instalação e Execução

### Pré-requisitos
- .NET SDK 8.0
- Docker Desktop
- Git
- Azure CLI (para deploy em nuvem)

### 🏃‍♂️ Execução Local (Desenvolvimento)

1. **Clone o repositório:**
```bash
git clone https://github.com/LarissaMouraDev/MotosScan-API.git
cd MotosScan-API
```

2. **Restaurar dependências:**
```bash
dotnet restore
```

3. **Executar a aplicação:**
```bash
dotnet run
```

4. **Acessar a aplicação:**
- API: http://localhost:5000
- Swagger: http://localhost:5000/swagger

### 🐳 Execução com Docker (Recomendado)

1. **Usando o script automatizado:**
```bash
chmod +x docker-build-run.sh
./docker-build-run.sh
```

2. **Ou manualmente:**
```bash
# Build da imagem
docker build -t motosscan-api .

# Executar container
docker run -d -p 8080:80 --name motosscan-container motosscan-api
```

3. **Acessar a aplicação:**
- API: http://localhost:8080
- Swagger: http://localhost:8080/swagger

### ☁️ Deploy no Azure

1. **Deploy automatizado:**
```bash
chmod +x deploy-azure.sh
./deploy-azure.sh
```

2. **Limpeza dos recursos (importante após o teste):**
```bash
chmod +x cleanup-azure.sh
./cleanup-azure.sh
```

## 📊 Exemplos de Uso da API

### Listar todas as motos
```bash
curl -X GET "http://localhost:8080/api/Motos" -H "accept: application/json"
```

### Buscar moto por placa
```bash
curl -X GET "http://localhost:8080/api/Motos/placa/ABC1234" -H "accept: application/json"
```

### Adicionar nova moto
```bash
curl -X POST "http://localhost:8080/api/Motos" \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d '{
    "modelo": "Honda CG 160",
    "placa": "XYZ9876",
    "estado": "Excelente",
    "localizacao": "Pátio A"
  }'
```

### Check-in com imagem
```bash
curl -X POST "http://localhost:8080/api/Motos/checkin?placa=ABC1234" \
  -H "accept: application/json" \
  -H "Content-Type: multipart/form-data" \
  -F "imagem=@foto_moto.jpg"
```

## 🏛️ Arquitetura em Nuvem Azure

### Recursos Utilizados:
- **Azure Virtual Machine (Standard_B1s)**: Hospedagem da aplicação
- **Azure Container Registry**: Armazenamento de imagens Docker (opcional)
- **Azure Storage**: Armazenamento de imagens das motos
- **Azure SQL Database**: Banco de dados em produção (configurável)

### Diagrama de Arquitetura:
```
Internet → Azure Load Balancer → VM Linux Ubuntu → Docker Container → MotosScan API
                                      ↓
                               Azure Storage (Imagens)
                                      ↓
                               SQLite/Oracle Database
```

## 🔧 Configurações

### Banco de Dados
- **Desenvolvimento**: SQLite (arquivo local)
- **Produção**: Oracle Database ou Azure SQL

### Variáveis de Ambiente
```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__SqliteConnection="Data Source=MotosScan.db"
```

## 📈 Funcionalidades Implementadas

✅ CRUD completo de motos  
✅ Upload de imagens no check-in/check-out  
✅ Validação de dados com anotações  
✅ Documentação automática com Swagger  
✅ Containerização com Docker  
✅ Scripts de deploy automatizado  
✅ Inicialização automática do banco de dados  
✅ Tratamento de erros e validações  
✅ Logging estruturado  
✅ Health checks  

## 🚦 Status do Projeto

- ✅ **Desenvolvimento**: Concluído
- ✅ **Containerização**: Concluído  
- ✅ **Deploy Local**: Testado e funcionando
- ✅ **Deploy Azure**: Scripts prontos
- ✅ **Documentação**: Completa

## 📞 Suporte

Para dúvidas ou problemas:
- Abra uma issue no GitHub
- Contate a equipe de desenvolvimento

## 📄 Licença

Este projeto foi desenvolvido para fins acadêmicos como parte do desafio DevOps & Cloud Computing da FIAP.

---

**MotosScan API v1.0** 
=======
﻿# 🔧 GUIA DE INTEGRAÇÃO - Adicionando Motorista e Manutenção

Este guia mostra exatamente onde adicionar cada arquivo no projeto **MotosScan-Devolps**.

---

## 📁 ESTRUTURA DE ARQUIVOS PARA ADICIONAR

```
MotosScan-Devolps/
├── Controllers/
│   ├── MotosController.cs          [JÁ EXISTE]
│   ├── MotoristasController.cs     [ADICIONAR] 
│   └── ManutencoesController.cs    [ADICIONAR] 
├── Data/
│   ├── AppDbContext.cs             [ATUALIZAR] 
│   └── DbInitializer.cs            [ATUALIZAR] 
├── Models/
│   ├── Moto.cs                     [ATUALIZAR] 
│   ├── Motorista.cs                [ADICIONAR] 
│   └── Manutencao.cs               [ADICIONAR] 
└── Program.cs                      [ATUALIZAR] 
```

---

## 🚀 PASSO A PASSO DE INTEGRAÇÃO

### **PASSO 1: Adicionar os Models**

1. Navegue até a pasta `Models/`
2. Crie o arquivo `Motorista.cs` com o conteúdo do artifact "Motorista.cs - Models"
3. Crie o arquivo `Manutencao.cs` com o conteúdo do artifact "Manutencao.cs - Models"
4. **SUBSTITUA** o arquivo `Moto.cs` pelo conteúdo do artifact "Moto.cs - Models (Atualizado)"

---

### **PASSO 2: Atualizar o AppDbContext**

1. Navegue até a pasta `Data/`
2. **SUBSTITUA** o conteúdo do arquivo `AppDbContext.cs` pelo artifact "AppDbContext.cs - Data (Atualizado)"

---

### **PASSO 3: Atualizar o DbInitializer**

1. Ainda na pasta `Data/`
2. **SUBSTITUA** o conteúdo do arquivo `DbInitializer.cs` pelo artifact "DbInitializer.cs - Data (Atualizado)"

---

### **PASSO 4: Adicionar os Controllers**

1. Navegue até a pasta `Controllers/`
2. Crie o arquivo `MotoristasController.cs` com o conteúdo do artifact "MotoristasController.cs - Controllers"
3. Crie o arquivo `ManutencoesController.cs` com o conteúdo do artifact "ManutencoesController.cs - Controllers"

---

### **PASSO 5: Atualizar o Program.cs**

1. Na raiz do projeto
2. **SUBSTITUA** o conteúdo do arquivo `Program.cs` pelo artifact "Program.cs (Atualizado)"

---

### **PASSO 6: Criar e Aplicar Migrations**

```bash
# 1. Certifique-se de estar na pasta do projeto
cd MotosScan-Devolps

# 2. Instalar ferramenta EF (se necessário)
dotnet tool install --global dotnet-ef

# 3. Adicionar pacote de design (se necessário)
dotnet add package Microsoft.EntityFrameworkCore.Design

# 4. Criar migration
dotnet ef migrations add AdicionarMotoristasEManutencoes

# 5. Aplicar ao banco de dados
dotnet ef database update
```

---

### **PASSO 7: Executar e Testar**

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Executar a aplicação
dotnet run

# 3. Acessar Swagger
# http://localhost:5000/swagger

# 4. Testar os novos endpoints:
# - GET /api/Motoristas
# - GET /api/Manutencoes
# - POST /api/Motoristas
# - POST /api/Manutencoes
```

---

### **PASSO 8: Atualizar o README.md**

1. Na raiz do projeto
2. **SUBSTITUA** o conteúdo do arquivo `README.md` pelo artifact "README.md (Atualizado com 3 Entidades)"

---

## ✅ CHECKLIST DE VERIFICAÇÃO

Após a integração, verifique se:

- [ ] Os 3 Models foram criados/atualizados corretamente
- [ ] O AppDbContext tem os 3 DbSets (Motos, Motoristas, Manutencoes)
- [ ] Os relacionamentos estão configurados no OnModelCreating
- [ ] O DbInitializer popula dados das 3 entidades
- [ ] Os 3 Controllers foram criados
- [ ] O Program.cs foi atualizado
- [ ] As migrations foram criadas e aplicadas
- [ ] O projeto compila sem erros (`dotnet build`)
- [ ] A aplicação executa sem erros (`dotnet run`)
- [ ] O Swagger mostra os novos endpoints
- [ ] Os dados de seed aparecem ao consultar os endpoints

---

## 🧪 TESTES RÁPIDOS

### Teste 1: Listar Motoristas
```bash
curl http://localhost:5000/api/Motoristas
```

### Teste 2: Listar Manutenções
```bash
curl http://localhost:5000/api/Manutencoes
```

### Teste 3: Criar Motorista
```bash
curl -X POST "http://localhost:5000/api/Motoristas" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Teste Motorista",
    "cpf": "99988877766",
    "cnh": "99988877765",
    "categoriaCNH": "A",
    "status": "Ativo"
  }'
```

### Teste 4: Criar Manutenção
```bash
curl -X POST "http://localhost:5000/api/Manutencoes" \
  -H "Content-Type: application/json" \
  -d '{
    "motoId": 1,
    "tipoManutencao": "Preventiva",
    "descricao": "Teste de manutenção",
    "dataManutencao": "2025-09-29T10:00:00",
    "status": "Pendente"
  }'
```

---

## 🐛 RESOLUÇÃO DE PROBLEMAS

### Erro: "DbSet not found"
**Solução:** Verifique se o AppDbContext foi atualizado corretamente com os 3 DbSets.

### Erro: "Migration failed"
**Solução:** Delete o banco de dados e recrie:
```bash
dotnet ef database drop
dotnet ef database update
```

### Erro: "Foreign key constraint failed"
**Solução:** Certifique-se de que os IDs das entidades relacionadas existem no banco.

### Erro: "Controller not found"
**Solução:** Verifique se os Controllers foram criados na pasta correta com os nomes exatos.

### Erro de compilação
**Solução:** Execute `dotnet clean` e depois `dotnet build`

---

## 📊 RESULTADO FINAL

Após completar todos os passos, você terá:

✅ **3 Entidades Principais:**
- Moto (8 endpoints)
- Motorista (9 endpoints)
- Manutenção (10 endpoints)

✅ **27 Endpoints RESTful** funcionando

✅ **Relacionamentos** entre entidades configurados

✅ **Dados de seed** populados automaticamente

✅ **Swagger** atualizado com todos os endpoints

✅ **100% dos requisitos** atendidos

---

## 🎯 PRÓXIMOS PASSOS (OPCIONAL)

1. Adicionar testes unitários
2. Implementar autenticação JWT
3. Adicionar cache com Redis
4. Implementar notificações de manutenção
5. Criar dashboard de estatísticas
6. Adicionar relatórios em PDF

---

>>>>>>> f930c64 (Adiciona novos arquivos (ex: ImagemService, DbInitializer, etc.))
