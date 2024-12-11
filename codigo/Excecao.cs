using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo_tp_2024_2_deus_na_frente.codigo
{
    public class Excecao : Exception
    {
        public Excecao(string message)
        : base(message)
        {
        }
        public static void VerificarDado(string entrada)
        {
            try
            {
                if (!int.TryParse(entrada, out _))
                {
                    throw new OverflowException("O número digitado é muito grande");
                }
            }
            catch (FormatException)
            {
                throw new Excecao("Formato de dado inválido");
            }
            catch (OverflowException ex)
            {
                throw new Excecao("Ocorreu um erro inesperado, verifique novamente os dados inseridos");
            }
        }
    }
}