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
            Gerador gerador = new Gerador();
            List<Aeroporto> aeroportosLista = gerador.RetornarListaAeroportos();
            List<Voo> vooLista = gerador.RetornarListaVoo();
            List<Cliente> clienteLista = gerador.RetornarListaClientes();
            Voo voo = new Voo();
            MenuOpcoes menuOpcoes = new MenuOpcoes();
            Relatorios relatorio = new Relatorios(clienteLista, vooLista, aeroportosLista);

            string opcao;


            Cabecalho();

            bool sair = false;

            while (!sair)
            {
                opcao = Menu();
                switch (opcao)
                {

                    case "A":
                        Espeçamento();
                        relatorio.MostrarRelatorios("A", relatorio);
                        break;


                    case "B":
                        Espeçamento();
                        relatorio.MostrarRelatorios("B", relatorio);
                        break;

                    case "C":
                        Espeçamento();
                        relatorio.MostrarRelatorios("C", relatorio);
                        break;

                    case "D":
                        Espeçamento();
                        relatorio.MostrarRelatorios("D", relatorio);
                        break;

                    case "F":
                        Espeçamento();
                        relatorio.MostrarRelatorios("F", relatorio);
                        break;

                    case "G":
                        Espeçamento();
                        relatorio.MostrarRelatorios("G", relatorio);
                        break;

                    case "I":
                        Espeçamento();
                        relatorio.MostrarRelatorios("I", relatorio);
                        break;

                    case "J":
                        Espeçamento();
                        relatorio.MostrarRelatorios("J", relatorio);

                        break;

                    case "1":
                        Espeçamento();
                        menuOpcoes.CadastrarCliente(clienteLista, aeroportosLista, vooLista);
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
                        menuOpcoes.CadastrarAeroporto(aeroportosLista);
                        break;
                    case "4":
                        Espeçamento();
                        menuOpcoes.ListarAeroportos(aeroportosLista);
                        break;
                    case "5":
                        Espeçamento();
                        menuOpcoes.CadastrarVoo(aeroportosLista, vooLista);
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

                    case "7":
                        sair = true;
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
            Console.WriteLine("7 - SAIR");
            Console.Write("Digite sua escolha: ");
            return Console.ReadLine().ToUpper();
        }
    }
}