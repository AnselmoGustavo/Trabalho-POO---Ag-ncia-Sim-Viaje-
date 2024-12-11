using poo_tp_2024_2_deus_na_frente.codigo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimViaje.AgenciaV1
{
    public class Gerador
    {
        const int TAM_GRUPO = 4;
        const int DIST_DIAS = 10;
        const int TAM_VOOS = 10;
        private Random random;
        private List<Voo> voosLista;
        private static List<Aeroporto> aeroportosLista;
        private List<Cliente> clienteLista;

        public Gerador()
        {
            random = new Random();
            aeroportosLista = gerarAeroportos();
            voosLista = gerarVoos();
            clienteLista = gerarClientes();
        }

        public List<Cliente> gerarClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                string[] nomes = File.ReadAllLines(@"..\..\..\codigo\Dados\Nomes.txt");
                foreach (string nome in nomes)
                {
                    Cliente cliente = new Cliente(nome);

                    int quantidadeBilhetes = random.Next(1, 6);

                    for(int i=0; i< quantidadeBilhetes; i++)
                    {
                        Bilhete bilhete = gerarBilhetes();
                        cliente.AdicionarBilhete(bilhete);
                    }

                    int quantidadeAceleradores = random .Next(0, 5);
                    for(int i=0;i< quantidadeAceleradores; i++)
                    {
                        DateTime dataInicio=DateTime.Now.AddMonths(random.Next(-12,1));
                        
                        AceleradorSuper acelerador = new AceleradorSuper(dataInicio);
                        cliente.ContratarAcelerador(acelerador);
                    }
                    clientes.Add(cliente);
                }

            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Arquivo nao encontrado");
            }

            return clientes;
        }

        private Bilhete gerarBilhetes()
        {
            Random r = new Random();
            int tipoBilhete = random.Next(1, 3);

            switch (tipoBilhete)
            {
                case 1:
                    int bagagens = random.Next(0, 6);
                    Bilhete_Prioritario bilhete = new Bilhete_Prioritario(bagagens);
                    Voo voo = voosLista[r.Next(1, voosLista.Count - 1)]; 
                    Trecho trecho = voo.VenderTrecho();
                    bilhete.AddTrecho(trecho);
                    return bilhete;

                case 2:
                    Bilhete_Promocional bilhetePromo = new Bilhete_Promocional();
                    Voo voo2 = voosLista[r.Next(1, voosLista.Count - 1)];
                    Trecho trechoPromo = voo2.VenderTrecho();
                    bilhetePromo.AddTrecho(trechoPromo);
                    return bilhetePromo;

                default:
                    Bilhete_Promocional defaultBilhete = new Bilhete_Promocional();
                    Voo voo3 = voosLista[r.Next(1, voosLista.Count - 1)];
                    Trecho defaultTrecho = voo3.VenderTrecho();
                    defaultBilhete.AddTrecho(defaultTrecho);
                    return defaultBilhete;
            }
        }
            
        public static List<Aeroporto> gerarAeroportos()
        {
            List<Aeroporto> aeroportos = new List<Aeroporto>();
            try
            {
                string[] linhas = File.ReadAllLines(@"..\..\..\codigo\Dados\aeroportos_codigos.txt");

                foreach (string linha in linhas)
                {
                    string[] partes = linha.Split(',');
                    Aeroporto aeroporto = new Aeroporto(partes[0], partes[1]);
                    aeroportos.Add(aeroporto);
                }

            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Arquivo nao encontrado");
            }

            return aeroportos;
        }

        public static List<Voo> gerarVoos()
        {
            List<Voo> voos= new List<Voo>();
            Dictionary<int, List<Aeroporto>> gruposAeroportos = aeroportosLista
           .Select((aeroporto, index) => new { aeroporto, index })  // Associa cada aeroporto com seu índice
           .GroupBy(item => item.index / 4)                         // Agrupa por lotes de 4
           .ToDictionary(group => group.Key, group => group.Select(item => item.aeroporto).ToList());
            //public Voo(DateTime quando, Aeroporto origem, Aeroporto destino, double precoBase)
            foreach (KeyValuePair<int, List<Aeroporto>> grupo in gruposAeroportos)
            {
                DateTime dataVoo = GerarDataAleatoria();
                List<Aeroporto> aeroportosGrupo = grupo.Value;

                for (int i = 0; i < aeroportosGrupo.Count; i++)
                {
                    Aeroporto origem = aeroportosGrupo[i];

                    for (int j = 0; j < aeroportosGrupo.Count; j++)
                    {
                        if (i != j)
                        {
                            Aeroporto destino = aeroportosGrupo[j];
                            Voo voo = CriarVoo(origem, destino, ref dataVoo);
                            voos.Add(voo);
                        }
                    }
                }
            }
            return voos;
        }

        // Função para criar um voo com intervalo de DIST_DIAS dias
        private static Voo CriarVoo(Aeroporto origem, Aeroporto destino, ref DateTime dataVoo)
        {
            Voo voo = new Voo(dataVoo, origem, destino, GerarPrecoBase());
            dataVoo = dataVoo.AddDays(DIST_DIAS);
            return voo;
        }

        // Função para gerar um preço base aleatório para o voo
        private static double GerarPrecoBase()
        {
            Random r= new Random();
            return r.Next(100, 10001);
        }

        public List<Voo> RetornarListaVoo()
        {
            return voosLista;
        }
        public List<Aeroporto> RetornarListaAeroportos()
        {
            return aeroportosLista;
        }

        public List<Cliente> RetornarListaClientes()
        {
            return clienteLista;
        }

        private static DateTime GerarDataAleatoria()
        {
            Random r= new Random();
            int hora = r.Next(0, 24);
            int minutos=r.Next(0, 60);
            DateTime data = DateTime.Today.AddDays(DIST_DIAS).AddHours(hora).AddMinutes(minutos);
            return data;
        }
    }

}

