using System;
using System.Collections.Generic;
using System.Text;

namespace SimViaje.AgenciaV1
{
	public class Aeroporto
	{
		private string _cidade;
		private string _codigo;

		public Aeroporto(string cidade, string codigo)
		{
			_cidade = cidade;
			_codigo = codigo;
		}

		public override string ToString()
		{
			return $"{_cidade} ({_codigo})";
		}

		public override bool Equals(object? obj)
		{
			Aeroporto outro = (Aeroporto)obj;
			return ToString().Equals(outro.ToString());
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
}

