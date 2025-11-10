using Alma.Application.Interfaces;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using Alma.Domain.Enum;
using Microsoft.Extensions.Configuration;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alma.Application.Services
{
    public class DoacaoService : IDoacaoService
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly ICampanhaRepository _campanhaRepository;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        public DoacaoService(
            IDoacaoRepository doacaoRepository,
            ICampanhaRepository campanhaRepository,
            IConfiguration config,
            IUnitOfWork unitOfWork)
        {
            _doacaoRepository = doacaoRepository;
            _campanhaRepository = campanhaRepository;
            _config = config;
            _unitOfWork = unitOfWork;
            MercadoPagoConfig.AccessToken = _config["MercadoPago:AccessToken"];
        }

        public async Task<string> CriarSessaoPagamentoAsync(int campanhaId, decimal valor)
        {
            return await CriarSessaoPagamentoAsync(campanhaId, valor, "Anônimo", null);
        }

        // Sobrecarga não exigida pela interface; manter se usada em páginas Razor ou controllers.
        public async Task<string> CriarSessaoPagamentoAsync(int campanhaId, decimal valor, string doadorNome, string? doadorEmail)
        {
            if (valor <= 0) throw new ArgumentException("Valor da doação inválido.");
            var campanha = await _campanhaRepository.GetCampanhaByIdAsync(campanhaId);
            if (campanha == null) throw new InvalidOperationException("Campanha não encontrada.");

            var doacao = new Doacao
            {
                DoadorNome = doadorNome,
                DoadorEmail = doadorEmail,
                CampanhaId = campanhaId,
                Valor = valor,
                MetodoPagamento = "mercadopago",
                Recorrente = false
            };

            await _doacaoRepository.Adicionar(doacao);
            await _unitOfWork.CommitAsync();

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = campanha.Titulo,
                        Quantity = 1,
                        CurrencyId = "BRL",
                        UnitPrice = valor
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://seusite.com/sucesso",
                    Failure = "https://seusite.com/erro",
                    Pending = "https://seusite.com/pendente"
                },
                AutoReturn = "approved",
                ExternalReference = doacao.Id.ToString()
            };

            var client = new PreferenceClient();
            var preference = await client.CreateAsync(request);
            return preference.InitPoint;
        }

        public async Task ConfirmarDoacaoAsync(int doacaoId)
        {
            var doacao = await _doacaoRepository.ObterPorId(doacaoId);
            if (doacao == null) return;

            doacao.StatusPagamento = StatusPagamentoDoacao.Aprovado;
            await _doacaoRepository.Atualizar(doacao);
            await _unitOfWork.CommitAsync();

            // Caso queira acumular na campanha, adicionar coluna ValorArrecadado na entidade Campanha.
        }
    }
}
