# MinhaApi

API de exemplo gerada/local desenvolvida para o curso.

Como rodar localmente:

1. Abra PowerShell na pasta do projeto:

```powershell
cd C:\Users\souvi\Desktop\api\MinhaApi
dotnet restore
dotnet build
dotnet ef database update
dotnet run
```

2. A aplicação irá expor os endpoints documentados com OpenAPI quando em ambiente de desenvolvimento.

Observações:

- O arquivo `.gitignore` já inclui as pastas `bin/` e `obj/`.
- Adicione sua `ConnectionStrings` em `appsettings.Development.json` se necessário.
