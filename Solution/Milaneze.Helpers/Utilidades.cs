using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Milaneze.Helpers
{
    public static class Utilidades
    {
        private static DateTime? _dataAtual = null;
        private static DateTime? _inicioPeriodoDeTrabalho = null;
        private static DateTime? _fimPeriodoDeTrabalho = null;
        private static DateTime _inicioPeriodoDeTrabalhoPadrao = new DateTime(DataAtual.Year, DataAtual.Month, DataAtual.Day, 9, 0, 0);
        private static DateTime _fimPeriodoDeTrabalhoPadrao = new DateTime(DataAtual.Year, DataAtual.Month, DataAtual.Day, 17, 30, 0);
        private const string _feriadoDescricaoPascoa = "Páscoa";
        private const string _feriadoDescricaoSextaPaixao = "Sexta-Feira Santa";
        private const string _feriadoDescricaoCorpusChristi = "Corpus Christi";
        private const string _feriadoDescricaoCarnaval = "Carnaval";
        private const string _feriadoDescricaoDomingoCarnaval = "Domingo de Carnaval";
        private const string _feriadoDescricaoSegundaCarnaval = "Segunda de Carnaval";
        private static List<Tuple<int, int, string>> _feriadosFixos = new List<Tuple<int, int, string>>()
        {
            new Tuple<int, int, string>(1, 1, "Confraternização Universal"),
            new Tuple<int, int, string>(4, 21, "Tiradentes"),
            new Tuple<int, int, string>(5, 1, "Dia do trabalho"),
            new Tuple<int, int, string>(9, 7, "Independência do Brasil"),
            new Tuple<int, int, string>(10, 12, "Padroeira do Brasil"),
            new Tuple<int, int, string>(11, 2, "Finados"),
            new Tuple<int, int, string>(11, 15, "Proclamação da República"),
            new Tuple<int, int, string>(12, 25, "Natal")
        };

        /// <summary>
        /// Apaga todas as configurações.
        /// </summary>
        public static void Clear()
        {
            ClearDataAtual();
            ClearFimPeriodoDeTrabalho();
            ClearInicioPeriodoDeTrabalho();
        }

        #region Feriados

        private static DateTime calculaDiaPascoa(int ano)
        {
            int a = (19 * (ano % 19) + 24) % 30;
            int b = (2 * (ano % 4) + 4 * (ano % 7) + 6 * a + 5) % 7;

            if (a + b > 9)
                return new DateTime(ano, 4, (a + b - 9));

            return new DateTime(ano, 3, (a + b + 22));
        }

        private static DateTime calcularDomingoCarnaval(DateTime dataPascoa)
        {
            return dataPascoa.AddDays(-49);
        }

        private static DateTime calcularSegundaCarnaval(DateTime dataPascoa)
        {
            return dataPascoa.AddDays(-48);
        }

        private static DateTime calcularTercaCarnaval(DateTime dataPascoa)
        {
            return dataPascoa.AddDays(-47);
        }

        private static DateTime calcularSextaPaixao(DateTime dataPascoa)
        {
            return dataPascoa.AddDays(-2);
        }

        private static DateTime calcularCorpusChristi(DateTime dataPascoa)
        {
            return dataPascoa.AddDays(60);
        }

        /// <summary>
        /// Retorna os feriados fixos do ano com descrição.
        /// </summary>
        /// <param name="ano">Ano.</param>
        public static IEnumerable<Tuple<DateTime, string>> GetFeriadosFixosComDescricao(int ano)
        {
            return _feriadosFixos.Select(p => new Tuple<DateTime, string>(new DateTime(ano, p.Item1, p.Item2), p.Item3));
        }

        /// <summary>
        /// Retorna os feriados fixos do ano.
        /// </summary>
        /// <param name="ano">Ano.</param>
        public static IEnumerable<DateTime> GetFeriadosFixos(int ano)
        {
            return _feriadosFixos.Select(p => new DateTime(ano, p.Item1, p.Item2));
        }

        /// <summary>
        /// Retorna os feriados "móveis" do ano com descrição.
        /// </summary>
        /// <param name="ano">Ano.</param>
        /// <param name="contarCarnavalComoFeriado">Contar carnaval como feriado?</param>
        /// <param name="contarTodosDiasCarnavalComoFeriado">Contar todos dias de carnaval como feriado?</param>
        /// <param name="contarCorpusChristi">Contar "Corpus Christi" como feriado?</param>
        public static IEnumerable<Tuple<DateTime, string>> GetFeriadosMoveisComDescricao(int ano, bool contarCarnavalComoFeriado, bool contarTodosDiasCarnavalComoFeriado, bool contarCorpusChristi)
        {
            DateTime pascoa = calculaDiaPascoa(ano);

            yield return new Tuple<DateTime, string>(pascoa, _feriadoDescricaoPascoa);
            yield return new Tuple<DateTime, string>(calcularSextaPaixao(pascoa), _feriadoDescricaoSextaPaixao);

            if(contarCorpusChristi)
                yield return new Tuple<DateTime, string>(calcularCorpusChristi(pascoa), _feriadoDescricaoCorpusChristi);

            if (contarCarnavalComoFeriado)
            {
                yield return new Tuple<DateTime, string>(calcularTercaCarnaval(pascoa), _feriadoDescricaoCarnaval);

                if (contarTodosDiasCarnavalComoFeriado)
                {
                    yield return new Tuple<DateTime, string>(calcularDomingoCarnaval(pascoa), _feriadoDescricaoDomingoCarnaval);
                    yield return new Tuple<DateTime, string>(calcularSegundaCarnaval(pascoa), _feriadoDescricaoSegundaCarnaval);
                }
            }
        }

        /// <summary>
        /// Retorna os feriados "móveis" do ano.
        /// </summary>
        /// <param name="ano">Ano.</param>
        /// <param name="contarCarnavalComoFeriado">Contar carnaval como feriado?</param>
        /// <param name="contarTodosDiasCarnavalComoFeriado">Contar todos dias de carnaval como feriado?</param>
        /// <param name="contarCorpusChristi">Contar "Corpus Christi" como feriado?</param>
        public static IEnumerable<DateTime> GetFeriadosMoveis(int ano, bool contarCarnavalComoFeriado, bool contarTodosDiasCarnavalComoFeriado, bool contarCorpusChristi)
        {
            return GetFeriadosMoveisComDescricao(ano, contarCarnavalComoFeriado, contarTodosDiasCarnavalComoFeriado, contarCorpusChristi)
                .Select(p => p.Item1);
        }

        /// <summary>
        /// Retorna os feriados do ano com descrição.
        /// </summary>
        /// <param name="ano">Ano.</param>
        /// <param name="contarCarnavalComoFeriado">Contar carnaval como feriado?</param>
        /// <param name="contarTodosDiasCarnavalComoFeriado">Contar todos dias de carnaval como feriado?</param>
        /// <param name="contarCorpusChristi">Contar "Corpus Christi" como feriado?</param>
        public static IEnumerable<Tuple<DateTime, string>> GetFeriadosComDescricao(int ano, bool contarCarnavalComoFeriado, bool contarTodosDiasCarnavalComoFeriado, bool contarCorpusChristi)
        {
            var feriadosMoveis = GetFeriadosMoveisComDescricao(ano, contarCarnavalComoFeriado, contarTodosDiasCarnavalComoFeriado, contarCorpusChristi);
            var feriadosFixos = GetFeriadosFixosComDescricao(ano);

            foreach (var feriadoMovel in feriadosMoveis)
                yield return feriadoMovel;

            foreach (var feriadoFixo in feriadosFixos)
                yield return feriadoFixo;
        }

        /// <summary>
        /// Retorna os feriados do ano com descrição.
        /// </summary>
        /// <param name="ano">Ano.</param>
        /// <param name="contarCarnavalComoFeriado">Contar carnaval como feriado?</param>
        /// <param name="contarTodosDiasCarnavalComoFeriado">Contar todos dias de carnaval como feriado?</param>
        /// <param name="contarCorpusChristi">Contar "Corpus Christi" como feriado?</param>
        public static IEnumerable<DateTime> GetFeriados(int ano, bool contarCarnavalComoFeriado, bool contarTodosDiasCarnavalComoFeriado, bool contarCorpusChristi)
        {
            return GetFeriadosComDescricao(ano, contarCarnavalComoFeriado, contarTodosDiasCarnavalComoFeriado, contarCorpusChristi)
                .Select(p => p.Item1);
        }

        /// <summary>
        /// Retorna os feriados entre duas datas.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="contarCarnavalComoFeriado">Contar carnaval como feriado?</param>
        /// <param name="contarTodosDiasCarnavalComoFeriado">Contar todos dias de carnaval como feriado?</param>
        /// <param name="contarCorpusChristi">Contar "Corpus Christi" como feriado?</param>
        public static IEnumerable<DateTime> GetFeriados(DateTime dataInicial, DateTime dataFinal, bool contarCarnavalComoFeriado, bool contarTodosDiasCarnavalComoFeriado, bool contarCorpusChristi)
        {
            for (int i = dataInicial.Year; i <= dataFinal.Year; i++)
            {
                var feriadosAno = GetFeriados(i, contarCarnavalComoFeriado, contarTodosDiasCarnavalComoFeriado, contarCorpusChristi)
                    .Where(p => p.Date >= dataInicial.Date && p.Date <= dataFinal.Date);

                foreach (var feriado in feriadosAno)
                    yield return feriado;
            }
        }

        /// <summary>
        /// Retorna a data de Corpus Christi do ano.
        /// </summary>
        /// <param name="ano">Ano.</param>
        public static DateTime GetFeriadosQuartaFeiraDeCinzas(int ano)
        {
            return GetFeriadosMoveisComDescricao(ano, true, false, false)
                .Where(p => p.Item2 == _feriadoDescricaoCarnaval)
                .First()
                .Item1
                .Add(new TimeSpan(1, 0, 0, 0));
        }

        #endregion

        #region DataAtual

        /// <summary>
        /// Define a data atual (DateTime.Now) como a data que será usada para os cálculos e métodos da biblioteca.
        /// </summary>
        public static void SetDataAtualDefault()
        {
            _dataAtual = DateTime.Now;
        }

        /// <summary>
        /// Define a data atual que será usada para os cálculos e métodos da biblioteca.
        /// </summary>
        /// <param name="dataAtual">Data que será considerada como atual pela biblioteca.</param>
        public static void SetDataAtual(DateTime dataAtual)
        {
            _dataAtual = dataAtual;
        }

        /// <summary>
        /// Apaga a data atual considerada pela biblioteca.
        /// </summary>
        public static void ClearDataAtual()
        {
            _dataAtual = null;
        }

        /// <summary>
        /// Retorna a data que é usada como "data atual" pela biblioteca.
        /// </summary>
        public static DateTime DataAtual
        {
            get
            {
                if (_dataAtual.HasValue)
                    return _dataAtual.Value;

                return DateTime.Now;
            }
        }

        #endregion

        #region Início e Fim do Período de Trabalho

        /// <summary>
        /// Definir início do período de trabalho default usado pela biblioteca como 9h.
        /// </summary>
        public static void SetInicioPeriodoDeTrabalhoDefault()
        {
            _inicioPeriodoDeTrabalho = _inicioPeriodoDeTrabalhoPadrao;
        }

        /// <summary>
        /// Definir início do período de trabalho default usado pela biblioteca.
        /// </summary>
        /// <param name="inicioPeriodoDeTrabalho">Início do período de trabalho.</param>
        public static void SetInicioPeriodoDeTrabalho(DateTime inicioPeriodoDeTrabalho)
        {
            _inicioPeriodoDeTrabalho = inicioPeriodoDeTrabalho;
        }

        /// <summary>
        /// Apagar início do período de trabalho default usado pela biblioteca.
        /// </summary>
        public static void ClearInicioPeriodoDeTrabalho()
        {
            _inicioPeriodoDeTrabalho = null;
        }

        /// <summary>
        /// Retorna o início do período de trabalho usado pela biblioteca.
        /// </summary>
        public static DateTime InicioPeriodoDeTrabalho
        {
            get
            {
                if (_inicioPeriodoDeTrabalho.HasValue)
                    return _inicioPeriodoDeTrabalho.Value;

                return _inicioPeriodoDeTrabalhoPadrao;
            }
        }

        /// <summary>
        /// Definir fim do período de trabalho default usado pela biblioteca como 18h.
        /// </summary>
        public static void SetFimPeriodoDeTrabalhoDefault()
        {
            _fimPeriodoDeTrabalho = _fimPeriodoDeTrabalhoPadrao;
        }

        /// <summary>
        /// Definir fim do período de trabalho default usado pela biblioteca.
        /// </summary>
        /// <param name="fimPeriodoDeTrabalho">Fim do período de trabalho.</param>
        public static void SetFimPeriodoDeTrabalho(DateTime fimPeriodoDeTrabalho)
        {
            _fimPeriodoDeTrabalho = fimPeriodoDeTrabalho;
        }

        /// <summary>
        /// Apagar fim do período de trabalho default usado pela biblioteca.
        /// </summary>
        public static void ClearFimPeriodoDeTrabalho()
        {
            _fimPeriodoDeTrabalho = null;
        }

        /// <summary>
        /// Fim do período de trabalho default usado pela biblioteca.
        /// </summary>
        public static DateTime FimPeriodoDeTrabalho
        {
            get
            {
                if (_fimPeriodoDeTrabalho.HasValue)
                    return _fimPeriodoDeTrabalho.Value;

                return _fimPeriodoDeTrabalhoPadrao;
            }
        }

        #endregion
    }
}