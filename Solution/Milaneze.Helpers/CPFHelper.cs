using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milaneze.Helpers
{
    /// <summary>
    /// Validação e manipulação de CPF.
    /// </summary>
    public static class CPFHelper
    {
        #region Métodos privados
        private static int digitoVerificador(string cpf)
        {
            int soma = 0;
            for (int i = 0; i < cpf.Length; i++)
                soma += int.Parse((TirarFormatacao(cpf).Substring(0, cpf.Length))[i].ToString()) * (cpf.Length + 1 - i);

            if (soma % 11 < 2)
                return 0;

            return 11 - (soma % 11);
        }

        private static void minimoNoveCaracteresSenaoArgumentException(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || TirarFormatacao(cpf).Length < 9)
                throw new ArgumentException("Parâmetro \"cpf\" não pode ser nulo e deve conter, no mínimo, 9 caracteres.");
        }
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Valida um CPF.
        /// </summary>
        /// <param name="cpf">Número de CPF.</param>
        /// <returns>CPF válido?</returns>
        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || TirarFormatacao(cpf).Length != 11 || !cpf.IsCaracteresDiferentes() || TirarFormatacao(cpf) == "12345678909")
                return false;

            return TirarFormatacao(cpf).Substring(9, 2) == ExtrairDigitosVerificadoresValidos(ExtrairRaiz(cpf));
        }

        /// <summary>
        /// Extrai dígitos verificadores válidos de um CPF.
        /// </summary>
        /// <param name="cpf">Número de CPF.</param>
        /// <returns>Dígitos verificadores válidos do CPF.</returns>
        public static string ExtrairDigitosVerificadoresValidos(string cpf)
        {
            minimoNoveCaracteresSenaoArgumentException(cpf);

            return string.Format("{0}{1}", digitoVerificador(ExtrairRaiz(cpf)), digitoVerificador(ExtrairRaiz(cpf) + digitoVerificador(ExtrairRaiz(cpf)).ToString()));
        }

        /// <summary>
        /// Extrai dígitos verificadores de um CPF.
        /// </summary>
        /// <param name="cpf">Número de CPF.</param>
        /// <returns>Dígitos verificadores do CPF</returns>
        public static string ExtrairDigitosVerificadores(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || TirarFormatacao(cpf).Length != 11)
                return "";

            return TirarFormatacao(cpf).Substring(9, 2);
        }

        /// <summary>
        /// Extrai a raiz de um CPF.
        /// </summary>
        /// <param name="cpf">Número de CPF.</param>
        /// <returns>Raiz do CPF.</returns>
        public static string ExtrairRaiz(string cpf)
        {
            minimoNoveCaracteresSenaoArgumentException(cpf);

            return TirarFormatacao(cpf).Substring(0, 9);
        }

        /// <summary>
        /// Tira a formatação de um CPF.
        /// </summary>
        /// <param name="cpf">Número de CPF.</param>
        /// <returns>CPF sem formatação.</returns>
        public static string TirarFormatacao(string cpf)
        {
            if(cpf == null)
                throw new ArgumentException("Parâmetro \"cpf\" não pode ser nulo.");

            return cpf.RemoverCaracteresNaoNumericos();
        }

        /// <summary>
        /// Formata um CPF.
        /// </summary>
        /// <param name="cpf">Número de CPF.</param>
        /// <returns>CPF formatado.</returns>
        public static string Formatar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || TirarFormatacao(cpf).Length != 11)
                return cpf;

            return string.Format("{0}.{1}.{2}-{3}",
                TirarFormatacao(cpf).Substring(0, 3),
                TirarFormatacao(cpf).Substring(3, 3),
                TirarFormatacao(cpf).Substring(6, 3),
                ExtrairDigitosVerificadores(cpf));
        }
        #endregion
    }
}