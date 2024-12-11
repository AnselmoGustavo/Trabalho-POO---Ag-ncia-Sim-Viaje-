using System;
using System.Collections.Generic;
using System.Text;

namespace SimViaje.AgenciaV1
{
	public class Trecho
	{
		private readonly int[] Antecedencias = { 0, 5, 29, 89, int.MaxValue };
		private readonly double[] ModificadorPreco = { 1.5, 1.2, 1.0, 0.85, 0.7 };
		private Voo _voo;
		private DateTime _dataVenda;
		private double _precoCobrado;

		public Trecho(Voo voo)
		{
			_voo = voo;
			_dataVenda = DateTime.Now;
			_precoCobrado = voo.PrecoBase();
		}

		public double Preco()
		{
			return _precoCobrado;
		}

		public bool PertenceAoVoo(Voo voo)
		{
			return _voo.Equals(voo);
		}

		public Aeroporto Origem()
		{
			return _voo.SaiDe();
		}

		public Aeroporto Destino()
		{
			return _voo.ChegaEm();
		}

		public bool DepoisDe(Trecho outro)
		{
			return outro._voo.DepoisDe(_voo);
        }

		public string ToString()
		{
            StringBuilder relat = new StringBuilder("Dados do Trecho");
            relat.Append($"Voo: {_voo}\n Data da Venda: {_dataVenda}\n Preço cobrado: {_precoCobrado} ");
            return relat.ToString();
        }

		public override bool Equals(object? obj)
		{
			if(obj is Trecho trecho){
				return _dataVenda == trecho._dataVenda;
			}

			return false;
		}
	}
}
