using poo_tp_2024_2_deus_na_frente.codigo;
using SimViaje.AgenciaV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo_tp_2024_2_deus_na_frente
{
    public class MenuOpcoes
    {
        Relatorios relatorio;
        public MenuOpcoes()
        {

        }
        //OPCAO 1 - Cadastrar Cliente
        public void CadastrarCliente(List<Cliente> clienteLista, List<Aeroporto> aeroportosLista, List<Voo> voosLista)
        {
            Console.WriteLine("Insira o nome do cliente:");

            string nomeCliente = Console.ReadLine();


            Cliente novoCliente = new Cliente(nomeCliente);
            Console.WriteLine("Área de compra do bilhete\n");

            List<Trecho> listaTrechosClientes = new List<Trecho>();

            int escolha = -1;
            while (escolha != 2)
            {
                ListarVoos(voosLista);

                int escolhaVoo = -1;
                Voo vooCliente = null;
                Trecho trechoCliente = null;
                int escolhaVooTrecho = -1;
                while (escolhaVooTrecho != 2)
                {
                    bool vooValido = false;
                    while (!vooValido)
                    {
                        try
                        {
                            Console.WriteLine("Digite o código do Voo que desejas:");
                            escolhaVoo = int.Parse(Console.ReadLine());
                            vooCliente = voosLista.FirstOrDefault(v => v.GetHashCode() == escolhaVoo);
                            if (vooCliente == null)
                                throw new Excecao("Voo não encontrado. Por favor, insira um código válido.");
                            vooValido = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Entrada inválida. Digite um número válido para o código do voo.");
                        }
                        catch (Excecao ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    trechoCliente = vooCliente.VenderTrecho();
                    listaTrechosClientes.Add(trechoCliente);
                    try
                    {
                        Console.WriteLine("Deseja adicionar outro trecho?");
                        Console.WriteLine("1 - SIM");
                        Console.WriteLine("2 - NÃO");
                        escolhaVooTrecho = int.Parse(Console.ReadLine());
                        if (escolhaVooTrecho != 1 && escolhaVooTrecho != 2)
                            throw new ArgumentException("Escolha inválida. Digite 1 para SIM ou 2 para NÃO");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Entrada inválida. Digite um número válido.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                
                Bilhete bilheteCliente = EscolherTipoBilhete();

                foreach(Trecho t in listaTrechosClientes)
                {
                    bilheteCliente.AddTrecho(t);
                }
                Console.ReadLine();

                novoCliente.AdicionarBilhete(bilheteCliente);

                bool escolhaValidaBilhete = false;
                while (!escolhaValidaBilhete)
                {
                    try
                    {
                        Console.WriteLine("Deseja comprar outro bilhete?");
                        Console.WriteLine("1 - SIM");
                        Console.WriteLine("2 - NÃO");
                        escolha = int.Parse(Console.ReadLine());
                        if (escolha != 1 && escolha != 2)
                            throw new ArgumentException("Escolha inválida. Digite 1 para SIM ou 2 para NÃO.");
                        escolhaValidaBilhete = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Entrada inválida. Digite um número válido.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            Console.WriteLine("Área de compra de Aceleradores");
            int escolha2 = -1;
            bool escolhaAceleradorValida = false;

            while (!escolhaAceleradorValida)
            {
                try
                {
                    Console.WriteLine("Você deseja contratar o Acelerador?");
                    Console.WriteLine("1 - SIM");
                    Console.WriteLine("2 - NÃO");
                    escolha2 = int.Parse(Console.ReadLine());
                    if (escolha2 != 1 && escolha2 != 2)
                        throw new ArgumentException("Escolha inválida. Digite 1 para SIM ou 2 para NÃO.");
                    escolhaAceleradorValida = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada inválida. Digite um número válido.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while (escolha2 == 1)
            {
                AceleradorSuper aceleradorCliente = ContratarAcelerador();
                novoCliente.ContratarAcelerador(aceleradorCliente);

                escolhaAceleradorValida = false;
                while (!escolhaAceleradorValida)
                {
                    try
                    {
                        Console.WriteLine("Deseja contratar outro Acelerador?");
                        Console.WriteLine("1 - SIM");
                        Console.WriteLine("2 - NÃO");
                        escolha2 = int.Parse(Console.ReadLine());
                        if (escolha2 != 1 && escolha2 != 2)
                            throw new ArgumentException("Escolha inválida. Digite 1 para SIM ou 2 para NÃO.");
                        escolhaAceleradorValida = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Entrada inválida. Digite um número válido.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            clienteLista.Add(novoCliente);
            Console.WriteLine("Novo cliente criado com sucesso!");
        }
        //OPCAO 1 - Cadastrar Cliente

        private void ListarVoos(List<Voo> voos)
        {
            foreach (Voo voo in voos)
            {
                Console.WriteLine(voo);
            }
        }
        //OPCAO 1 - Cadastrar Cliente

        private AceleradorSuper ContratarAcelerador()
        {
            DateTime inicial = DateTime.MinValue;
            bool dataValida = false;
            DateTime dataConvertida = DateTime.MinValue;

            while (!dataValida)
            {
                try
                {
                    Console.WriteLine("Digite a data inicial do contrato (formato: DD/MM/AAAA):");
                    string data = Console.ReadLine();
                    dataConvertida = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                    dataValida = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return new AceleradorSuper(inicial);
        }
        //OPCAO 1 - Cadastrar Cliente

        private Bilhete CriarBilhetePrioritario()
        {
            Console.WriteLine("Bilhete Prioritário.");
            Console.WriteLine("Quantas Bagagens deseja incluir?");
            int qtd = int.Parse(Console.ReadLine());
            return new Bilhete_Prioritario(qtd);
        }
        //OPCAO 1 - Cadastrar Cliente
        private Bilhete EscolherTipoBilhete()
        {
            int opcao = 0;
            bool opcaoValida = false;

            while (!opcaoValida)
            {
                try
                {
                    Console.WriteLine("Qual será o tipo do Bilhete?");
                    Console.WriteLine("1 - Bilhete Promocional");
                    Console.WriteLine("2 - Bilhete Prioritário");
                    Console.Write("Digite a opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao != 1 && opcao != 2)
                    {
                        Console.WriteLine("Opção inválida. Escolha 1 ou 2.");
                    }
                    opcaoValida = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada inválida. Por favor, insira um número inteiro.");
                }
            }

            return opcao switch
            {
                2 => CriarBilhetePrioritario(),
                1 => CriarBilhetePromocional(),
                _ => throw new Exception("Opção Errada.")
            };
        }
        //OPCAO 1 - Cadastrar Cliente
        private Bilhete CriarBilhetePromocional()
        {
            Console.WriteLine("Bilhete Promocional.");
            return new Bilhete_Promocional();
        }

        // OPCAO 3 - CADASTRAR VOO

        public void CadastrarAeroporto(List<Aeroporto> aeroportosLista)
        {
            Console.WriteLine("Insira o nome do Aeroporto");
            string nomeAeroporto = Console.ReadLine();
            Console.WriteLine("Insira o código do Aeroporto");
            string codigoAeroporto = Console.ReadLine();
            Aeroporto aeroporto = new Aeroporto(nomeAeroporto, codigoAeroporto);
            aeroportosLista.Add(aeroporto);
            Console.WriteLine("Aeroporto cadastrado com sucesso");
        }


        //4 - Listar Aeroportos"
        public void ListarAeroportos(List<Aeroporto> lista)
        {
            foreach (Aeroporto a in lista)
            {
                Console.WriteLine(a);
                Console.WriteLine($"Código: {a.GetHashCode()}");
            }
        }

        //Cadastar Voo

        public void CadastrarVoo(List<Aeroporto> aeroportosLista, List<Voo> vooLista)
        {
            try
            {
               
                Console.WriteLine("Cadastrar voo");

                Aeroporto aeroportoEmbarque = null;
                while (aeroportoEmbarque == null)
                {
                    try
                    {
                        Console.WriteLine("Escolha o aeroporto de embarque:");
                        for (int i = 0; i < aeroportosLista.Count; i++)
                        {
                            Console.WriteLine($"{i + 1} - {aeroportosLista[i]}");
                        }

                        int opcaoEmbarque = int.Parse(Console.ReadLine());
                        if (opcaoEmbarque < 1 || opcaoEmbarque > aeroportosLista.Count)
                        {
                            Console.WriteLine("Opção inválida.");
                        }
                        else
                        {
                            aeroportoEmbarque = aeroportosLista[opcaoEmbarque - 1];
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Entrada inválida. Por favor, insira um número.");
                    }
                   
                }
                Aeroporto aeroportoDestino = null;
                while (aeroportoDestino == null)
                {
                    try
                    {
                        Console.WriteLine("Escolha o aeroporto de destino:");
                        for (int i = 0; i < aeroportosLista.Count; i++)
                        {
                            if (!aeroportosLista[i].Equals(aeroportoEmbarque))
                            {
                                Console.WriteLine($"{i + 1} - {aeroportosLista[i]}");
                            }
                        }

                        int opcaoDestino = int.Parse(Console.ReadLine());
                        if (opcaoDestino < 1 || opcaoDestino > aeroportosLista.Count || aeroportosLista[opcaoDestino - 1].Equals(aeroportoEmbarque))
                        {
                            Console.WriteLine("Opção inválida.");
                        }
                        else
                        {
                            aeroportoDestino = aeroportosLista[opcaoDestino - 1];
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Entrada inválida. Por favor, insira um número.");
                    }
                    
                }

                DateTime dataVoo = DateTime.MinValue;
                while (dataVoo == DateTime.MinValue)
                {
                    try
                    {
                        Console.WriteLine("Digite a data do voo (formato: dd/MM/yyyy):");
                        dataVoo = DateTime.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Data inválida. Por favor, insira uma data no formato correto (dd/MM/yyyy).");
                    }
                   
                }
                double precoBase = 0;
                while (precoBase <= 0)
                {
                    try
                    {
                        Console.WriteLine("Digite o preço do voo:");
                        precoBase = double.Parse(Console.ReadLine());

                        if (precoBase <= 0)
                        {
                            Console.WriteLine("O preço deve ser maior que zero. Tente novamente.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Preço inválido. Por favor, insira um valor numérico.");
                    }
                   
                }

                Voo novoVoo = new Voo(dataVoo, aeroportoEmbarque, aeroportoDestino, precoBase);
                vooLista.Add(novoVoo);

                Console.WriteLine("Voo cadastrado com sucesso!");
            }
            catch (Excecao ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }
    }
}
