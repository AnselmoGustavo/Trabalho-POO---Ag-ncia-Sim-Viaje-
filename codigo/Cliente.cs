using poo_tp_2024_2_deus_na_frente.codigo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimViaje.AgenciaV1
{
    public class Cliente
    {
        private static int _ultimoId;
        private string _nome;
        private LinkedList<Bilhete> _bilhetes;
        private List<IAcelerador> _aceleradores;
        private int _registro;

        public Cliente() { }
        public Cliente(string nome)
        {
            _nome = nome;
            _registro = ++_ultimoId;
            _bilhetes = new LinkedList<Bilhete>();
            _aceleradores = new List<IAcelerador>();
        }

        public int AdicionarBilhete(Bilhete bilhete)
        {
            _bilhetes.AddLast(bilhete);
            return _ultimoId;
        }

        public void ContratarAcelerador(IAcelerador acelerador)
        {
            if (PodeAdicionarAcelerador(acelerador.TerminoAcelerador()))
            {
                _aceleradores.Add(acelerador);
            }
        }

        /// <summary>
        /// Verificar primeiro se n tem um acelerador funcionando no mesmo período de tempo que o que está adicionando.
        /// </summary>
        /// <param name="dataFinal"></param>
        /// <returns></returns>
        private bool PodeAdicionarAcelerador(DateTime dataFinal)
        {
            return _aceleradores.All(x => x.TerminoAcelerador() >= dataFinal.AddDays(-30));
        }
        public string RelatorioCompras()
        {
            StringBuilder relat = new StringBuilder("Dados do Cliente");
            relat.AppendLine($"\nNúmero de registro do cliente: {GetHashCode().ToString()}");
            relat.AppendLine($"Nome do Passageiro: {_nome}\n");
            relat.AppendLine($"Dados do(s) Bilhete(s): {DadosBilhetes()}");
            relat.AppendLine($"Quantidade de aceleradores comprados: {_aceleradores.Count}");
            return relat.ToString();
        }

        public string DadosBilhetes()
        {
            StringBuilder dadosB = new StringBuilder();
            foreach (Bilhete b in _bilhetes)
            {
                dadosB.AppendLine(b.ToString());
            }
            return dadosB.ToString();
        }

        public int PontosAcumulados()
        {
            int PontosTotais = 0;
            IAcelerador aceleradorAtivo = _aceleradores.FirstOrDefault(a => a.EstaAtivo());

            foreach (Bilhete bi in _bilhetes)
            {
                if (aceleradorAtivo != null)
                {
                    PontosTotais += aceleradorAtivo.PontosBilhete(bi);
                }
                else
                {
                    PontosTotais += bi.PontosAcumulados();
                }
            }

            return PontosTotais;
        }

        public double GastoTotal()
        {
            double gasto = 0;

            foreach (Bilhete bi in _bilhetes)
            {
                gasto += bi.PrecoFinal();
            }

            foreach (IAcelerador ace in _aceleradores)
            {
                gasto += ace.CustoTotal();
            }

            return gasto;
        }

        public LinkedList<Bilhete> RetornarBilhetes()
        {
            return _bilhetes;
        }

        public string RelatorioResumido()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_nome);
            sb.AppendLine($"Gasto total: {GastoTotal().ToString("C", new System.Globalization.CultureInfo("pt-BR"))}");

            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(RelatorioCompras());
            sb.AppendLine($"Gasto total R$: {GastoTotal():F2}");
            sb.AppendLine($"Pontos acumulados: {PontosAcumulados()}");

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return _registro.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Cliente cliente)
            {
                return _registro == cliente._registro;
            }
            return false;
        }

      

        

    }
}
