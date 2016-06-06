using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Milaneze.Helpers.ValoresPorExtenso
{
    /// <summary>
    /// Classe usada para passar números escritos (1, 2, 3, 4... 1000000, ...) por extenso (um, dois, três, quatro... um milhão, ...).
    /// </summary>
    public static class ValorPorExtenso
    {
        #region Campos
        /// <summary>
        /// Variável de controle da propriedade Unidade
        /// </summary>
        private static Unidade _unidade;

        private static string[] GrupoMilhar = { 
            "",
            "mil",
            "milhão",
            "bilhão",
            "trilhão",
            "quadrilhão",
            "quintilhão",
            "sextilhão",
            "setilhão",
            "octilhão",
            "nonilhão",
            "decilhão",
            "undecilhão",
            "dodecilhão",
            "tredicilhão",
            "quatordecilhão",
            "quindecilhão",
            "sedecilhão",
            "septendecilhão"
        };

        private static string[] GrupoMilharPlural = { 
            "",
            "mil",
            "milhões",
            "bilhões",
            "trilhões",
            "quadrilhões",
            "quintilhões",
            "sextilhões",
            "setilhões",
            "octilhões",
            "nonilhões",
            "decilhões",
            "undecilhões",
            "dodecilhões",
            "tredicilhões",
            "quatordecilhões",
            "quindecilhões",
            "sedecilhões",
            "septendecilhões"
        };

        private static Dictionary<int, string> palavrasSimples = new Dictionary<int, string>()
        {
            { 0, "" },
            { 1, "um" },
            { 2, "dois" },
            { 3, "três" },
            { 4, "quatro" },
            { 5, "cinco" },
            { 6, "seis" },
            { 7, "sete" },
            { 8, "oito" },
            { 9, "nove" },
            { 10, "dez" },
            { 11, "onze" },
            { 12, "doze" },
            { 13, "treze" },
            { 14, "quatorze" },
            { 15, "quinze" },
            { 16, "dezesseis" },
            { 17, "dezessete" },
            { 18, "dezoito" },
            { 19, "dezenove" },
            { 20, "vinte" },
            { 30, "trinta" },
            { 40, "quarenta" },
            { 50, "cinquenta" },
            { 60, "sessenta" },
            { 70, "setenta" },
            { 80, "oitenta" },
            { 90, "noventa" },
            { 100, "cento" },
            { 200, "duzentos" },
            { 300, "trezentos" },
            { 400, "quatrocentos" },
            { 500, "quinhentos" },
            { 600, "seiscentos" },
            { 700, "setecentos" },
            { 800, "oitocentos" },
            { 900, "novecentos" }
        };

        private static string[] palavrasComENoFinal = {
            "um",
            "dois",
            "três",
            "quatro",
            "cinco",
            "seis",
            "sete",
            "oito",
            "nove",
            "dez",
            "onze",
            "doze",
            "treze",
            "quatorze",
            "quinze",
            "dezesseis",
            "dezessete",
            "dezoito",
            "dezenove",
            "vinte",
            "trinta",
            "quarenta",
            "cinquenta",
            "sessenta",
            "setenta",
            "oitenta",
            "noventa",
            "cento",
            "cem",
            "duzentos",
            "trezentos",
            "quatrocentos",
            "quinhentos",
            "seiscentos",
            "setecentos",
            "oitocentos",
            "novecentos"
        };
        #endregion

        #region Métodos privados
        /// <summary>
        /// Retorna um array do enumerador eDigitos com 3 elementos, sendo que:
        /// o elemento da posição [0] representa o algarismo da centena do inteiro informado;
        /// o elemento da posição [1] representa o algarismo da dezena do inteiro informado;
        /// o elemento da posição [3] representa o algarismo da unidade do inteiro informado;
        /// </summary>
        private static int[] inteiro999ParaMatrizDigitos(int x)
        {
            return new int[] 
            { 
                x / 100, 
                (x - (100 * (x / 100))) / 10, 
                x - 10 * (x / 10) 
            };
        }

        /// <summary>
        /// Retorna um inteiro calculado à partir de um array do enumerador eDigitos, sendo que:
        /// o Elemento da última posição do array representa o algarismo da unidade do inteiro a retornar;
        /// o Elemento da penúltima posição do array representa o algarismo da dezena do inteiro a retornar;
        /// o Elemento da antipenúltima posição do array representa o algarismo da centena do inteiro a retornar;
        /// os demais Elementos que eventualmente pertencerem ao array serão ignorados
        /// </summary>
        private static int matrizDigitosParaInteiro999(int[] x)
        {
            return (100 * Convert.ToByte(x[x.Length - 3])) + (10 * Convert.ToByte(x[x.Length - 2])) + (Convert.ToByte(x[x.Length - 1]));
        }

        /// <summary>
        /// Retorna string contendo as palavras que formam a descrição por extenso da
        /// milhar representada pelo array do enumerador eDigitos passado como parâmetro
        /// </summary>
        private static string milharPorExtenso(int[] a, byte classe)
        {
            string[] palavras = { "", "", "" };

            if (matrizDigitosParaInteiro999(a) > 20)
            {
                palavras[0] = palavrasSimples[100 * Convert.ToInt32(a[a.Length - 3])];

                int _dez1 = (((int)a[a.Length - 2]) * 10) + ((int)a[a.Length - 1]);

                if (_dez1 >= 20)
                    palavras[1] = palavrasSimples[10 * Convert.ToInt32(a[a.Length - 2])];
                else
                    palavras[1] = palavrasSimples[_dez1];


                int _dez2 = (((int)a[a.Length - 2]) * 10) + ((int)a[a.Length - 1]);

                if (_dez2 > 20)
                    palavras[2] = palavrasSimples[Convert.ToInt32(a[a.Length - 1])];
                else
                    palavras[2] = "";
            }
            else
            {
                palavras[0] = "";
                palavras[1] = palavrasSimples[10 * (int)a[a.Length - 2] + (int)a[a.Length - 1]];
                palavras[2] = "";
            }

            if (matrizDigitosParaInteiro999(a) == 100)
                palavras[0] = "cem";

            switch (matrizDigitosParaInteiro999(a))
            {
                case 1:
                    return getNumeroPorExtenso(palavras) + " " + GrupoMilhar[classe];
                default:
                    return getNumeroPorExtenso(palavras) + " " + GrupoMilharPlural[classe];
            }
        }

        private static string getNumeroPorExtenso(string[] palavras)
        {
            string numeroPorExtenso = palavras[0];

            if (numeroPorExtenso != "" && palavras[1] != "")
                numeroPorExtenso += (" e " + palavras[1]);
            else
                numeroPorExtenso += palavras[1];

            if (numeroPorExtenso != "")
                if (palavras[2] != "")
                    numeroPorExtenso += (" e " + palavras[2]);
                else
                    numeroPorExtenso += palavras[2];

            return numeroPorExtenso;
        }

        private static string paraExtenso(long numero, out bool incluirConjuncao, out byte classe)
        {
            int[] digitos = { 0, 0, 0 };

            string[] palavras = getNumeroPorExtensoSemTratativasDeVirgula(numero, out incluirConjuncao, out classe).Split(' ');

            if (palavras != null)
            {
                if (palavras.Length > 1)
                {
                    var palavrasEs = palavras.Select((p, i) => new { palavra = p, index = i }).Where(p => p.palavra == "e").ToArray();

                    foreach (var palavra in palavrasEs)
                        if (palavras[palavra.index - 1] != null)
                            if((GrupoMilhar.Any(p => p == palavras[palavra.index - 1]) || GrupoMilharPlural.Any(p => p == palavras[palavra.index - 1])))
                                palavras[palavra.index] = ",";

                    if ((palavrasComENoFinal.Any(p => p == palavras.Last()) || isUltimaPalavraMilhar(palavras)))
                        if(isUltimaPalavraVirgula(palavras))
                            palavras[palavras.Length - 2] = "e";
                    else if (isUltimaPalavraMilhar(palavras) && isPenultimaPalavraSimples(palavras) && isAntepenultimaPalavraContemVirgula(palavras))
                        palavras[palavras.Length - 3] = palavras[palavras.Length - 3].Replace(",", " e ");
                }
            }

            return string.Join(" ", palavras)
                .Replace(" , ", ", ")
                .RemoverEspacosDuplicados();
        }

        private static string getNumeroPorExtensoSemTratativasDeVirgula(long n, out bool incluirConjuncao, out byte classe)
        {
            incluirConjuncao = false;
            classe = 0;

            long x = Math.Abs(n);
            string retorno = "";

            while (x > 0)
            {
                int milhar = Convert.ToInt32(x - ((x / 1000) * 1000));

                incluirConjuncao = (incluirConjuncao || (classe == 1 && milhar == 0));

                x /= 1000;

                if (milhar != 0)
                {
                    if (retorno != "")
                        if (classe > 2 && !incluirConjuncao)
                            retorno = ", " + retorno;
                        else
                            retorno = " e " + retorno;

                    retorno = milharPorExtenso(inteiro999ParaMatrizDigitos(milhar), classe++) + retorno;
                }
                else
                {
                    classe++;
                }
            }

            if (n < 0)
                retorno = "menos " + retorno;

            return retorno
                .Replace("um milhão", "um xilhão")
                .Replace("um mil", "mil")
                .Replace("um xilhão", "um milhão")
                .Trim();
        }

        private static bool isAntepenultimaPalavraContemVirgula(string[] palavras)
        {
            return palavras.IsIndexOk(palavras.Length - 3) && !string.IsNullOrWhiteSpace(palavras[palavras.Length - 3]) && palavras[palavras.Length - 3].Last() == ',';
        }

        private static bool isPenultimaPalavraSimples(string[] palavras)
        {
            if (palavras.IsIndexOk(palavras.Length - 2))
                if (palavrasComENoFinal.Any(p => p == palavras[palavras.Length - 2]))
                    return true;

            return false;
        }

        private static bool isUltimaPalavraVirgula(string[] palavras)
        {
            if (palavras.IsIndexOk(palavras.Length - 2))
                if (palavras[palavras.Length - 2] != null)
                    if (palavras[palavras.Length - 2] == ",")
                        return true;

            return false;
        }

        private static bool isUltimaPalavraMilhar(string[] palavras)
        {
            return GrupoMilhar.Any(p => p == palavras.Last()) || GrupoMilharPlural.Any(p => p == palavras.Last());
        }

        private static void definirUnidadeCasoNaoTenhaSidoDefinida()
        {
            if (_unidade == null)
            {
                _unidade = new Unidade()
                {
                    InteiroSingular = "real",
                    InteiroPlural = "reais",
                    DecimalSingular = "centavo",
                    DecimalPlural = "centavos"
                };
            }
        }

        private static string getDe(bool incluirConjuncao, byte classe)
        {
            if (incluirConjuncao)
                if (classe >= 3)
                    return "de";

            return "";
        }
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Propriedade que recebe/retorna as descrições textuais da unidade monetária a ser utilizada.
        /// </summary>
        public static Unidade Unidade
        {
            get
            {
                return _unidade;
            }
            set
            {
                _unidade = value;
            }
        }

        /// <summary>
        /// Retorna a descrição monetária por extenso do número informado como parâmetro.
        /// </summary>
        /// <param name="numero">Número.</param>
        public static string ParaExtensoMoeda(float numero)
        {
            return ParaExtensoMoeda(Convert.ToDouble(numero));
        }

        /// <summary>
        /// Retorna a descrição monetária por extenso do número informado como parâmetro.
        /// </summary>
        /// <param name="numero">Número.</param>
        public static string ParaExtensoMoeda(decimal numero)
        {
            return ParaExtensoMoeda(Convert.ToDouble(numero));
        }

        /// <summary>
        /// Retorna a descrição monetária por extenso do número informado como parâmetro.
        /// </summary>
        /// <param name="numero">Número.</param>
        public static string ParaExtensoMoeda(double numero)
        {
            definirUnidadeCasoNaoTenhaSidoDefinida();

            long parteInteira = Convert.ToInt64(Math.Floor(Math.Abs(numero)) * Math.Sign(numero));
            int parteDecimal = Convert.ToInt32(100 * (Math.Abs(numero) - Math.Abs(parteInteira)));
            string retornoInteiro = ParaExtensoMoeda(parteInteira);
            string retornoDecimal = "";

            switch (parteDecimal)
            {
                case 0:
                    break;
                case 1:
                    retornoDecimal = milharPorExtenso(inteiro999ParaMatrizDigitos(parteDecimal), 0) + _unidade.DecimalSingular;
                    break;
                default:
                    retornoDecimal = milharPorExtenso(inteiro999ParaMatrizDigitos(parteDecimal), 0) + _unidade.DecimalPlural;
                    break;
            }

            if (retornoDecimal == "")
                return retornoInteiro;
            else
                if (retornoInteiro == "zero " + _unidade.InteiroPlural)
                    return retornoDecimal;

            return retornoInteiro + " e " + retornoDecimal;
        }

        /// <summary>
        /// Retorna a descrição monetária por extenso do número informado como parâmetro.
        /// </summary>
        /// <param name="numero">Número.</param>
        public static string ParaExtensoMoeda(int numero)
        {
            return ParaExtensoMoeda(Convert.ToInt64(numero));
        }

        /// <summary>
        /// Retorna a descrição monetária por extenso do número informado como parâmetro.
        /// </summary>
        /// <param name="numero">Número.</param>
        public static string ParaExtensoMoeda(long numero)
        {
            definirUnidadeCasoNaoTenhaSidoDefinida();

            bool incluirConjuncao;
            byte classe;
            string numeroPorExtenso = paraExtenso(numero, out incluirConjuncao, out classe);
            string numeroPorExtensoComDe = (numeroPorExtenso + " " + getDe(incluirConjuncao, classe)).Trim().RemoverEspacosDuplicados() + " ";

            switch (numero)
            {
                case 0:
                    return ("zero " + _unidade.InteiroPlural);
                case 1:
                    return (numeroPorExtensoComDe + _unidade.InteiroSingular);
                default:
                    return (numeroPorExtensoComDe + _unidade.InteiroPlural);
            }
        }

        /// <summary>
        /// Retorna a descrição por extenso do número informado como parâmetro.
        /// </summary>
        /// /// <param name="numero">Número.</param>
        public static string ParaExtenso(int numero)
        {
            return ParaExtenso(Convert.ToInt64(numero));
        }

        /// <summary>
        /// Retorna a descrição por extenso do número informado como parâmetro.
        /// </summary>
        /// <param name="numero">Número.</param>
        public static string ParaExtenso(long numero)
        {
            bool incluirConjuncao;
            byte classe;
 
            switch (numero)
            {
                case 0:
                    return ("zero");
                default:
                    return (paraExtenso(numero, out incluirConjuncao, out classe).TrimEnd());
            }
        }
        #endregion
    }
}