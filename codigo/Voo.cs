using poo_tp_2024_2_deus_na_frente;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimViaje.AgenciaV1
{
    public class Voo
    {
        private const int Capacidade = 150;
        private static int _ultimoCodigo = 0;
        private int _codigo;
        private DateTime _horario;
        private double _precoBase;
        private int _bilhetesVendidos;
        private Aeroporto _origem;
        private Aeroporto _destino;

        public Voo() { }
        public Voo(DateTime quando, Aeroporto origem, Aeroporto destino, double precoBase)
        {
            _codigo = ++_ultimoCodigo;
            _horario = quando;
            _origem = origem;
            _destino = destino;
            if (precoBase < 100)
            {
                precoBase = 100;
            }
            else
            {
                _precoBase = precoBase;
            }
        }

        // PrecoBase()     João José
        public double PrecoBase()
        {
            return _precoBase;
        }
        /// <sumary>
        /// Verifica inicialmente se o voo já não atingiu a capacidade máxima. Se sim, ele avisa pro usuário que não tem como vender o trecho.
        /// Passando pelo if, um novo trecho será criado e receberá (this) como parâmetro, pois no construtor do treco ele precisa de um voo
        /// utilizando o this, ele adiciona os dados do voo em que está para ser passado como parâmetro, assim a variável _voo da classe trecho
        /// recebe o voo atual.
        ///</sumary>
        ///<returns>Retorna o trecho vendido </returns>
        public Trecho VenderTrecho()
        {
            if (_bilhetesVendidos >= Capacidade)
            {
                throw new Exception("Não há mais assentos disponíveis neste voo.");
            }

            Trecho trecho = new Trecho(this);
            _bilhetesVendidos++;
            return trecho;
        }

        public int Antecedencia(DateTime data)
        {
            return (int)_horario.Subtract(data).TotalDays;
        }

        public Aeroporto SaiDe()
        {
            return _origem;
        }

        public Aeroporto ChegaEm()
        {
            return _destino;
        }

        public bool DepoisDe(Voo voo)
        {
            return voo.SaiDe() == _origem && voo.ChegaEm() == _destino && voo.Horario() < _horario;
        }

        public DateTime Horario()
        {
            return _horario;
        }

        public int BilhetesVendidos()
        {
            return _bilhetesVendidos;
        }

        public override string ToString()
        {
            return $"Voo {_codigo}: Origem = {_origem}, Destino = {_destino}, Horário = {_horario.ToString("dd/MM/yyyy HH:mm")}, Preço Base = {_precoBase:C}, Bilhetes Vendidos = {BilhetesVendidos()}/{Capacidade}";
        }
        /// <summary>
        /// Combinando o código do voo com o horário do mesmo para garantir que todos os hashcodes serão diferentes, utlizando um método padrão do C#.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }
        /// <summary>
        /// Verifica se o objeto é um voo e depois compara o código dos dois.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is Voo voo)
            {
                return _codigo == voo._codigo;
            }
            return false;
        }

        
    }
}
