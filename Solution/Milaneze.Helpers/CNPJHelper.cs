using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milaneze.Helpers
{
    /// <summary>
    /// Validação e manipulação de CNPJ.
    /// </summary>
    public static class CNPJHelper
    {
        private const string sufixoMatriz = "0001";

        private static int[] multiplicador(string cnpj)
        {
            int[] multiplicador = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            if (cnpj.Length >= 13)
                multiplicador = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            return multiplicador;
        }

        private static string digitoVerificador(string cnpj)
        {
            int soma = 0;
            for (int i = 0; i < cnpj.Length; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador(cnpj)[i];

            if (soma % 11 >= 2)
                return (11 - (soma % 11)).ToString();

            return "0";
        }

        private static void minimoDozeCaracteresSenaoException(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || TirarFormatacao(cnpj).Length < 12)
                throw new ArgumentException("Parâmetro \"cnpj\" não pode ser nulo e deve conter, no mínimo, 12 caracteres.");
        }

        /// <summary>
        /// Valida um número de CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>CNPJ válido?</returns>
        public static bool Validar(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || TirarFormatacao(cnpj).Length != 14)
                return false;

            return TirarFormatacao(cnpj).Substring(12, 2) == ExtrairDigitosVerificadoresValidos(cnpj);
        }

        /// <summary>
        /// Extrai os dígitos do sufixo de um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>Dígitos do sufixo do CNPJ.</returns>
        public static string ExtrairDigitosSufixo(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || TirarFormatacao(cnpj).Length < 12)
                return "";

            return TirarFormatacao(cnpj).Substring(8, 4);
        }

        /// <summary>
        /// Extrai os dígitos verificadores válidos de um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>Dígitos verificadores válidos de um CNPJ.</returns>
        public static string ExtrairDigitosVerificadoresValidos(string cnpj)
        {
            minimoDozeCaracteresSenaoException(cnpj);

            return digitoVerificador(TirarFormatacao(cnpj).Substring(0, 12)) + digitoVerificador(TirarFormatacao(cnpj).Substring(0, 12) + digitoVerificador(TirarFormatacao(cnpj).Substring(0, 12)));
        }

        /// <summary>
        /// Extrai os dígitos verificadores de um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>Dígitos verificadores de um CNPJ.</returns>
        public static string ExtrairDigitosVerificadores(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || TirarFormatacao(cnpj).Length != 14)
                return "";

            return TirarFormatacao(cnpj).Substring(12, 2);
        }

        /// <summary>
        /// Tirar formatação de um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>CNPJ sem formatação.</returns>
        public static string TirarFormatacao(string cnpj)
        {
            if (cnpj == null)
                throw new ArgumentException("Parâmetro \"cnpj\" não pode ser nulo.");

            return cnpj.RemoverCaracteresNaoNumericos();
        }

        /// <summary>
        /// Formatar um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>CNPJ formatado.</returns>
        public static string Formatar(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || TirarFormatacao(cnpj).Length != 14)
                return cnpj;

            return string.Format("{0}.{1}.{2}/{3}-{4}",
                TirarFormatacao(cnpj).Substring(0, 2),
                TirarFormatacao(cnpj).Substring(2, 3),
                TirarFormatacao(cnpj).Substring(5, 3),
                ExtrairDigitosSufixo(cnpj),
                ExtrairDigitosVerificadores(cnpj));
        }

        /// <summary>
        /// Extrai a raiz de um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>Raiz de um CNPJ.</returns>
        public static string ExtrairRaiz(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj) || TirarFormatacao(cnpj).Length < 8)
                throw new ArgumentException("Parâmetro \"cnpj\" não pode ser nulo e deve conter, no mínimo, 8 caracteres.");

            return TirarFormatacao(cnpj).Substring(0, 8);
        }

        /// <summary>
        /// Verifica se um CNPJ é a matriz do grupo.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>É matriz do grupo?</returns>
        public static bool IsMatriz(string cnpj)
        {
            minimoDozeCaracteresSenaoException(cnpj);

            return ExtrairDigitosSufixo(cnpj) == sufixoMatriz;
        }

        /// <summary>
        /// Adiciona o sufixo a um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>Número de CNPJ + sufixo.</returns>
        public static string AdicionarSufixoMatriz(string cnpj)
        {
            if (cnpj == null)
                return null;

            return TirarFormatacao(cnpj).SafeSubstring(0, 8).PadLeft(8, '0') + sufixoMatriz;
        }

        /// <summary>
        /// Adiciona o sufixo + matriz + dígitos verificadores em um CNPJ.
        /// </summary>
        /// <param name="cnpj">Número de CNPJ.</param>
        /// <returns>Número de CNPJ + sufixo + matriz + dígitos verificadores de um CNPJ.</returns>
        public static string AdicionarSufixoMatrizComDigitosVerificadores(string cnpj)
        {
            if (cnpj == null)
                return null;

            string cnpjComSufixo = AdicionarSufixoMatriz(cnpj);

            return cnpjComSufixo + ExtrairDigitosVerificadoresValidos(cnpjComSufixo);
        }
    }
}