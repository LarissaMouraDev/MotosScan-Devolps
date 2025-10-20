# 🏍️ MotosScan.NET - CP5

## 👥 Integrantes
- Larissa de Freitas Moura - 555136
- Guilherme Francisco - 557648

## 🎯 Sobre o Projeto CP5

Evolução do projeto MotosScan.NET com implementação de:
- ✅ **Clean Architecture** com DDD
- ✅ **MongoDB** como banco de dados
- ✅ **Health Check** monitorando API e banco
- ✅ **Versionamento de API** via Swagger
- ✅ **Clean Code** (SRP, DRY, KISS, YAGNI)

## 🏗️ Arquitetura

```
src/
├── MotosScan.Api/              # Apresentação
├── MotosScan.Application/      # Casos de Uso
├── MotosScan.Domain/           # Regras de Negócio
└── MotosScan.Infrastructure/   # Acesso a Dados
```

## 🛠️ Tecnologias

- .NET 6.0
- MongoDB 6.0+
- ASP.NET Core Web API
- Swagger/OpenAPI
- Health Checks

## 📋 Pré-requisitos

- .NET SDK 6.0+
- MongoDB 6.0+ (local ou Atlas)
- Visual Studio 2022 ou VS Code

## 🚀 Como Executar

### 1. Instalar MongoDB

**Windows:**
```bash
# Baixar em: https://www.mongodb.com/try/download/community
# Ou via Chocolatey:
choco install mongodb
```

**macOS:**
```bash
brew tap mongodb/brew
brew install mongodb-community
brew services start mongodb-community
```

**Linux:**
```bash
sudo apt-get install mongodb
sudo systemctl start mongodb
```

### 2. Clonar e Configurar

```bash
git clone https://github.com/LarissaMouraDev/MotosScan.NET
cd MotosScan.NET
```

### 3. Configurar Connection String

Edite `appsettings.json`:
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "MotosScanDB"
  }
}
```

### 4. Executar

```bash
dotnet restore
dotnet run --project src/MotosScan.Api
```

### 5. Acessar

- **API:** https://localhost:5001
- **Swagger:** https://localhost:5001/swagger
- **Health Check:** https://localhost:5001/health

## 📡 Endpoints

### V1 API

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/v1/motos` | Lista todas motos |
| GET | `/api/v1/motos/{id}` | Busca por ID |
| POST | `/api/v1/motos` | Cria nova moto |
| PUT | `/api/v1/motos/{id}` | Atualiza moto |
| DELETE | `/api/v1/motos/{id}` | Remove moto |

## 🏥 Health Check

```bash
curl https://localhost:5001/health
```

Resposta:
```json
{
  "status": "Healthy",
  "timestamp": "2025-10-17T10:30:00Z",
  "checks": [
    {
      "name": "mongodb",
      "status": "Healthy",
      "description": "MongoDB is available",
      "duration": 45.2
    },
    {
      "name": "api",
      "status": "Healthy",
      "description": "API funcionando",
      "duration": 0.1
    }
  ],
  "totalDuration": 45.3
}
```

## 📚 Padrões Implementados

### Clean Code
- ✅ **SRP:** Uma responsabilidade por classe
- ✅ **DRY:** Sem duplicação de código
- ✅ **KISS:** Soluções simples e diretas
- ✅ **YAGNI:** Apenas o necessário

### DDD
- ✅ Entidades ricas com comportamento
- ✅ Value Objects (Placa, CPF, CNH)
- ✅ Agregados Raiz (Moto, Motorista)
- ✅ Repositórios no Domain

### Clean Architecture
- ✅ Separação em camadas
- ✅ Inversão de dependências
- ✅ Baixo acoplamento
- ✅ Alta coesão

## 📊 Critérios de Avaliação CP5

| Critério | Pontos | Status |
|----------|--------|--------|
| Clean Architecture | 1.0 | ✅ |
| DDD | 1.0 | ✅ |
| Clean Code | 1.0 | ✅ |
| MongoDB + CRUD | 3.0 | ✅ |
| Health Check | 2.0 | ✅ |
| Swagger + Versionamento | 1.0 | ✅ |
| GitHub + Commits | 1.0 | ✅ |
| **TOTAL** | **10.0** | ✅ |

## 🔄 Commits Semânticos

```bash
feat: adiciona versionamento de API
fix: corrige validação de placa
refactor: melhora estrutura de repositórios
docs: atualiza README com MongoDB
```

## 📄 Licença

MIT License - Veja LICENSE para detalhes.
```

---

## ✅ 11. Checklist Final

### Antes de Entregar

- [ ] Compilação sem erros
- [ ] MongoDB configurado e funcionando
- [ ] Health Check respondendo em `/health`
- [ ] Swagger acessível e versionado (v1)
- [ ] Todos os endpoints CRUD funcionando
- [ ] Value Objects implementados (Placa, CPF, CNH)
- [ ] Agregados raiz com comportamento
- [ ] Repositórios no Domain (interfaces)
- [ ] Clean Code aplicado (SRP, DRY, KISS, YAGNI)
- [ ] README completo e atualizado
- [ ] Commits semânticos no Git
- [ ] Lista de integrantes no README

### Testes Rápidos

```bash
# 1. Testar Health Check
curl https://localhost:5001/health

# 2. Testar criação de moto
curl -X POST https://localhost:5001/api/v1/motos \
  -H "Content-Type: application/json" \
  -d '{"modelo":"Honda CG 160","placa":"ABC1234","ano":2024}'

# 3. Listar motos
curl https://localhost:5001/api/v1/motos
```

---

## 🎓 Observações Finais

1. **MongoDB Local vs Atlas:**
   - Local: `mongodb://localhost:27017`
   - Atlas: `mongodb+srv://user:pass@cluster.mongodb.net/`

2. **Portas Padrão:**
   - API: 5000 (HTTP) / 5001 (HTTPS)
   - MongoDB: 27017

3. **Penalizações a Evitar:**
   - ❌ Projeto não compila
   - ❌ Health Check ausente
   - ❌ Swagger sem versionamento
   - ❌ Código duplicado
   - ❌ README incompleto

**Boa sorte no CP5! 🚀**

    public async Task<bool> ExistePorPlacaAsync(string placa)
    {
        return await _collection.Find(m => m.Placa == placa).AnyAsync();
    }
}