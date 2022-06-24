using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgenciaBancaria.Dominio
{
    public abstract class ContaBancaria
    {
        public ContaBancaria(Cliente cliente)
        {
            Random random = new Random();
            NumeroConta = random.Next(50000, 100000);
            DigitoVerificador = random.Next(0, 9);

            Situacao = SituacaoConta.Criada;

            Cliente = cliente ?? throw new Exception("Cliente deve ser informado.");
        }

        public void Abrir(string senha)
        {
            SetaSenha(senha);

            Situacao = SituacaoConta.Aberta;
            DataAbertura = DateTime.Now;
            Lancamentos = new List<Lancamento>();
        }

        public void Fechar(string senha)
        {
            while(senha != Senha){
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Senha Incorreta, tente novamente.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                senha = Console.ReadLine();
            }
            Situacao = SituacaoConta.Encerrada;
            DataEncerramento = DateTime.Now;

            Console.Clear();
        }

        private void SetaSenha(string senha)
        {
            senha = senha.ValidaStringVazia();

            // Minimum eight characters, at least one letter and one number
            while(!Regex.IsMatch(senha, @"^(?=.*?[a-z])(?=.*?[0-9]).{8,}$")){
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Senha inválida. Por favor, siga o padrão de senha aceitável");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");
                senha = Console.ReadLine();
                Console.Clear();
            }
            Senha = senha;
        }

        public void Depositar(decimal valor)
        {
            var deposito = new Deposito(valor, DateTime.Now, this);

            Saldo += deposito.Valor;
            Lancamentos.Add(deposito);
        }

        public virtual void Sacar(decimal valor, string senha)
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

            if (Saldo < saque.Valor)
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

        public string VerSaldo()
        {
            return $"Saldo atual: R$ {Saldo}";
        }

        public virtual string VerExtrato()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"-- Extrato - Lançamentos --");

            foreach (var lancamento in Lancamentos)
            {
                sb.Append(lancamento.GetType().Name + "  -->  ");
                sb.Append(lancamento.Data.ToString("dd/MM/yyyy hh:mm:ss" + "   -->  "));

                if (lancamento is Saque){
                    sb.Append(" - ");
                }

                if (lancamento is Deposito){
                    sb.Append(" + ");
                }

                sb.Append("R$ ");
                sb.AppendLine(lancamento.Valor.ToString());
            }

            sb.AppendLine("Saldo final:   R$ " + Saldo);

            return sb.ToString();
        }

        public int NumeroConta { get; init; }
        public int DigitoVerificador { get; init; }
        public decimal Saldo { get; protected set; }
        public DateTime? DataAbertura { get; private set; }
        public DateTime? DataEncerramento { get; private set; }
        public SituacaoConta Situacao { get; private set; }
        public string Senha { get; private set; }
        public Cliente Cliente { get; init; }
        public List<Lancamento> Lancamentos { get; private set; }
    }
}
