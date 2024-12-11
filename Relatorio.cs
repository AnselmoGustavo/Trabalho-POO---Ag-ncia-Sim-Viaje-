using SimViaje.AgenciaV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo_tp_2024_2_deus_na_frente
{
    public class Relatorio
    {
        public Relatorio()
        {

        }

        //A - consulta a dados de um cliente e a seu relatório de compras;
        public void ClienteEspecifico(List<Cliente> clienteLista)
        {
            bool idValido = false;

            while (!idValido)
            {
                try
                {
                    Console.WriteLine("Digite o id do cliente:");

                    int idCliente = int.Parse(Console.ReadLine());
                    Cliente especifico = clienteLista.FirstOrDefault(c => c.GetHashCode() == idCliente);

                    if (especifico != null)
                    {
                        Console.WriteLine($"{especifico}");
                        idValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Não existe cliente com esse ID. Tente novamente.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erro: O ID digitado não é um número válido. Tente novamente.");
                }




            }
        }

        // B - relatório resumido de clientes por ordem alfabética (crescente);
        public void ClienteAlfabetico(List<Cliente> clienteLista)
        {
            if (clienteLista == null)
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }
            else
            {
                List<Cliente> crescente = clienteLista.OrderBy(c => c.RelatorioResumido()).ToList();
                foreach (Cliente cli in crescente)
                {
                    Console.WriteLine(cli.RelatorioResumido());
                }
            }
        }

        public void ClientePorGasto(List<Cliente> clienteLista)
        {
            if (clienteLista == null)
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }
            else
            {
                List<Cliente> decrescente = clienteLista.OrderByDescending(c => c.GastoTotal()).ToList();

                foreach (Cliente cli in decrescente)
                {
                    Console.WriteLine(cli.RelatorioResumido());
                }
            }
        }

        // C - relatório resumido de clientes por ordem de gastos (decrescente);
        public List<Voo> VoosPorData(List<Voo> voos, DateTime data)
        {
            return voos.Where(v => v.Horario().Date == data).ToList();
        }

        //D - relatório de voos filtrados por uma data específica;
        public void VooPorData(List<Voo> vooLista, Relatorio relat)
        {
            bool dataValida = false;
            DateTime dataConvertida = DateTime.MinValue;

            while (!dataValida)
            {
                try
                {
                    Console.WriteLine("Digite a data que quer filtrar os voos (formato: dd/MM/yyyy):");
                    string data = Console.ReadLine();

                    dataConvertida = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dataValida = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("A data digitada não está em um formato válido. Tente novamente.");
                }
            }

            try
            {
                List<Voo> _voos = relat.VoosPorData(vooLista, dataConvertida);
                if (_voos.Any())
                {
                    foreach (Voo v in _voos)
                    {
                        Console.WriteLine(v);
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum voo encontrado para a data informada.");
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Formato ínvalido");
            }

        }

        // F - relatório completo do cliente com o maior gasto na agência até o momento;
        public Cliente ClienteMaiorGasto(List<Cliente> clientes)
        {
            Cliente resultado = null;

            if (clientes == null)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            else
            {
                resultado = clientes.OrderByDescending(c => c.GastoTotal()).FirstOrDefault();
            }

            return resultado;
        }

        // G - cartão de embarque do bilhete mais caro vendido pela agência;
        public Bilhete BilheteMaisCaro(List<Cliente> listaCliente)
        {
            Bilhete MaisCaro = null;

            if (listaCliente == null)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
            }
            else
            {
                MaisCaro = listaCliente
                           .SelectMany(c => c.RetornarBilhetes())
                           .OrderByDescending(b => b.PrecoFinal())
                           .FirstOrDefault();
            }
            return MaisCaro;
        }

        // i - listagem dos 10 voos com maior quantidade de bilhetes vendidos, em ordem decrescente;

        public void VoosMaiorQuantidade(List<Voo> vooLista)
        {
            if (vooLista == null)
            {
                Console.WriteLine("Nenum voo cadastrado");
            }
            else
            {
                List<Voo> voosComMaisBilhetes = vooLista
                                   .OrderByDescending(v => v.BilhetesVendidos())
                                   .Take(10)
                                   .ToList();

                foreach (Voo x in voosComMaisBilhetes)
                {
                    Console.WriteLine(x);
                }
            }

        }

        // J valor total arrecadado com bilhetes que contenham voos saindo de uma origem ou chegando a um destino escolhido;
        public void ValorArrecadadoBilhetes(List<Voo> voos, List<Cliente> clientes)
        {
            int opcao = 0;
            int codAeroporto = 0;
            bool entradaValida = false;

            while (!entradaValida)
            {
                try
                {
                    Console.WriteLine("Escolha a opção:");
                    Console.WriteLine("Origem - 1");
                    Console.WriteLine("Destino - 2");
                    opcao = int.Parse(Console.ReadLine());

                    if (opcao != 1 && opcao != 2)
                    {
                        Console.WriteLine("Opção inválida. Escolha 1 ou 2.");
                    }

                    entradaValida = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("O valor digitado está incorreto. Digite novamente.");
                }
            }

            entradaValida = false;
            while (!entradaValida)
            {
                try
                {
                    Console.WriteLine("Digite o código do aeroporto:");
                    codAeroporto = int.Parse(Console.ReadLine());
                    entradaValida = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("O valor digitado está incorreto. Digite novamente.");
                }
            }

            double valorTotal = clientes
                .SelectMany(c => c.RetornarBilhetes())
                .SelectMany(b => b.RetornarTrechos())
                .Where(t => (opcao == 1 && t.Origem().GetHashCode() == codAeroporto) ||
                            (opcao == 2 && t.Destino().GetHashCode() == codAeroporto))
                .Sum(t => t.Preco());

            Console.WriteLine($"O valor total arrecadado foi: {valorTotal:C}");
        }

    }
}
