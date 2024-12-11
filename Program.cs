using poo_tp_2024_2_deus_na_frente;
using poo_tp_2024_2_deus_na_frente.codigo;
using SimViaje.AgenciaV1;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading.Channels;

namespace Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            Cliente cliente = new Cliente();
            Gerador gerador = new Gerador();
            List<Aeroporto> aeroportosLista = gerador.RetornarListaAeroportos();
            List<Voo> vooLista = gerador.RetornarListaVoo();
            List<Cliente> clienteLista = gerador.RetornarListaClientes();
            Voo voo = new Voo();
            Relatorio relat = new Relatorio();

            string opcao;
            string nomeCliente, nomeAeroporto, codigoAeroporto;


            Cabecalho();

            bool sair = false;

            while (!sair)
            {
                opcao = Menu();
                switch (opcao)
                {

                    case "A":
                        Espeçamento();
                        relat.ClienteEspecifico(clienteLista);
                        break;


                    case "B":
                        Espeçamento();
                        relat.ClienteAlfabetico(clienteLista);
                        break;

                    case "C":
                        Espeçamento();
                        relat.ClientePorGasto(clienteLista);
                        break;

                    case "D":
                        Espeçamento();
                        relat.VooPorData(vooLista, relat);
                        break;

                    case "F":
                        Espeçamento();
                        Console.WriteLine(relat.ClienteMaiorGasto(clienteLista));
                        break;

                    case "G":
                        Espeçamento();
                        Console.WriteLine(relat.BilheteMaisCaro(clienteLista));
                        break;

                    case "I":
                        Espeçamento();
                        relat.VoosMaiorQuantidade(vooLista);
                        break;

                    case "J":
                        Espeçamento();
                        relat.ValorArrecadadoBilhetes(vooLista, clienteLista);
                        break;

                    case "1":
                        Espeçamento();
                        CadastrarCliente(clienteLista, aeroportosLista, vooLista);
                        break;


                    case "2":
                        Espeçamento();
                        Console.WriteLine("Clientes Lista:");
                        foreach (Cliente cli in clienteLista)
                        {
                            Console.WriteLine(cli);
                            Console.WriteLine();
                        }
                        break;
                    case "3":
                        Espeçamento();
                        Console.WriteLine("Insira o nome do Aeroporto");
                        nomeAeroporto = Console.ReadLine();
                        Console.WriteLine("Insira o código do Aeroporto");
                        codigoAeroporto = Console.ReadLine();
                        Aeroporto aeroporto = new Aeroporto(nomeAeroporto, codigoAeroporto);
                        aeroportosLista.Add(aeroporto);
                        break;
                    case "4":
                        Espeçamento();
                        ListarAeroportos(aeroportosLista);
                        break;               
                    case "5":
                        Espeçamento();
                        Console.WriteLine("Cadastrar voo");
                        Console.WriteLine("Escolha o aeroporto de embarque");

                        for (int i = 0; i < aeroportosLista.Count; i++)
                        {
                            Console.WriteLine($"{i + 1} - {aeroportosLista[i]}");
                        }

                        int opcaoEmbarque = int.Parse(Console.ReadLine()) - 1;
                        Aeroporto aeroportoEmbarque = aeroportosLista[opcaoEmbarque];

                        Console.WriteLine("Escolha o aeroporto de destino:");
                        for (int i = 0; i < aeroportosLista.Count; i++)
                        {
                            if (i != opcaoEmbarque)
                            {
                                Console.WriteLine($"{i + 1} - {aeroportosLista[i]}");
                            }
                        }

                        int opcaoDestino = int.Parse(Console.ReadLine()) - 1;
                        Aeroporto aeroportoDestino = aeroportosLista[opcaoDestino];
                        Console.WriteLine("Digite a data do voo");
                        DateTime dataVoo = DateTime.Now;
                        Console.WriteLine("Digite o preço do voo:");
                        double precoBase = double.Parse(Console.ReadLine());
                        Voo novoVoo = new Voo(dataVoo, aeroportoEmbarque, aeroportoDestino, precoBase);
                        vooLista.Add(novoVoo);
                        break;
                    case "6":
                        Espeçamento();
                        Console.WriteLine("Lista de voos");
                        foreach (Voo v in vooLista)
                        {
                            Console.WriteLine(v);
                            Console.WriteLine();
                        }
                        break;
                }

                if (!sair)
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu");
                    Console.ReadLine();
                    Console.Clear();
                }
                Console.WriteLine();

            }
        }


        static void Cabecalho()
        {
            //Console.Clear();
            Console.WriteLine(" Bem vindo à Não Interessa Airlines");
            Console.WriteLine("=============");

        }

        static void Espeçamento()
        {
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine();

        }

        static string Menu()
        {
            Console.WriteLine("Digite sua opção:");
            Console.WriteLine("A - Consultar dados de um cliente e a seu relatório de compras");
            Console.WriteLine("B - Relatório Resumido de Clientes por ordem Alfabética(Crescente)");
            Console.WriteLine("C - Relatório Resumido de Clientes por ordens de Gastos (Decrescente)");
            Console.WriteLine("D - Relatório de voos filtrados por uma data específica");
            Console.WriteLine("F - Cliente com Maior Gasto");
            Console.WriteLine("G - cartão de embarque do bilhete mais caro vendido pela agência");
            Console.WriteLine("I-  Listagem dos 10 voos com maior quantidade de bilhetes vendidos, em ordem decrescente");
            Console.WriteLine("J - Valor Total Arrecado de Bilhetes de Origem ou Destino");
            Console.WriteLine(">>>>><<<<<");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Listar Clientes");
            Console.WriteLine("3 - Cadastrar Aeroporto");
            Console.WriteLine("4 - Listar Aeroportos");
            Console.WriteLine("5 - Cadastar Voo");
            Console.WriteLine("6 - Listar voos");
            Console.WriteLine("7 - Código Aeroportos");
            Console.Write("Digite sua escolha: ");
            return Console.ReadLine().ToUpper();
        }

        static Bilhete EscolherTipoBilhete()
        {
            Cabecalho();

            Console.WriteLine("Qual será o tipo do Bilhete?");
            Console.WriteLine("1 - Bilhete Promocional");
            Console.WriteLine("2 - Bilhete Prioritário");
            Console.Write("Digite a opção:");
            int opcao = int.Parse(Console.ReadLine());

            return opcao switch
            {
                2 => CriarBilhetePrioritario(),
                1 or _ => CriarBilhetePromocional(),
            };

        }

        static Bilhete CriarBilhetePrioritario()
        {
            Console.WriteLine("Bilhete Prioritário.");
            Console.WriteLine("Quantas Bagagens deseja incluir?");
            int qtd = int.Parse(Console.ReadLine());
            return new Bilhete_Prioritario(qtd);
        }

        static Bilhete CriarBilhetePromocional()
        {
            Console.WriteLine("Bilhete Promocional.");
            return new Bilhete_Promocional();
        }

        static AceleradorSuper ContratarAcelerador()
        {
            Console.WriteLine("Digite a data Inicial do contrato");
            DateTime.TryParse(Console.ReadLine(), out DateTime inicial);
            return new AceleradorSuper(inicial);
        }
        static void TesteBilhetes()
        {
            Aeroporto origem = new Aeroporto("sp", "44");
            Aeroporto destino = new Aeroporto("Aeroporto B", "asc");
            Voo voo = new Voo(DateTime.Now, origem, destino, 300);

            Console.WriteLine($"Bilhetes antes: {voo.BilhetesVendidos()}");

            for (int i = 0; i < 10; i++)
            {
                voo.VenderTrecho();
            }

            Console.WriteLine($"Bilhetes após venda: {voo.BilhetesVendidos()}");
        }

        static void ListarAeroportos(List<Aeroporto> lista)
        {
            foreach (Aeroporto a in lista)
            {
                Console.WriteLine(a);
                Console.WriteLine($"Código: {a.GetHashCode()}");
            }
        }

        static void ListarVoos(List<Voo> voos)
        {
            foreach (Voo voo in voos)
            {
                Console.WriteLine(voo);
            }
        }

        static void CadastrarCliente(List<Cliente>clienteLista, List<Aeroporto>aeroportosLista, List<Voo> voosLista)
        {
            Console.WriteLine("Insira o nome do cliente");
            string nomeCliente = Console.ReadLine();
            Cliente novoCliente = new Cliente(nomeCliente);
            Espeçamento();
            Console.WriteLine("Área de compra do bilhete\n");
            int escolha = -1;
            do
            {
                ListarVoos(voosLista);
                Console.WriteLine("Digite o código do Voo que desejas");
                int escolhaVoo = int.Parse(Console.ReadLine());
                Voo vooCliente = voosLista.FirstOrDefault(v => v.GetHashCode() == escolhaVoo);
                Trecho trechoCliente = vooCliente.VenderTrecho();

                Bilhete bilheteCliente = EscolherTipoBilhete();
                bilheteCliente.AddTrecho(trechoCliente);
                novoCliente.AdicionarBilhete(bilheteCliente);

                Console.WriteLine("Deseja comprar outro bilhete?");
                Console.WriteLine("1 - SIM");
                Console.WriteLine("2 - NÃO");
                escolha = int.Parse(Console.ReadLine());
                Espeçamento();
            } while (escolha != 2);

            Console.WriteLine("Área de compra de Aceleradores");
            int escolha2 = -1;
            Console.WriteLine("Você deseja contratar o Acelerador?");
            Console.WriteLine("1 - Sim");
            Console.WriteLine("2 - Não");
            escolha2 = int.Parse(Console.ReadLine());
            while(escolha2 != 2)
            {
                AceleradorSuper aceleradorCliente = ContratarAcelerador();
                novoCliente.ContratarAcelerador(aceleradorCliente);
                Console.WriteLine("Deseja contratar outro Acelerador?");
                Console.WriteLine("1 - Sim");
                Console.WriteLine("0 - Não");
                escolha2 = int.Parse(Console.ReadLine());
            }

                clienteLista.Add(novoCliente);
                Console.WriteLine("Novo cliente criado com sucesso!");
            }
    }
}