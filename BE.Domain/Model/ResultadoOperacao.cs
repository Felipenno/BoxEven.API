namespace BE.Domain.Entities;

public class ResultadoOperacao
{
    public bool Sucesso { get; set; }
    public string? ErroMsg { get; set; }

    public ResultadoOperacao(){}

    public ResultadoOperacao(bool sucesso, string? erroMsg = null)
    {
        Sucesso = sucesso;
        ErroMsg = erroMsg;
    }
}