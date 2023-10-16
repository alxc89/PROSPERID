namespace PROSPERID.Domain.Entities;
public class Budget : Entity
{
    public Budget(string nome, DateTime dataInicio, DateTime dataFim, List<Category> categorias, List<FinancialMovement> transacoes)
    {
        Nome = nome;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Categorias = categorias;
        Transacoes = transacoes;
    }

    // Propriedades
    public string Nome { get; set; } // Nome do orçamento (por exemplo, "Janeiro 2023")
    public DateTime DataInicio { get; set; } // Data de início do período orçamentário
    public DateTime DataFim { get; set; } // Data de término do período orçamentário
    public List<Category> Categorias { get; set; } // Lista de categorias de despesas no orçamento
    public List<FinancialMovement> Transacoes { get; set; } // Lista de transações relacionadas ao orçamento

    // Métodos
    public decimal CalcularSaldoDisponivel()
    {
        // Implemente a lógica para calcular o saldo disponível com base nas transações e categorias
        decimal saldo = 0;

        // Cálculos aqui...

        return saldo;
    }

    public void AdicionarTransacao(FinancialMovement transacao)
    {
        // Implemente a lógica para adicionar uma transação ao orçamento
        Transacoes.Add(transacao);
    }

    public void AdicionarCategoria(Category categoria)
    {
        // Implemente a lógica para adicionar uma categoria ao orçamento
        Categorias.Add(categoria);
    }

    // Outros métodos e funcionalidades conforme necessário
}
