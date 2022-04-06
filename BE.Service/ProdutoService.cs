using AutoMapper;
using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;

namespace BE.Service;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILocalizacaoRepository _localizacaoRepository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository produtoRepository, ILocalizacaoRepository localizacaoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _localizacaoRepository = localizacaoRepository;
        _mapper = mapper;
    }

    public async Task<List<ProdutoListarDto>> ListarProdutosAsync(bool? status, int? id, string? nome)
    {
        var produtoDtoList = new List<ProdutoListarDto>();

        var produtos = await _produtoRepository.ListarTodosAsync(status, id, nome);
        if (produtos != null)
        {
            foreach (Produto item in produtos)
            {
                string imagemBase64 = null;

                if (!(string.IsNullOrEmpty(item.ImagemNome) || string.IsNullOrEmpty(item.ImagemTipo)))
                {
                    string nomeImagem = item.ImagemNome;
                    string metaDados = item.ImagemTipo;

                    string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagem);

                    if (File.Exists(caminhoImagem))
                    {
                        byte[] imgBytes = await File.ReadAllBytesAsync(caminhoImagem);

                        imagemBase64 = metaDados + Convert.ToBase64String(imgBytes);

                        //using (var fileStream = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read))
                        //{
                        //    byte[] bytes = new byte[fileStream.Length];
                        //    int numBytesToRead = (int)fileStream.Length;
                        //    int numBytesRead = 0;
                        //    while (numBytesToRead > 0)
                        //    {
                        //        // Read may return anything from 0 to numBytesToRead.
                        //        int n = fileStream.Read(bytes, numBytesRead, numBytesToRead);

                        //        // Break when the end of the file is reached.
                        //        if (n == 0)
                        //            break;

                        //        numBytesRead += n;
                        //        numBytesToRead -= n;
                        //    }

                        //    imagemBase64 = metaDados + Convert.ToBase64String(bytes);
                        //}
                    }
                }

                var produtoDto = new ProdutoListarDto()
                {
                    ProdutoId = item.ProdutoId,
                    Ativo = item.Ativo,
                    Quantidade = item.Quantidade,
                    Preco = item.Preco,
                    Nome = item.Nome,
                    Imagem = imagemBase64,
                    CodigoBarras = item.CodigoBarras,
                    Criacao = item.Criacao,
                    Atualizacao = item.Atualizacao,
                    UnidadeMedida = new UnidadeMedidaDto()
                    {
                        UnidadeMedidaId = item.UnidadeMedida.UnidadeMedidaId,
                        Descricao = item.UnidadeMedida.Descricao
                    }
                };

                if (item.Localizacoes.Count > 0 && item.Localizacoes[0] != null)
                {
                    produtoDto.Localizacoes = new List<LocalizacaoEnderecoMontadoDto>();

                    foreach (var localizacao in item.Localizacoes)
                    {
                        produtoDto.Localizacoes.Add(new LocalizacaoEnderecoMontadoDto()
                        {
                            LocalizacaoId = localizacao.LocalizacaoId,
                            Endereco = localizacao.MontarEndereco()
                        });
                    }
                }

                produtoDtoList.Add(produtoDto);
            }
        }

        return produtoDtoList;
    }

    public async Task<List<ProdutoListarDto>> ListarProdutosDesalocadosAsync()
    {
        var produtoDtoList = new List<ProdutoListarDto>();

        var produtos = await _produtoRepository.ListarTodosDesalocadosAsync();
        if (produtos != null)
        {
            foreach (Produto item in produtos)
            {
                string imagemBase64 = null;

                if (!(string.IsNullOrEmpty(item.ImagemNome) || string.IsNullOrEmpty(item.ImagemTipo)))
                {
                    string nomeImagem = item.ImagemNome;
                    string metaDados = item.ImagemTipo;

                    string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagem);

                    if (File.Exists(caminhoImagem))
                    {
                        byte[] imgBytes = await File.ReadAllBytesAsync(caminhoImagem);

                        imagemBase64 = metaDados + Convert.ToBase64String(imgBytes);
                    }
                }

                var produtoDto = new ProdutoListarDto()
                {
                    ProdutoId = item.ProdutoId,
                    Ativo = item.Ativo,
                    Quantidade = item.Quantidade,
                    Preco = item.Preco,
                    Nome = item.Nome,
                    Imagem = imagemBase64,
                    CodigoBarras = item.CodigoBarras,
                    Criacao = item.Criacao,
                    Atualizacao = item.Atualizacao,
                    UnidadeMedida = new UnidadeMedidaDto()
                    {
                        UnidadeMedidaId = item.UnidadeMedida.UnidadeMedidaId,
                        Descricao = item.UnidadeMedida.Descricao
                    }
                };

                if (item.Localizacoes.Count > 0 && item.Localizacoes[0] != null)
                {
                    produtoDto.Localizacoes = new List<LocalizacaoEnderecoMontadoDto>();

                    foreach (var localizacao in item.Localizacoes)
                    {
                        produtoDto.Localizacoes.Add(new LocalizacaoEnderecoMontadoDto()
                        {
                            LocalizacaoId = localizacao.LocalizacaoId,
                            Endereco = localizacao.MontarEndereco()
                        });
                    }
                }

                produtoDtoList.Add(produtoDto);
            }
        }

        return produtoDtoList;
    }

    public async Task<ProdutoListarDto> ListarPorIdAsync(int produtoId)
    {
        ProdutoListarDto produtoDto = null;

        var produto = await _produtoRepository.ListarPorIdAsync(produtoId);
        if (produto != null)
        {
            string imagemBase64 = null;

            if (!(string.IsNullOrEmpty(produto.ImagemNome) || string.IsNullOrEmpty(produto.ImagemTipo)))
            {
                string nomeImagem = produto.ImagemNome;
                string metaDados = produto.ImagemTipo;

                string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagem);

                if (File.Exists(caminhoImagem))
                {
                    byte[] imgBytes = await File.ReadAllBytesAsync(caminhoImagem);

                    imagemBase64 = metaDados + Convert.ToBase64String(imgBytes);
                }
            }

            produtoDto = new ProdutoListarDto();
            produtoDto.ProdutoId = produto.ProdutoId;
            produtoDto.Ativo = produto.Ativo;
            produtoDto.Quantidade = produto.Quantidade;
            produtoDto.Preco = produto.Preco;
            produtoDto.Nome = produto.Nome;
            produtoDto.Imagem = imagemBase64;
            produtoDto.CodigoBarras = produto.CodigoBarras;
            produtoDto.Criacao = produto.Criacao;
            produtoDto.Atualizacao = produto.Atualizacao;
            produtoDto.UnidadeMedida = new UnidadeMedidaDto()
            {
                UnidadeMedidaId = produto.UnidadeMedida.UnidadeMedidaId,
                Descricao = produto.UnidadeMedida.Descricao
            };

            if (produto.Localizacoes.Count > 0 && produto.Localizacoes[0] != null)
            {
                produtoDto.Localizacoes = new List<LocalizacaoEnderecoMontadoDto>();

                foreach (var localizacao in produto.Localizacoes)
                {
                    produtoDto.Localizacoes.Add(new LocalizacaoEnderecoMontadoDto()
                    {
                        LocalizacaoId = localizacao.LocalizacaoId,
                        Endereco = localizacao.MontarEndereco()
                    });
                }
            }
        }

        return produtoDto;
    }

    public async Task<bool> CriarProdutoAsync(ProdutoCriarDto produtoDto)
    {
        Produto produto;
        bool concluido;

        if (!string.IsNullOrEmpty(produtoDto.Imagem))
        {
            string tipoImagemDados = produtoDto.Imagem.Split(',').First() + ",";
            string tipoImagem = "." + tipoImagemDados.Split(';').First().Split('/').Last();
            string base64Imagem = produtoDto.Imagem.Split("base64,").Last();
            string nomeProduto = new string(produtoDto.Nome.Take(10).ToArray()).ToLower().Replace(" ", "");
            string nomeImagem = $"{nomeProduto}{DateTime.Now:yymmssfff}{tipoImagem}";
            string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagem);

            produto = new Produto(produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, tipoImagemDados, nomeImagem, produtoDto.CodigoBarras, DateTime.Now, DateTime.Now, produtoDto.UnidadeMedidaId);

            concluido = await _produtoRepository.AdicionarAsync(produto);

            if (concluido)
            {
                byte[] imagemBytes = Convert.FromBase64String(base64Imagem);

                using (var fileStream = new FileStream(caminhoImagem, FileMode.CreateNew))
                {
                    for (int i = 0; i < imagemBytes.Length; i++)
                    {
                        fileStream.WriteByte(imagemBytes[i]);
                    }
                }
            }

            return concluido;
        }

        produto = new Produto(produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, null, null, produtoDto.CodigoBarras, DateTime.Now, DateTime.Now, produtoDto.UnidadeMedidaId);

        concluido = await _produtoRepository.AdicionarAsync(produto);
        return concluido;
    }

    public async Task<bool> AtualizarProdutoAsync(ProdutoEditarDto produtoDto, int id)
    {
        var produtoExistente = await _produtoRepository.ListarPorIdAsync(id);
        if (produtoExistente == null || produtoExistente.ProdutoId != produtoDto.ProdutoId)
        {
            return false;
        }

        Produto produto;
        bool concluido;

        if (!string.IsNullOrEmpty(produtoDto.Imagem))
        {
            string tipoImagemDados = produtoDto.Imagem.Split(',').First() + ",";
            string tipoImagem = "." + tipoImagemDados.Split(';').First().Split('/').Last();
            string base64Imagem = produtoDto.Imagem.Split("base64,").Last();
            string nomeProduto = new string(produtoDto.Nome.Take(10).ToArray()).ToLower().Replace(" ", "");
            string nomeImagem = $"{nomeProduto}{DateTime.Now:yymmssfff}{tipoImagem}";
            string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagem);

            produto = new Produto(produtoDto.ProdutoId, produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, tipoImagemDados, nomeImagem, produtoDto.CodigoBarras, DateTime.Now, produtoDto.UnidadeMedidaId);

            concluido = await _produtoRepository.AtualizarAsync(produto);

            if (concluido)
            {
                if (!(string.IsNullOrEmpty(produtoExistente.ImagemNome) || string.IsNullOrEmpty(produtoExistente.ImagemTipo)))
                {
                    string nomeImagemAnterior = produtoExistente.ImagemNome;
                    string caminhoImagemAnterior = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagemAnterior);

                    if (File.Exists(caminhoImagemAnterior))
                    {
                        File.Delete(caminhoImagemAnterior);
                    }
                }

                byte[] imagemBytes = Convert.FromBase64String(base64Imagem);

                using (var fileStream = new FileStream(caminhoImagem, FileMode.CreateNew))
                {
                    for (int i = 0; i < imagemBytes.Length; i++)
                    {
                        fileStream.WriteByte(imagemBytes[i]);
                    }
                }

                if (produtoExistente.Localizacoes != null || produtoDto.Localizacoes != null)
                {
                    var localizacoesIO = produtoExistente.TrocarLocalizacoes(produtoDto.Localizacoes);

                    if (localizacoesIO.LocalizacoesAdicionadas != null)
                    {
                        await _localizacaoRepository.AlocarProdutoAsync(localizacoesIO.LocalizacoesAdicionadas, produtoDto.ProdutoId);
                    }

                    if (localizacoesIO.LocalizacoesRemovidas != null)
                    {
                        await _localizacaoRepository.DesalocarProdutoAsync(localizacoesIO.LocalizacoesRemovidas);
                    }
                }

            }

            return concluido;
        }

        produto = new Produto(produtoDto.ProdutoId, produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, null, null, produtoDto.CodigoBarras, DateTime.Now, produtoDto.UnidadeMedidaId);

        concluido = await _produtoRepository.AtualizarAsync(produto);

        if (concluido)
        {
            if (!(string.IsNullOrEmpty(produtoExistente.ImagemNome) || string.IsNullOrEmpty(produtoExistente.ImagemTipo)) && string.IsNullOrEmpty(produtoDto.Imagem))
            {
                string nomeImagemAnterior = produtoExistente.ImagemNome;
                string caminhoImagemAnterior = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagemAnterior);

                if (File.Exists(caminhoImagemAnterior))
                {
                    File.Delete(caminhoImagemAnterior);
                }
            }

            if (produtoExistente.Localizacoes != null || produtoDto.Localizacoes != null)
            {
                var localizacoesIO = produtoExistente.TrocarLocalizacoes(produtoDto.Localizacoes);

                if (localizacoesIO.LocalizacoesAdicionadas != null)
                {
                    await _localizacaoRepository.AlocarProdutoAsync(localizacoesIO.LocalizacoesAdicionadas, produtoDto.ProdutoId);
                }

                if (localizacoesIO.LocalizacoesRemovidas != null)
                {
                    await _localizacaoRepository.DesalocarProdutoAsync(localizacoesIO.LocalizacoesRemovidas);
                }
            }
        }
        return concluido;
    }
}