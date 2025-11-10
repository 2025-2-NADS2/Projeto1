using Alma.Application.Interfaces;
using Alma.Application.Interfaces.Repositorios;
using Alma.Domain.Entities;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.Extensions.Configuration;

public class DoacaoService : IDoacaoService
{
    private readonly IDoacaoRepository _doacaoRepository;
    private readonly ICampanhaRepository _campanhaRepository;
    private readonly IConfiguration _config;

    public DoacaoService(
        IDoacaoRepository doacaoRepository,
        ICampanhaRepository campanhaRepository,
        IConfiguration config)
    {
        _doacaoRepository = doacaoRepository;
        _campanhaRepository = campanhaRepository;
        _config = config;

        MercadoPagoConfig.AccessToken = _config["MercadoPago:AccessToken"];
    }

    public async Task<string> CriarSessaoPagamentoAsync(Guid campanhaId, decimal valor)
    {
        var campanha = await _campanhaRepository.GetCampanhaById(campanhaId);
        if (campanha == null)
            throw new Exception("Campanha não encontrada.");

        var doacao = new Doacao
        {
            Id = Guid.NewGuid(),
            CampanhaId = campanhaId,
            Valor = valor,
            Data = DateTime.UtcNow,
            Status = StatusDoacao.Pendente
        };

        await _doacaoRepository.Adicionar(doacao);

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

        return preference.InitPoint; // URL do checkout
    }

    public async Task ConfirmarDoacaoAsync(Guid doacaoId)
    {
        var doacao = await _doacaoRepository.ObterPorId(doacaoId);
        if (doacao == null) return;

        doacao.Status = StatusDoacao.Confirmada;
        await _doacaoRepository.Atualizar(doacao);

        var campanha = await _campanhaRepository.GetCampanhaById(doacao.CampanhaId);
        if (campanha != null)
        {
            campanha.ValorArrecadado += doacao.Valor;
            await _campanhaRepository.UpdateCampanha(campanha);
        }
    }
}
