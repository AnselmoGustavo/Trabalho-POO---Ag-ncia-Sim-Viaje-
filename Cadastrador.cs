using System;
using SimViaje.AgenciaV1;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo_tp_2024_2_deus_na_frente
{
    public interface ICadastrador
    {
        public Trecho CriarTrecho(int codigo1, int codigo2, List<Aeroporto> aeroportosLista, List<Voo> voosLista);
    }
}
