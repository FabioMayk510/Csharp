using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaBancaria.Dominio
{
    public class ContaCorrente : ContaBancaria
    {
        public ContaCorrente(Cliente cliente, decimal limite): base(cliente)
        {
            ValorTaxaManutencao = 0.05M;
            Limite = limite;
        }

        public override void Sacar(decimal valor, string senha)
        {
            while(senha != Senha){
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Senha Incorreta, tente novamente.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                senha = Console.ReadLine();
            }

            var saque = new Saque(valor, DateTime.Now, this);

            var valorMaximoSaque = Saldo + Limite;

            if (valorMaximoSaque < saque.Valor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Saldo indisponível.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadLine();
            } else {
                Saldo -= saque.Valor;
                Lancamentos.Add(saque);
            }
        }

        public override string VerExtrato()
        {
            var sb = new StringBuilder();

            sb.Append(base.VerExtrato());

            sb.AppendLine("Limite:        R$ " + Limite);
            sb.AppendLine("Saldo+Limite:  R$ " + (Limite + Saldo));

            return sb.ToString();
        }

        public decimal Limite { get; private set; }

        public decimal ValorTaxaManutencao { get; private set; }
    }
}
