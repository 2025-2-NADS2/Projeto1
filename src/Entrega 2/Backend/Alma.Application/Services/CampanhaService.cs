using Alma.Application.DTOs.Campanha;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Domain.Enum;

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
        {
            return await _campanhaRepository.GetAllCampanhasDisponiveis();
        }

        public async Task<Guid> CriarNovaCampanha(NovaCampanhaDto dto)
        {

            isValid(dto);

            var campanha = new Campanha
            {
                Id = Guid.NewGuid(),
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                MetaValor = dto.MetaValor,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                ImagemUrl = dto.ImagemUrl,
                StatusCampanha = StatusCampanha.EM_PROGRESSO,
                CreatedBy = null
            };

            _campanhaRepository.PostCampanha(campanha);
            await _unitOfWork.CommitAsync();

            return campanha.Id;
        }

        public async Task UpdateCampanha(NovaCampanhaDto dto)
        {

            isValid(dto);

            var campanha = new Campanha
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                MetaValor = dto.MetaValor,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                ImagemUrl = dto.ImagemUrl
            };

            _campanhaRepository.UpdateCampanha(campanha);
            await _unitOfWork.CommitAsync();

        }

        public async Task DeleteCampanha(Guid id)
        {
            var campanha = await _campanhaRepository.GetCampanhaById(id);
            await _campanhaRepository.DeleteCampanha(campanha);
        }

        private void isValid(NovaCampanhaDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ArgumentException("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentException("Descrição é obrigatória.");

            if (dto.MetaValor == null)
                throw new ArgumentException("Valor para a meta é obrigatório.");

            if (dto.DataInicio == null || dto.DataFim == null || dto.DataFim < DateTime.Now) // se 0 for "NaoDefinido"
                throw new ArgumentException("Datas são obrigatórias.");

        }

    }
}
