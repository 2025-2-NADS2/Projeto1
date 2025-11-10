using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Alma.Application.Interfaces;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Alma.Application.Services
{
    public class TransparenciaService : IRelatorioTransparenciaService
    {
        private readonly IRelatorioTransparenciaRepository _repository;
        private readonly IWebHostEnvironment _env;

        public TransparenciaService(IRelatorioTransparenciaRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<IEnumerable<RelatorioTransparencia>> ListarRelatorios()
            => await _repository.List();

        public async Task<RelatorioTransparencia> EnviarRelatorio(IFormFile arquivo, string titulo, string descricao)
        {
            var pasta = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "transparencia");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var nomeArquivo = $"{Guid.NewGuid()}_{Path.GetFileName(arquivo.FileName)}";
            var caminhoCompleto = Path.Combine(pasta, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var relatorio = new RelatorioTransparencia
            {
                Titulo = titulo,
                Descricao = descricao,
                ArquivoUrl = $"/uploads/transparencia/{nomeArquivo}",
                DataPublicacao = DateTime.UtcNow.Date
            };

            await _repository.Adicionar(relatorio);
            return relatorio;
        }

        public async Task<(string CaminhoArquivo, string NomeArquivo)> ObterRelatorioParaDownload(int id)
        {
            var relatorio = await _repository.GetById(id);
            if (relatorio == null) return (null, null);

            var relativo = relatorio.ArquivoUrl?.TrimStart('/', '\\');
            if (string.IsNullOrWhiteSpace(relativo)) return (null, null);

            var completo = Path.Combine(_env.WebRootPath ?? "wwwroot", relativo);
            if (!File.Exists(completo)) return (null, null);

            return (completo, Path.GetFileName(completo));
        }
    }
}
