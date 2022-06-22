using System;
using System.Threading;

namespace Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main()
        {
            Console.Clear();

            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
						Console.WriteLine("Opção Invalida!");
						Thread.Sleep(1000);
						Console.Clear();
						break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Sair();
        }

        private static void Sair()
        {
            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.ResetColor();
			Environment.Exit(0);
        }

        private static void ExcluirSerie()
		{
			Console.Clear();
			Tit();
			Console.Write("Digite o id da série: ");

			var op = Console.ReadLine();

			if(op.ToUpper() == "X"){
				Sair();
			} else if(op.ToUpper() == "C"){
				Main();
			} else if(int.TryParse(op, out int indiceSerie)){
				repositorio.Exclui(indiceSerie);
				Console.Clear();
				Tit();
				Console.WriteLine("Série excluída com sucesso");
				Thread.Sleep(1000);
				Console.Clear();
			} else {
				Console.Clear();
				Tit();
				Console.WriteLine("Opção Invalida!");
				Thread.Sleep(1000);
				Console.Clear();
			}
		}

        private static void VisualizarSerie()
        {
            Console.Clear();
            Tit();
            Console.Write("Digite o id da série: ");

            var op = Console.ReadLine();

            if (op.ToUpper() == "X")
            {
                Sair();
            }
            else if (op.ToUpper() == "C")
            {
                Main();
            }
            else if (int.TryParse(op, out int indiceSerie))
            {
                Console.Clear();
                Tit();
                var serie = repositorio.RetornaPorId(indiceSerie);
                Console.WriteLine(serie);
            } else {
				Console.Clear();
				Console.WriteLine("Opção Invalida!");
				Thread.Sleep(1000);
				Main();
			}

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
            Console.Clear();
        }

        private static void Option(string op)
        {
            if (op.ToUpper() == "X")
            {
                Sair();
            }
            else if (op.ToUpper() == "C")
            {
                Main();
            }else if (!int.TryParse(op, out int a))
            {
				Console.Clear();
                Tit();
                Console.WriteLine("Opção Invalida!");
				Thread.Sleep(1000);
				Console.Clear();
				Main();
            }
        }

        private static void AtualizarSerie()
		{
			Console.Clear();
			Tit();
			Console.Write("Digite o id da série: ");
			var opp = Console.ReadLine();

			Option(opp);

			int indiceSerie = int.Parse(opp);

			Console.Clear();
			Tit();
			Console.WriteLine("Alterar para o Gênero:");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.WriteLine();

			var op = Console.ReadLine().ToUpper();

			if(op.ToUpper() == "X"){
				Sair();
			} else if(op.ToUpper() == "C"){
				Main();
			} else if(int.TryParse(op, out int entradaGenero)){
				Console.Clear();
				Tit();
				Console.Write("Digite o Título da Série: ");
				string entradaTitulo = Console.ReadLine();

				Console.Clear();
				Tit();
				Console.Write("Digite o Ano de Início da Série: ");
				opp = Console.ReadLine();

				Option(opp);

				int entradaAno = int.Parse(opp);

				Console.Clear();
				Tit();
				Console.Write("Digite a Descrição da Série: ");
				string entradaDescricao = Console.ReadLine();

				Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

				repositorio.Atualiza(indiceSerie, atualizaSerie);
				Console.Clear();
			} else {
				Console.Clear();
				Tit();
				Console.WriteLine("Opção Inválida!");
				Thread.Sleep(1000);
				Console.Clear();
			}
		}
        private static void ListarSeries()
		{
			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.Clear();
				Tit();
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Sem séries no momento");
				Thread.Sleep(1000);
				Console.Clear();
				return;
			}

			Console.Clear();
			Tit();
			Console.WriteLine("Listar séries");

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Press Enter...");
			Console.ReadLine();
			Console.Clear();
		}

        private static void InserirSerie()
		{
			Console.Clear();
			Tit();
			Console.WriteLine("Genero:");
			Console.WriteLine();

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-  {1}", i, Enum.GetName(typeof(Genero), i));
			}
			Console.WriteLine();

			var op = Console.ReadLine().ToUpper();

			if(op.ToUpper() == "X"){
				Sair();
			} else if(op.ToUpper() == "C"){
				Main();
			} else if(int.TryParse(op, out int entradaGenero)){
				Console.Clear();
				Tit();
				Console.Write("Digite o Título da Série: ");
				string entradaTitulo = Console.ReadLine();

				Console.Clear();
				Tit();
				Console.Write("Digite o Ano de Início da Série: ");

				var opp = Console.ReadLine();

				Option(opp);

				int entradaAno = int.Parse(opp);

				Console.Clear();
				Tit();
				Console.Write("Digite a Descrição da Série: ");
				string entradaDescricao = Console.ReadLine();

				Serie novaSerie = new Serie(id: repositorio.ProximoId(),
											genero: (Genero)entradaGenero,
											titulo: entradaTitulo,
											ano: entradaAno,
											descricao: entradaDescricao);

				repositorio.Insere(novaSerie);
				Console.Clear();
			} else {
				Console.Clear();
				Tit();
				Console.WriteLine("Opção Inválida!");
				Thread.Sleep(1000);
				Console.Clear();
			}
		}

        private static string ObterOpcaoUsuario()
        {
            Tit();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine();

            Console.WriteLine("1-  Listar séries");
            Console.WriteLine("2-  Inserir nova série");
            Console.WriteLine("3-  Atualizar série");
            Console.WriteLine("4-  Excluir série");
            Console.WriteLine("5-  Visualizar série");
            Console.WriteLine("C-  Limpar Tela");
            Console.WriteLine("X-  Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void Tit()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;

            string tit = "DIO SÉRIES";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (tit.Length / 2)) + "}", tit));
            Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
