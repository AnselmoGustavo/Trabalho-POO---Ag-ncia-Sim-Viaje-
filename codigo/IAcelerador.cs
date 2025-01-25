using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimViaje.AgenciaV1
{
    public interface IAcelerador
    {
        public int PontosBilhete (Bilhete bilhete);

        public double CustoTotal();

        public DateTime TerminoAcelerador();

        public bool EstaAtivo();
    }
}
