using poo_tp_2024_2_deus_na_frente.codigo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimViaje.AgenciaV1
{
    public abstract class Bilhete
    {
        private static int ultimoId = 0;
        private DateTime _dataCompra;
        private string _id;
        protected LinkedList<Trecho> _trechos;
        protected const double _valorBase = 30;

        public Bilhete()
        {
            _trechos = new LinkedList<Trecho>();
            _id = GerarId(++ultimoId);
            _dataCompra = DataDeCompra();
        }

        private string GerarId(int valor)
        {
            Random random = new Random();

            char letra = (char)random.Next('A', 'Z' + 1);
            string digitos = valor.ToString("D5");

            return $"{letra}{digitos}";
        }
        /// <summary>
        /// Método adiciona e retorna true se o trecho for adicionado com sucesso, se não puder adicionar com base no método de pode incluir, ele retorna false.
        /// </summary>
        /// <param name="trecho"></param>
        /// <returns></returns>
        public bool AddTrecho(Trecho trecho)
        {
            if (PodeIncluir(trecho))
            {
                _trechos.AddLast(trecho);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Primeiro verifica se n tem nenhum pra adicionar o primeiro, caso n seja o primeiro, ele verifica se o destino do que já está lá é a origem do novo, se não, ele não pode adicionar.
        /// </summary>
        /// <param name="trecho"></param>
        /// <returns></returns>
        private bool PodeIncluir(Trecho trecho)
        {
            if (_trechos.Count == 0)
            {
                return true;
            }

            Trecho ultimoTrecho = _trechos.Last.Value;
            return ultimoTrecho.Destino == trecho.Origem;
        }

        public DateTime DataDeCompra()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Chama o equals do voo em questão para comparar com o equals de cada voo no trecho do bilhete para ver se existe esse voo em algum trecho.
        /// </summary>
        /// <param name="voo"></param>
        /// <returns></returns>
        public bool ContemTrecho(Voo voo)
        {
            foreach (Trecho trecho in _trechos)
            {
                return trecho.PertenceAoVoo(voo);
            }
            return false;
        }

        public LinkedList<Trecho> RetornarTrechos()
        {
            return _trechos;
        }

        /// <summary>
        /// Preço final agora é implementado por cada uma das filhas
        /// </summary>
        /// <returns></returns>
        public abstract double PrecoFinal();
        public abstract int PontosAcumulados();
        public abstract string CartaoDeEmbarque();
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(CartaoDeEmbarque());
            str.AppendLine($"Bilhete ID: {_id}");
            foreach (Trecho trecho in _trechos)
            {
                str.AppendLine(trecho.ToString());
            }
            str.AppendLine($"Data da Compra: {DataDeCompra():dd/MM/yyyy}");
            str.AppendLine($"Pontos: {PontosAcumulados()}");
            str.AppendLine($"Preco Final: {PrecoFinal():C}");
            return str.ToString();
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Bilhete bilhete)
            {
                return _id == bilhete._id;
            }

            return false;
        }
    }
}
