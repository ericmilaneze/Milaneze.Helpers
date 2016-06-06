using Milaneze.Helpers.ValoresPorExtenso;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Milaneze.Helpers
{
    /// <summary>
    /// Extension methods para tratativas de string.
    /// </summary>
    public static class StringHelper
    {
        #region Métodos privados

        private static int getMenorLength(int length, int numeroDeCaracteres, int menorStartIndex)
        {
            if (menorStartIndex < numeroDeCaracteres)
                return getMenorOuZero(length, numeroDeCaracteres - menorStartIndex);

            return 0;
        }

        private static int getMenorOuZero(int startIndex, int numeroDeCaracteres)
        {
            return Math.Max(Math.Min(startIndex, numeroDeCaracteres), 0);
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Retorna uma substring da mesma forma que o método [string object].substring(int startIndex, int length), porém sem retornar Exception caso o startIndex seja maior que o número de caracteres ou o length maior que o número de caracteres restantes.
        /// </summary>
        /// <param name="str">String em que será aplicada a o método.</param>
        /// <param name="startIndex">Caractere inicial.</param>
        /// <returns>Substring.</returns>
        public static string SafeSubstring(this string str, int startIndex)
        {
            return SafeSubstring(str, startIndex, str.Length);
        }

        /// <summary>
        /// Retorna uma substring da mesma forma que o método [string object].substring(int startIndex, int length), porém sem retornar Exception caso o startIndex seja maior que o número de caracteres ou o length maior que o número de caracteres restantes.
        /// </summary>
        /// <param name="str">String em que será aplicada a o método.</param>
        /// <param name="startIndex">Caractere inicial.</param>
        /// <param name="length">Número de caracteres retornados.</param>
        /// <returns>Substring.</returns>
        public static string SafeSubstring(this string str, int startIndex, int length)
        {
            int numeroDeCaracteres = str.Length;

            int menorStartIndex = getMenorOuZero(startIndex, numeroDeCaracteres);

            int menorLength = getMenorLength(length, numeroDeCaracteres, menorStartIndex);

            return str.Substring(menorStartIndex, menorLength);
        }

        /// <summary>
        /// Remover acentos.
        /// </summary>
        /// <param name="str">String com acentos.</param>
        /// <returns>String sem acentos.</returns>
        public static string RemoverAcentos(this string str)
        {
            StringBuilder retorno = new StringBuilder();

            var arrayText = str.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    retorno.Append(letter);

            return retorno.ToString();
        }

        /// <summary>
        /// Substituir primeira ocorrência de uma string.
        /// </summary>
        /// <param name="str">String que será modificada.</param>
        /// <param name="search">String procurada.</param>
        /// <param name="replace">String que substituirá a string procurada.</param>
        /// <returns>String com a primeira ocorrência buscada substituída.</returns>
        public static string ReplaceFirst(this string str, string search, string replace)
        {
            int pos = str.IndexOf(search);

            if (pos < 0)
                return str;

            return str.Substring(0, pos) + replace + str.Substring(pos + search.Length);
        }

        /// <summary>
        /// Remove espaços duplicados.
        /// </summary>
        /// <param name="str">String que será afetada.</param>
        /// <returns>String sem espaços duplicados.</returns>
        public static string RemoverEspacosDuplicados(this string str)
        {
            while (str.Trim().Contains("  "))
                str = str.Replace("  ", " ");

            return str.Trim();
        }

        /// <summary>
        /// Remove caracteres não numéricos.
        /// </summary>
        /// <param name="str">String afetada.</param>
        /// <returns>String com caracteres não numéricos removidos.</returns>
        public static string RemoverCaracteresNaoNumericos(this string str)
        {
            string strSemNumericos = "";

            for (int i = 0; i < str.Trim().Length; i++)
                if (str[i].IsNumeric())
                    strSemNumericos += str[i];

            return strSemNumericos.Trim();
        }

        /// <summary>
        /// Apaga os caracteres especiais não aceitos para nomes de arquivos no Windows.
        /// </summary>
        /// <param name="str">String que será afetada.</param>
        /// <returns>Nome do arquivo de Windows sem caracteres válidos.</returns>
        public static string RemoverCaracteresEspeciaisArquivos(this string str)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            
            return r.Replace(str, "");
        }

        /// <summary>
        /// Captura palavras numéricas.
        /// </summary>
        /// <param name="str">String que terá as palavras capturadas.</param>
        /// <returns>Array de strings com as palavras numéricas capturadas.</returns>
        public static string[] CapturarPalavrasNumericas(this string str)
        {
            string[] palavras = str.Split(' ');

            List<string> palavrasNumericas = new List<string>();

            foreach (var palavra in palavras)
                if (palavra.IsNumeric())
                    palavrasNumericas.Add(palavra);

            return palavrasNumericas.ToArray();
        }

        /// <summary>
        /// Escreve os número em uma string por extenso. Exemplo: "9" = "nove".
        /// </summary>
        /// <returns>String com números por extenso.</returns>
        public static string EscreverNumerosPorExtenso(this string str)
        {
            string[] numerosParaSubstituir = str.CapturarPalavrasNumericas();

            string retorno = str;

            foreach (var numeroParaSubstituir in numerosParaSubstituir)
            {
                string padrao = String.Format(@"\b{0}\b", numeroParaSubstituir);
                
                retorno = Regex.Replace(retorno, padrao, ValorPorExtenso.ParaExtenso(Convert.ToInt64(numeroParaSubstituir)), RegexOptions.None);
            }

            return retorno;
        }

        /// <summary>
        /// Os caracteres são diferentes? Exemplo: "1111" = false
        /// </summary>
        public static bool IsCaracteresDiferentes(this string str)
        {
            char[] caracteres = str.ToCharArray();

            for (int i = 0; i < caracteres.Length; i++)
                if (caracteres.IsIndexOk(i - 1))
                    if (caracteres[i - 1] != caracteres[i])
                        return true;

            return false;
        }

        /// <summary>
        /// É número?
        /// </summary>
        public static bool IsNumeric(this string str)
        {
            double numero;

            return double.TryParse(str, out numero);
        }

        /// <summary>
        /// Retorna a mesma string com o primeiro caractere maiúsculo.
        /// </summary>
        public static string PrimeiroCaractereMaiusculo(this string str)
        {
            if (str.Length >= 1)
                return string.Format("{0}{1}",
                    str[0].ToString().ToUpper(),
                    str.SafeSubstring(1));

            return str;
        }

        /// <summary>
        /// Retorna a mesma string com o primeiro caractere de cada palavra maiúsculo.
        /// </summary>
        public static string PrimeirasLetrasPalavraMaiusculas(this string str)
        {
            try
            {
                return new CultureInfo("pt-BR").TextInfo.ToTitleCase(str);
            }
            catch (CultureNotFoundException)
            {
                return "";
            }
            catch
            {
                return "";
            }
        }

        #endregion
    }
}