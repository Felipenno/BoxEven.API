using BE.Domain.Entities;
using BE.Domain.Enum;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Options;
using BE.Domain.Model;
using BE.Domain.Interfaces.Repository;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
//using System.Text.Json;

namespace BE.Infra;

public class VendasContext : IVendasContext
{
    private readonly IOptions<VendasContextSettings> _vendasContextSettings;
    private readonly IHttpClientFactory _httpClient;

    public VendasContext(IHttpClientFactory httpClient, IOptions<VendasContextSettings> vendasContextSettings )
    { 
        _vendasContextSettings = vendasContextSettings;
        _httpClient = httpClient;
    }

    public async Task<List<Pedido>> ListarPedidos()
    {
        var http = _httpClient.CreateClient();
        http.BaseAddress = new Uri(_vendasContextSettings.Value.BaseUrl);
        http.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

        var response = await http.GetAsync("api/pedido");
        if (response.IsSuccessStatusCode)
        {
            var dados = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PedidoModel>>(dados);
            return PedidoModel.ToPedidoEntityList(result);

            //using var dados = await response.Content.ReadAsStreamAsync();
            //var result = await JsonSerializer.DeserializeAsync<List<Pedido>>(dados);
            //return result;
        }

        return new List<Pedido>();
    }

    public async Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao)
    {

        var http = _httpClient.CreateClient();
        http.BaseAddress = new Uri(_vendasContextSettings.Value.BaseUrl);
        http.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

        var response = await http.GetAsync($"api/pedido/filtro?status={status}&conclusao={conclusao}");
        if (response.IsSuccessStatusCode)
        {
            var dados = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PedidoModel>>(dados);
            return PedidoModel.ToPedidoEntityList(result);
        }

        return new List<Pedido>();
    }

    public async Task AlterarStatusPedidoAsync(string id, StatusPedido status)
    {
        var http = _httpClient.CreateClient();
        http.BaseAddress = new Uri(_vendasContextSettings.Value.BaseUrl);
        http.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");

        var objetcContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(status), Encoding.UTF8, Application.Json);

        using var response = await http.PatchAsync($"api/pedido/{id}", objetcContent);
        response.EnsureSuccessStatusCode();
    }
}
