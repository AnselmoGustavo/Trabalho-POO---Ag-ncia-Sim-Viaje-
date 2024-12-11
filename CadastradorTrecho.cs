using SimViaje.AgenciaV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo_tp_2024_2_deus_na_frente
{
    internal class CadastradorTrecho: ICadastrador
    {
        public CadastradorTrecho() { }

        public Trecho CriarTrecho(int codigo1, int codigo2, List<Aeroporto> aeroportosLista, List<Voo> voosLista)
        {
            Aeroporto partida = aeroportosLista.FirstOrDefault(a => a.GetHashCode() == codigo1);
            Aeroporto chegada = aeroportosLista.FirstOrDefault(a => a.GetHashCode() == codigo2);
            Voo vooCliente = voosLista.FirstOrDefault(v => v.SaiDe() == partida && v.ChegaEm() == chegada);
            Trecho trechoCliente = vooCliente.VenderTrecho();

            return trechoCliente;
        }
    }
}
