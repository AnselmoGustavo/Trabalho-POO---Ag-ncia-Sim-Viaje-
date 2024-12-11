using SimViaje.AgenciaV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo_tp_2024_2_deus_na_frente.codigo
{
    internal class Bilhete_Prioritario : Bilhete, IBagagem
    {
        private const int ReaisPorPonto = 90;
        private const double BagagensExtras = 25d;
        private const double UmaBagagem = 50d;
        private const double PctAcrescimo = 0.15;
        private int _quantBagagens;

        public Bilhete_Prioritario(int quantidade) : base()
        {
            _quantBagagens = quantidade;
        }

        private double ValorBagagens()
        {
            if (_quantBagagens == 1)
            {
                return UmaBagagem;
            }
            return UmaBagagem + (BagagensExtras * (_quantBagagens-1));
        }

        public override double PrecoFinal()
        {
            double precoTrechos = 0;
            double precoBaseAlterado = _valorBase * (1 + PctAcrescimo);
            foreach(Trecho t in _trechos)
            {
                precoTrechos+= t.Preco();
            }
            return precoBaseAlterado + ValorBagagens() + precoTrechos;
        }

        public override int PontosAcumulados()
        {
            return (int)(PrecoFinal() / ReaisPorPonto);
        }

        public override string CartaoDeEmbarque()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("---- Cartão de Embarque Prioritário ----");
            sb.AppendLine($"Quantidade de Bagagens: {_quantBagagens}");
            return sb.ToString();
        }

        public int AdicionarBagagem(int quantas)
        {
            _quantBagagens += quantas;
            return _quantBagagens;
        }
    }
}
