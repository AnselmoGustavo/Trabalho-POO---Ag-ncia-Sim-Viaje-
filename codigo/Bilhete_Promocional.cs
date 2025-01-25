using SimViaje.AgenciaV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo_tp_2024_2_deus_na_frente.codigo
{
    internal class Bilhete_Promocional : Bilhete
    {
        private const double Desconto = 0.1;
        private const int PontosConcedidos = 5;
        public Bilhete_Promocional() { }
        public override int PontosAcumulados()
        {
            return PontosConcedidos;
        }

        public override string CartaoDeEmbarque()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("---- Cartão de Embarque Promocional ----");
            sb.AppendLine($"Quantidade de Bagagens: Sem bagagens");
            return sb.ToString();
        }

        public override double PrecoFinal()
        {
            double precoTrechos = 0;
            foreach (Trecho t in _trechos)
            {
                precoTrechos += t.Preco();
            }
            double precoBaseAlterado = _valorBase-(_valorBase*Desconto);
            return precoBaseAlterado + precoTrechos;
        }
    }
}
