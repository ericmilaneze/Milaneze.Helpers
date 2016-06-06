using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milaneze.Helpers
{
    /// <summary>
    /// Extension methods para tratativas do tipo Char.
    /// </summary>
    public static class CharHelper
    {
        /// <summary>
        /// Verifica se o caractere é um número.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns>É numérico?</returns>
        public static bool IsNumeric(this char chr)
        {
            return chr.ToString().IsNumeric();
        }
    }
}
