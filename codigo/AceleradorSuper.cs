using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimViaje.AgenciaV1
{
    public class AceleradorSuper : IAcelerador
    {
        private const double Multiplicador = 0.15;
        private const double ValorMensal = 29.90;
        private DateTime _dataInicio;
        private DateTime _dataFinal;

        public AceleradorSuper(DateTime dataInicio)
        {
            _dataInicio = dataInicio;
            _dataFinal = dataInicio.AddDays(30);
        }

        public int PontosBilhete(Bilhete bilhete)
        {
            return (int)(bilhete.PontosAcumulados()*(1+Multiplicador));
        }

        public double CustoTotal()
        {
            int duracaoMeses= ((_dataFinal.Year - _dataInicio.Year)*12) + _dataFinal.Month;
            return duracaoMeses * ValorMensal;
        }

        public DateTime TerminoAcelerador()
        {
            return _dataFinal;
        }

        public bool EstaAtivo()
        {
            DateTime dataAtual=DateTime.Now;
            return dataAtual>= _dataInicio && dataAtual<= _dataFinal;
        }
    }
}
