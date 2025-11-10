using Alma.Application.DTOs.Campanha;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alma.Application.Services
{
    public class CampanhaService : ICampanhaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICampanhaRepository _campanhaRepository;

        public CampanhaService(IUnitOfWork unitOfWork, ICampanhaRepository campanhaRepository)
        {
            _unitOfWork = unitOfWork;
            _campanhaRepository = campanhaRepository;
        }

        public async Task<List<Campanha>> GetTodasCampanhasDisponiveis()
            => await _campanhaRepository.GetAllCampanhasDisponiveis();

        public async Task<int> CriarNovaCampanha(NovaCampanhaDto dto)
        {
            Validar(dto, isUpdate:false);

            var campanha = new Campanha
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                MetaValor = dto.MetaValor,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                Status = dto.Status ?? StatusCampanha.Ativa
            };

            await _campanhaRepository.PostCampanha(campanha);
            await _unitOfWork.CommitAsync();

            return campanha.Id; // Id gerado pelo banco
        }

        public async Task UpdateCampanha(NovaCampanhaDto dto)
        {
            if (!dto.Id.HasValue || dto.Id.Value <= 0)
                throw new ArgumentException("Id da campanha é obrigatório para atualização.");

            Validar(dto, isUpdate:true);

            var existente = await _campanhaRepository.GetCampanhaByIdAsync(dto.Id.Value);
            if (existente == null)
                throw new InvalidOperationException("Campanha não encontrada.");

            existente.Titulo = dto.Titulo;
            existente.Descricao = dto.Descricao;
            existente.MetaValor = dto.MetaValor;
            existente.DataInicio = dto.DataInicio;
            existente.DataFim = dto.DataFim;
            if (dto.Status.HasValue)
                existente.Status = dto.Status.Value;

            await _campanhaRepository.UpdateCampanha(existente);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCampanha(int id)
        {
            var existente = await _campanhaRepository.GetCampanhaByIdAsync(id);
            if (existente == null)
                throw new InvalidOperationException("Campanha não encontrada.");

            await _campanhaRepository.DeleteCampanha(existente);
            await _unitOfWork.CommitAsync();
        }

        private static void Validar(NovaCampanhaDto dto, bool isUpdate)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentException("Descrição é obrigatória.");

            if (!dto.MetaValor.HasValue || dto.MetaValor <= 0)
                throw new ArgumentException("Meta de valor é obrigatória e deve ser maior que zero.");

            if (dto.DataInicio == default)
                throw new ArgumentException("Data de início é obrigatória.");

            if (dto.DataFim.HasValue && dto.DataFim < dto.DataInicio)
                throw new ArgumentException("Data fim não pode ser anterior à data início.");
        }
    }
}
