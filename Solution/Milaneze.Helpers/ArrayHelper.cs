using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milaneze.Helpers
{
    /// <summary>
    /// Extension methods para tratativas em arrays.
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Verifica se a posição existe dentro do ICollection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index">Posição dentro do ICollection.</param>
        /// <returns>Existe esse index no ICollection?</returns>
        public static bool IsIndexOk<T>(this ICollection<T> array, int index)
        {
            if (index >= 0 && index < array.Count)
                return true;

            return false;
        }
    }
}