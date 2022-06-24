using AgenciaBancaria.Dominio;
using System;
using System.Threading;

namespace AgenciaBancaria.App
{
    class Program
    {

        static void Main(string[] args)
        {                
            Console.Clear();

            try
            {
                Console.WriteLine("Por favor, informe seu nome completo");
                Console.WriteLine();
                string nome = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Por favor, informe seu CPF");
                Console.WriteLine();
                string cpf = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Por favor, informe seu RG");
                Console.WriteLine();
                string rg = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Por favor, informe seu endereço");
                Console.WriteLine();
                string logradouro = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Por favor, informe seu CEP");
                Console.WriteLine();
                string cep = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Por favor, informe seu município");
                Console.WriteLine();
                string cidade = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Por favor, informe seu estado");
                Console.WriteLine();
                string estado = Console.ReadLine();
                Console.Clear();

                // Criação da conta
                Endereco endereco = new Endereco(logradouro, cep, cidade, estado);
                Cliente cliente = new Cliente(nome, cpf, rg, endereco);
                ContaCorrente conta = new ContaCorrente(cliente, 500);

                Console.WriteLine("Conta " + conta.Situacao + ": " + conta.NumeroConta + "-" +
                    conta.DigitoVerificador);

                // Abertura de conta
                Console.WriteLine("Por favor, crie sua senha: ");
                Console.Write("> ");
                string senha = Console.ReadLine();
                Console.Clear();
                conta.Abrir(senha);

                Console.WriteLine("Conta " + conta.Situacao + ": " + conta.NumeroConta + "-" +
                    conta.DigitoVerificador);

                Thread.Sleep(2000);
                Console.Clear();

                string operacao = ObterOpcao();

                while (operacao.ToUpper() != "X")
                {

                    switch (operacao)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Valor do depósito: ");
                            Console.Write("> ");
                            conta.Depositar(decimal.Parse(Console.ReadLine()));
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("Valor do saque: ");
                            Console.Write("> ");
                            decimal valor = decimal.Parse(Console.ReadLine());
                            Console.WriteLine();
                            Console.WriteLine("Confirme a senha: ");
                            senha = Console.ReadLine();
                            conta.Sacar(valor, senha);
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine(conta.VerSaldo());
                            Console.ReadLine();
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine(conta.VerExtrato());
                            Console.ReadLine();
                            break;
                        case "5":
                            Console.Clear();
                            Console.WriteLine("Confirme a senha: ");
                            senha = Console.ReadLine();
                            conta.Fechar(senha);

                            Console.WriteLine("Conta " + conta.Situacao + ": " + conta.NumeroConta + "-" +
                    conta.DigitoVerificador + " em " + conta.DataEncerramento);

                            Console.ReadLine();
                            Console.WriteLine("Volte Sempre!");
                            Console.WriteLine();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            break;
                    }

                    operacao = ObterOpcao();
                }
                Console.WriteLine("Volte Sempre!");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static string ObterOpcao()
        {
            Console.Clear();
            Console.WriteLine("Qual operação deseja realizar? ");
            Console.WriteLine("1 - Depósito");
            Console.WriteLine("2 - Saque");
            Console.WriteLine("3 - Ver Saldo");
            Console.WriteLine("4 - Ver Extrato");
            Console.WriteLine("5 - Fechar Conta");
            Console.WriteLine("X - Cancelar");
            Console.WriteLine();
            string operacao = Console.ReadLine().ToUpper();
            return operacao;
        }
    }
}
