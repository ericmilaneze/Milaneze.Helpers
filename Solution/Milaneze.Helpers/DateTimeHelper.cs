using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Milaneze.Helpers
{
    /// <summary>
    /// Tratativas de data.
    /// </summary>
    public static class DateTimeHelper
    {
        #region Data por extenso
        /// <summary>
        /// Data por extenso em português.
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Data por extenso em português.</returns>
        public static string GetDataExtenso(this DateTime data)
        {
            CultureInfo culture = new CultureInfo("pt-BR");

            return string.Format("{0}, {1} de {2} de {3}",
                culture.DateTimeFormat.GetDayName(data.DayOfWeek).PrimeiroCaractereMaiusculo(),
                data.Day,
                data.MesPorExtenso().ToLower(),
                data.Year);
        }

        /// <summary>
        /// Dia da semana por extenso.
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Dia da semana por extenso.</returns>
        public static string DiaDaSemanaPorExtenso(this DateTime data)
        {
            try
            {
                return new CultureInfo("pt-BR").DateTimeFormat.GetDayName(data.DayOfWeek).PrimeiroCaractereMaiusculo();
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

        /// <summary>
        /// Mês por extenso.
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Mês por extenso.</returns>
        public static string MesPorExtenso(this DateTime data)
        {
            return MesPorExtenso(data.Month);
        }

        /// <summary>
        /// Mês por extenso.
        /// </summary>
        /// <param name="mes">Mês</param>
        /// <returns>Mês por extenso.</returns>
        public static string MesPorExtenso(int mes)
        {
            try
            {
                return new System.Globalization.CultureInfo("pt-BR").DateTimeFormat.GetMonthName(mes).PrimeiroCaractereMaiusculo();
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

        #region CalcularIdade
        /// <summary>
        /// Calcular idade.
        /// </summary>
        /// <param name="dataNascimento">Data de nascimento</param>
        /// <returns>Idade</returns>
        public static int CalcularIdade(this DateTime dataNascimento)
        {
            DateTime dataAtual = Utilidades.DataAtual;

            return dataNascimento.CalcularIdade(dataAtual);
        }

        /// <summary>
        /// Calcular idade.
        /// </summary>
        /// <param name="dataNascimento">Data de nascimento.</param>
        /// <param name="dataAtual">Data atual.</param>
        /// <returns>Idade</returns>
        public static int CalcularIdade(this DateTime dataNascimento, DateTime dataAtual)
        {
            if ((dataAtual.Month < dataNascimento.Month) || (dataAtual.Month.Equals(dataNascimento.Month) && dataAtual.Day < dataNascimento.Day))
                return Utilidades.DataAtual.Year - dataNascimento.Year - 1;

            return dataAtual.Year - dataNascimento.Year;
        }
        #endregion

        #region BusinessTimeDelta

        #region Métodos privados

        private static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime dataInicialBusinessTime, DateTime dataFinalBusinessTime, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (isZerado(dataInicial, dataFinal, dataInicialBusinessTime, dataFinalBusinessTime, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, datasInicioFimPausa))
                return TimeSpan.Zero;

            if (dataInicialBusinessTime.Date == dataFinalBusinessTime.Date)
                if (dataInicialBusinessTime.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                    return calcularTotalHorasComPausa(dataInicialBusinessTime, dataFinalBusinessTime, horariosInicioFimPausa, datasInicioFimPausa);

            return
                calcularTempoPrimeiroDia(dataInicialBusinessTime, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, dataInicialBusinessTime.DefinirHorario(horarioFimPeriodoDeTrabalho.GetTimeSpan()), horariosInicioFimPausa, datasInicioFimPausa)
                + calcularTempoUltimoDia(dataFinalBusinessTime, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, dataFinalBusinessTime.DefinirHorario(horarioInicioPeriodoDeTrabalho.GetTimeSpan()), horariosInicioFimPausa, datasInicioFimPausa)
                + calcularTempoEntrePrimeiroEUltimoDia(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, dataInicialBusinessTime.DefinirHorario(horarioInicioPeriodoDeTrabalho.GetTimeSpan()), dataInicialBusinessTime.DefinirHorario(horarioFimPeriodoDeTrabalho.GetTimeSpan()), dataFinalBusinessTime.DefinirHorario(horarioInicioPeriodoDeTrabalho.GetTimeSpan()), horariosInicioFimPausa, datasInicioFimPausa);
        }

        private static TimeSpan BusinessTimeDelta24h(this DateTime dataInicial, DateTime dataFinal, DateTime dataInicialBusinessTime, DateTime dataFinalBusinessTime, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (isZerado(dataInicial, dataFinal, dataInicialBusinessTime, dataFinalBusinessTime, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, datasInicioFimPausa))
                return TimeSpan.Zero;

            if (dataInicialBusinessTime.Date == dataFinalBusinessTime.Date)
                if (dataInicialBusinessTime.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                    return calcularTotalHorasComPausa(dataInicial, dataFinal, horariosInicioFimPausa, datasInicioFimPausa);

            return
                calcularTempoPrimeiroDia(dataInicial, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, dataInicial.AddDays(1).DefinirHorario(new TimeSpan(0, 0, 0)), horariosInicioFimPausa, datasInicioFimPausa)
                + calcularTempoUltimoDia(dataFinal, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, dataFinal.DefinirHorario(new TimeSpan(0, 0, 0)), horariosInicioFimPausa, datasInicioFimPausa)
                + calcularTempoEntrePrimeiroEUltimoDia24h(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, dataInicial.AddDays(1).DefinirHorario(new TimeSpan(0, 0, 0)), dataInicial.AddDays(2).DefinirHorario(new TimeSpan(0, 0, 0)), dataFinal, horariosInicioFimPausa, datasInicioFimPausa);
        }

        private static bool isZerado(DateTime dataInicial, DateTime dataFinal, DateTime dataInicialBusinessTime, DateTime dataFinalBusinessTime, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (isHorarioInicialFinalDentroDaPausa(dataInicial, dataFinal, horariosInicioFimPausa) || isDataInicialFinalDentroDaPausa(dataInicial, dataFinal, datasInicioFimPausa))
                return true;

            if (dataInicialBusinessTime.Date == dataFinalBusinessTime.Date)
                if (!dataInicialBusinessTime.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                    return true;

            return false;
        }

        private static Tuple<DateTime, DateTime> validarHorariosInicioFimPausa(Tuple<DateTime, DateTime> horariosInicioFimPausa)
        {
            if (horariosInicioFimPausa != null)
                return new Tuple<DateTime, DateTime>(
                    new DateTime().DefinirHorario(calcularHoraInicialValida(horariosInicioFimPausa.Item1.GetTimeSpan(), horariosInicioFimPausa.Item2.GetTimeSpan())),
                    new DateTime().DefinirHorario(calcularHoraFinalValida(horariosInicioFimPausa.Item1.GetTimeSpan(), horariosInicioFimPausa.Item2.GetTimeSpan())));

            return null;
        }

        private static List<Tuple<DateTime, DateTime>> validarDatasInicioFimPausa2(IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            List<Tuple<DateTime, DateTime>> datasInicioFimPausaValidas = new List<Tuple<DateTime, DateTime>>();

            if (datasInicioFimPausa != null)
                foreach (var datasInicioFimPausaAtual in datasInicioFimPausa)
                    if (datasInicioFimPausaAtual.Item1.Date == datasInicioFimPausaAtual.Item2.Date)
                    {
                        if (!datasInicioFimPausaValidas.Exists(p =>
                            p.Item1 == calcularDataInicialValida(datasInicioFimPausaAtual.Item1, datasInicioFimPausaAtual.Item2) &&
                            p.Item2 == calcularDataInicialValida(datasInicioFimPausaAtual.Item1, datasInicioFimPausaAtual.Item2)))
                        {
                            datasInicioFimPausaValidas.Add(new Tuple<DateTime, DateTime>(
                            calcularDataInicialValida(datasInicioFimPausaAtual.Item1, datasInicioFimPausaAtual.Item2),
                            calcularDataFinalValida(datasInicioFimPausaAtual.Item1, datasInicioFimPausaAtual.Item2)));
                        }
                    }

            if (datasInicioFimPausaValidas.Any())
                return datasInicioFimPausaValidas.OrderBy(p => p.Item1).ToList();

            return null;
        }

        private static List<Tuple<DateTime, DateTime>> validarDatasInicioFimPausaSemDuplicados(IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            List<Tuple<DateTime, DateTime>> datasInicioFimPausaValidas = validarDatasInicioFimPausa2(datasInicioFimPausa);

            if (datasInicioFimPausaValidas != null)
            {
                List<Tuple<DateTime, DateTime>> datasInicioFimPausaValidasSemDuplicados = new List<Tuple<DateTime, DateTime>>();
                datasInicioFimPausaValidasSemDuplicados.AddRange(datasInicioFimPausaValidas);

                for (int h = 0; h < datasInicioFimPausaValidasSemDuplicados.Count; h++)
                {
                    var pausaAtual = datasInicioFimPausaValidasSemDuplicados[h];

                    List<Tuple<DateTime, DateTime>> outrasPausasMesmoDia = datasInicioFimPausa.Where(p =>
                        p.Item1.Date == pausaAtual.Item1.Date &&
                        p.Item1 != pausaAtual.Item1 &&
                        p.Item2 != pausaAtual.Item2).ToList();

                    for (int i = 0; i < outrasPausasMesmoDia.Count; i++)
                    {
                        if (isHorarioInicialFinalDentroDaPausa(outrasPausasMesmoDia[i].Item1, outrasPausasMesmoDia[i].Item2, pausaAtual))
                        {
                            if (datasInicioFimPausaValidasSemDuplicados.Exists(p =>
                                p.Item1 == outrasPausasMesmoDia[i].Item1 &&
                                p.Item2 == outrasPausasMesmoDia[i].Item2))
                            {
                                datasInicioFimPausaValidasSemDuplicados.Remove(outrasPausasMesmoDia[i]);

                                i = 0;
                                continue;
                            }
                        }

                        if (isHorarioDentroDaPausa(outrasPausasMesmoDia[0].Item1, pausaAtual.Item1, pausaAtual.Item2) && outrasPausasMesmoDia[0].Item1 != pausaAtual.Item2)
                        {
                            var dataASerMudada = datasInicioFimPausaValidasSemDuplicados.FirstOrDefault(p =>
                                p.Item1 == outrasPausasMesmoDia[i].Item1 &&
                                p.Item2 == outrasPausasMesmoDia[i].Item2);

                            if (dataASerMudada != null)
                            {
                                datasInicioFimPausaValidasSemDuplicados.Remove(dataASerMudada);

                                datasInicioFimPausaValidasSemDuplicados.Add(
                                    new Tuple<DateTime, DateTime>(
                                        dataASerMudada.Item1.DefinirHorario(pausaAtual.Item2.GetTimeSpan()),
                                        dataASerMudada.Item2));

                                i = 0;
                                continue;
                            }
                        }

                        break;
                    }
                }

                return datasInicioFimPausaValidasSemDuplicados.OrderBy(p => p.Item1).ToList();
            }

            return null;
        }

        private static DateTime getHoraInicioValida(DateTime? horarioInicioPeriodoDeTrabalho)
        {
            if (horarioInicioPeriodoDeTrabalho.HasValue)
                return horarioInicioPeriodoDeTrabalho.Value;

            return Utilidades.InicioPeriodoDeTrabalho;
        }

        private static DateTime getHoraFimValida(DateTime? horarioFimPeriodoDeTrabalho)
        {
            if (horarioFimPeriodoDeTrabalho.HasValue)
                return horarioFimPeriodoDeTrabalho.Value;

            return Utilidades.FimPeriodoDeTrabalho;
        }

        private static TimeSpan calcularTotalHorasComPausa(DateTime dataInicialDia, DateTime dataFinalDia, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            IList<Tuple<DateTime, DateTime>> datasInicioFimPausaComPausaDia = getPausaDia(dataInicialDia, horariosInicioFimPausa, datasInicioFimPausa);

            if (datasInicioFimPausaComPausaDia != null)
            {
                TimeSpan totalPausa = TimeSpan.Zero;

                foreach (var inicioFimPausaDoDia in datasInicioFimPausaComPausaDia)
                {
                    if (inicioFimPausaDoDia.Item1.Date == dataInicialDia.Date)
                    {
                        TimeSpan horaInicioDaPausa = inicioFimPausaDoDia.Item1.GetTimeSpan();
                        TimeSpan horaFimDaPausa = inicioFimPausaDoDia.Item2.GetTimeSpan();

                        if (isPausaDentroDoHorario(dataInicialDia, dataFinalDia, horaInicioDaPausa, horaFimDaPausa))
                            totalPausa += (horaFimDaPausa - horaInicioDaPausa);

                        if (isHorarioDentroDaPausa(dataInicialDia, new DateTime().DefinirHorario(horaInicioDaPausa), new DateTime().DefinirHorario(horaFimDaPausa)))
                            totalPausa += (dataInicialDia.DefinirHorario(horaFimDaPausa) - dataInicialDia);

                        if (isHorarioDentroDaPausa(dataFinalDia, new DateTime().DefinirHorario(horaInicioDaPausa), new DateTime().DefinirHorario(horaFimDaPausa)))
                            totalPausa += (dataFinalDia - dataFinalDia.DefinirHorario(horaInicioDaPausa));
                    }
                }

                return dataFinalDia - dataInicialDia - totalPausa;
            }

            return dataFinalDia - dataInicialDia;
        }

        private static IList<Tuple<DateTime, DateTime>> getPausaDia(DateTime dataInicialDia, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (datasInicioFimPausa != null)
                if (!datasInicioFimPausa.Any(p => p.Item1.Date == dataInicialDia.Date))
                    if (horariosInicioFimPausa != null)
                        return new List<Tuple<DateTime, DateTime>>()
                        {
                            new Tuple<DateTime, DateTime>(
                                dataInicialDia.DefinirHorario(horariosInicioFimPausa.Item1.GetTimeSpan()),
                                dataInicialDia.DefinirHorario(horariosInicioFimPausa.Item2.GetTimeSpan()))
                        };


            if (datasInicioFimPausa == null)
                if (horariosInicioFimPausa != null)
                    return new List<Tuple<DateTime, DateTime>>()
                    {
                        new Tuple<DateTime, DateTime>(
                            dataInicialDia.DefinirHorario(horariosInicioFimPausa.Item1.GetTimeSpan()),
                            dataInicialDia.DefinirHorario(horariosInicioFimPausa.Item2.GetTimeSpan()))
                    };

            return datasInicioFimPausa;
        }

        private static bool isVinteEQuatroHoras(DateTime? horarioInicioPeriodoDeTrabalho, DateTime? horarioFimPeriodoDeTrabalho)
        {
            if (horarioInicioPeriodoDeTrabalho == null)
                if (horarioFimPeriodoDeTrabalho == null)
                    return true;

            return false;
        }

        private static TimeSpan calcularTempoPrimeiroDia(DateTime dataInicial, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, DateTime dataFinalDia, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (dataInicial.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                return calcularTotalHorasComPausa(dataInicial, dataFinalDia, horariosInicioFimPausa, datasInicioFimPausa);

            return TimeSpan.Zero;
        }

        private static TimeSpan calcularTempoUltimoDia(DateTime dataFinal, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, DateTime inicioUltimoDia, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (dataFinal.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                return calcularTotalHorasComPausa(inicioUltimoDia, dataFinal, horariosInicioFimPausa, datasInicioFimPausa);

            return TimeSpan.Zero;
        }

        private static TimeSpan calcularTempoEntrePrimeiroEUltimoDia(IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, DateTime inicioTrabalhoPrimeiroDia, DateTime horarioFimTrabalho, DateTime inicioUltimoDia, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            TimeSpan timeInBetween = TimeSpan.Zero;

            for (DateTime diaAtualIterador = inicioTrabalhoPrimeiroDia.AddDays(1); diaAtualIterador < inicioUltimoDia; diaAtualIterador = diaAtualIterador.AddDays(1))
                if (diaAtualIterador.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                    timeInBetween += calcularTotalHorasComPausa(diaAtualIterador, diaAtualIterador.DefinirHorario(horarioFimTrabalho.GetTimeSpan()), horariosInicioFimPausa, datasInicioFimPausa);

            return timeInBetween;
        }

        private static TimeSpan calcularTempoEntrePrimeiroEUltimoDia24h(IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, DateTime inicioTrabalhoPrimeiroDia, DateTime horarioFimTrabalho, DateTime inicioUltimoDia, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            TimeSpan timeInBetween = TimeSpan.Zero;

            for (DateTime diaAtualIterador = inicioTrabalhoPrimeiroDia.AddDays(1); diaAtualIterador < inicioUltimoDia; diaAtualIterador = diaAtualIterador.AddDays(1))
                if (diaAtualIterador.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                    timeInBetween += calcularTotalHorasComPausa(diaAtualIterador, diaAtualIterador.AddDays(1).DefinirHorario(new TimeSpan(0, 0, 0)), horariosInicioFimPausa, datasInicioFimPausa);

            return timeInBetween;
        }

        private static bool isDataInicialFinalDentroDaPausa(DateTime dataInicial, DateTime dataFinal, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (datasInicioFimPausa != null)
            {
                if (dataInicial.Date == dataFinal.Date)
                {
                    foreach (var inicioFimPausa in datasInicioFimPausa)
                    {
                        DateTime inicioPausa = inicioFimPausa.Item1;
                        DateTime fimPausa = inicioFimPausa.Item2;

                        if (isDataDentroDaPausa(dataInicial, inicioPausa, fimPausa))
                            if (isDataDentroDaPausa(dataFinal, inicioPausa, fimPausa))
                                return true;
                    }
                }
            }

            return false;
        }

        private static bool isHorarioInicialFinalDentroDaPausa(DateTime dataInicial, DateTime dataFinal, Tuple<DateTime, DateTime> horariosInicioFimPausa)
        {
            if (horariosInicioFimPausa != null)
                if (dataInicial.Date == dataFinal.Date)
                    if (isHorarioDentroDaPausa(dataInicial, horariosInicioFimPausa.Item1, horariosInicioFimPausa.Item2) && isHorarioDentroDaPausa(dataFinal, horariosInicioFimPausa.Item1, horariosInicioFimPausa.Item2))
                        return true;

            return false;
        }

        private static TimeSpan calcularHoraInicialValida(TimeSpan horaInicial, TimeSpan horaFinal)
        {
            if (horaInicial > horaFinal)
                return horaFinal;

            return horaInicial;
        }

        private static TimeSpan calcularHoraFinalValida(TimeSpan horaInicial, TimeSpan horaFinal)
        {
            if (horaInicial > horaFinal)
                return horaInicial;

            return horaFinal;
        }

        private static DateTime calcularDataInicialValida(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataInicial > dataFinal)
                return dataFinal;

            return dataInicial;
        }

        private static DateTime calcularDataFinalValida(DateTime dataInicial, DateTime dataFinal)
        {
            if (dataInicial > dataFinal)
                return dataInicial;

            return dataFinal;
        }

        private static bool isDataDentroDaPausa(DateTime data, DateTime inicioPausa, DateTime fimPausa)
        {
            if (inicioPausa.Date == fimPausa.Date)
                if (data.Date == inicioPausa.Date)
                    if (isHorarioDentroDaPausa(data, inicioPausa, fimPausa))
                        return true;

            return false;
        }

        private static bool isHorarioDentroDaPausa(DateTime data, DateTime inicioPausa, DateTime fimPausa)
        {
            if (data.GetTimeSpan() >= inicioPausa.GetTimeSpan())
                if (data.GetTimeSpan() <= fimPausa.GetTimeSpan())
                    return true;

            return false;
        }

        private static DateTime dataDentroDoDiaDeTrabalhoEntrada(DateTime dataEntrada, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, Tuple<DateTime, DateTime> horariosInicioFimPausa, List<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (dataEntrada < dataEntrada.DefinirHorario(horarioInicioPeriodoDeTrabalho.GetTimeSpan()))
                return dataEntrada.DefinirHorario(horarioInicioPeriodoDeTrabalho.GetTimeSpan());

            if (dataEntrada > dataEntrada.DefinirHorario(horarioFimPeriodoDeTrabalho.GetTimeSpan()))
                return dataEntrada.DefinirHorario(horarioFimPeriodoDeTrabalho.GetTimeSpan());

            return dataEntrada;
        }

        private static DateTime dataDentroDoDiaDeTrabalhoSaida(DateTime dataSaida, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, Tuple<DateTime, DateTime> horariosInicioFimPausa, List<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (dataSaida < dataSaida.DefinirHorario(horarioInicioPeriodoDeTrabalho.GetTimeSpan()))
                return dataSaida.DefinirHorario(horarioInicioPeriodoDeTrabalho.GetTimeSpan());

            if (dataSaida > dataSaida.DefinirHorario(horarioFimPeriodoDeTrabalho.GetTimeSpan()))
                return dataSaida.DefinirHorario(horarioFimPeriodoDeTrabalho.GetTimeSpan());

            return dataSaida;
        }

        private static bool isPausaDentroDoHorario(DateTime dataInicialDia, DateTime dataFinalDia, TimeSpan horaInicioDaPausa, TimeSpan horaFimDaPausa)
        {
            if (dataInicialDia.GetTimeSpan() <= horaInicioDaPausa)
                if (dataFinalDia.GetTimeSpan() >= horaFimDaPausa)
                    return true;

            return false;
        }

        private static bool isDiaUtilFeriado(DateTime data, IList<DateTime> feriados, IList<DateTime> datasSabadoDomingoFeriadoIgnorar)
        {
            if (feriados != null)
                if (feriados.Any(p => p.Date == data.Date))
                    if (datasSabadoDomingoFeriadoIgnorar == null || !datasSabadoDomingoFeriadoIgnorar.Any(p => p.Date == data.Date))
                        return false;

            return true;
        }

        private static bool isDiaUtilFinalDeSemana(DateTime data, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, DayOfWeek diaDeSemanaSabadoOuDomingo, bool diaFinalDeSemanaContar)
        {
            if (diaFinalDeSemanaContar)
                if (data.DayOfWeek == diaDeSemanaSabadoOuDomingo)
                    if (datasSabadoDomingoFeriadoIgnorar == null || !datasSabadoDomingoFeriadoIgnorar.Any(p => p.Date == data.Date))
                        return false;

            return true;
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial)
        {
            DateTime dataFinal = Utilidades.DataAtual;

            return BusinessTimeDelta(dataInicial, dataFinal);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, Utilidades.InicioPeriodoDeTrabalho);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="feriados">Feriados.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, IList<DateTime> feriados)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, Utilidades.InicioPeriodoDeTrabalho, Utilidades.FimPeriodoDeTrabalho, feriados);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime horarioInicioPeriodoDeTrabalho)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, horarioInicioPeriodoDeTrabalho, Utilidades.FimPeriodoDeTrabalho);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, horarioInicioPeriodoDeTrabalho, horarioFimPeriodoDeTrabalho, null);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <param name="feriados">Feriados.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, IList<DateTime> feriados)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, horarioInicioPeriodoDeTrabalho, horarioFimPeriodoDeTrabalho, feriados, true, true);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, horarioInicioPeriodoDeTrabalho, horarioFimPeriodoDeTrabalho, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, null);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <param name="datasSabadoDomingoFeriadoIgnorar">Dias de sábado ou domingo que serão considerados como dia de trabalho normal.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, horarioInicioPeriodoDeTrabalho, horarioFimPeriodoDeTrabalho, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, null);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <param name="datasSabadoDomingoFeriadoIgnorar">Dias de sábado ou domingo que serão considerados como dia de trabalho normal.</param>
        /// <param name="horariosInicioFimPausa">Horários de pausa.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, Tuple<DateTime, DateTime> horariosInicioFimPausa)
        {
            return BusinessTimeDelta(dataInicial, dataFinal, horarioInicioPeriodoDeTrabalho, horarioFimPeriodoDeTrabalho, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, null);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="vinteEQuatroHoras">Considerar as 24 horas do dia (sem horário de início e fim)?</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, bool vinteEQuatroHoras)
        {
            if (vinteEQuatroHoras)
                return dataInicial.BusinessTimeDelta(dataFinal, null, null, null, false, false, null, null, null);

            return dataInicial.BusinessTimeDelta(dataFinal);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="vinteEQuatroHoras">Considerar as 24 horas do dia (sem horário de início e fim)?</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <param name="datasSabadoDomingoFeriadoIgnorar">Dias de sábado ou domingo que serão considerados como dia de trabalho normal.</param>
        /// <param name="horariosInicioFimPausa">Horários de pausa.</param>
        /// <param name="datasInicioFimPausa">Datas de início e fim de pausa.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, bool vinteEQuatroHoras, DateTime? horarioInicioPeriodoDeTrabalho, DateTime? horarioFimPeriodoDeTrabalho, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            if (vinteEQuatroHoras)
                return dataInicial.BusinessTimeDelta(dataFinal, null, null, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, datasInicioFimPausa);

            return dataInicial.BusinessTimeDelta(dataFinal, horarioInicioPeriodoDeTrabalho, horarioFimPeriodoDeTrabalho, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, datasInicioFimPausa);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <param name="datasSabadoDomingoFeriadoIgnorar">Dias de sábado ou domingo que serão considerados como dia de trabalho normal.</param>
        /// <param name="horariosInicioFimPausa">Horários de pausa.</param>
        /// <param name="datasInicioFimPausa">Datas de início e fim de pausa.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime horarioInicioPeriodoDeTrabalho, DateTime horarioFimPeriodoDeTrabalho, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            return dataInicial.BusinessTimeDelta(dataFinal, (DateTime?)horarioInicioPeriodoDeTrabalho, (DateTime?)horarioFimPeriodoDeTrabalho, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, datasInicioFimPausa);
        }

        /// <summary>
        /// Calcular tempo de trabalho.
        /// </summary>
        /// <param name="dataInicial">Data inicial.</param>
        /// <param name="dataFinal">Data final.</param>
        /// <param name="horarioInicioPeriodoDeTrabalho">Horário do início de expediente.</param>
        /// <param name="horarioFimPeriodoDeTrabalho">Horário do fim de expediente.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <param name="datasSabadoDomingoFeriadoIgnorar">Dias de sábado ou domingo que serão considerados como dia de trabalho normal.</param>
        /// <param name="horariosInicioFimPausa">Horários de pausa.</param>
        /// <param name="datasInicioFimPausa">Datas de início e fim de pausa.</param>
        /// <returns>Tempo de trabalho.</returns>
        public static TimeSpan BusinessTimeDelta(this DateTime dataInicial, DateTime dataFinal, DateTime? horarioInicioPeriodoDeTrabalho, DateTime? horarioFimPeriodoDeTrabalho, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar, Tuple<DateTime, DateTime> horariosInicioFimPausa, IList<Tuple<DateTime, DateTime>> datasInicioFimPausa)
        {
            DateTime dataInicialValida = calcularDataInicialValida(dataInicial, dataFinal);
            DateTime dataFinalValida = calcularDataFinalValida(dataInicial, dataFinal);
            DateTime horarioInicioPeriodoDeTrabalhoValido = getHoraInicioValida(horarioInicioPeriodoDeTrabalho);
            DateTime horarioFimPeriodoDeTrabalhoValido = getHoraFimValida(horarioFimPeriodoDeTrabalho);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausaValidas = validarDatasInicioFimPausaSemDuplicados(datasInicioFimPausa);
            Tuple<DateTime, DateTime> horariosInicioFimPausaValidas = validarHorariosInicioFimPausa(horariosInicioFimPausa);
            DateTime dataInicialBusinessTime = dataDentroDoDiaDeTrabalhoEntrada(dataInicialValida, horarioInicioPeriodoDeTrabalhoValido, horarioFimPeriodoDeTrabalhoValido, horariosInicioFimPausaValidas, datasInicioFimPausaValidas);
            DateTime dataFinalBusinessTime = dataDentroDoDiaDeTrabalhoSaida(dataFinalValida, horarioInicioPeriodoDeTrabalhoValido, horarioFimPeriodoDeTrabalhoValido, horariosInicioFimPausaValidas, datasInicioFimPausaValidas);

            if (isVinteEQuatroHoras(horarioInicioPeriodoDeTrabalho, horarioFimPeriodoDeTrabalho))
                return BusinessTimeDelta24h(dataInicialValida, dataFinalValida, dataInicialBusinessTime, dataFinalBusinessTime, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausaValidas, datasInicioFimPausaValidas);

            return BusinessTimeDelta(dataInicialValida, dataFinalValida, dataInicialBusinessTime, dataFinalBusinessTime, horarioInicioPeriodoDeTrabalhoValido, horarioFimPeriodoDeTrabalhoValido, feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausaValidas, datasInicioFimPausaValidas);
        }

        /// <summary>
        /// Verifica se a data é um dia útil.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <param name="datasSabadoDomingoFeriadoIgnorar">Dias de sábado ou domingo que serão considerados como dia de trabalho normal.</param>
        /// <returns></returns>
        public static bool IsDiaUtil(this DateTime data, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar)
        {
            return
                isDiaUtilFinalDeSemana(data, datasSabadoDomingoFeriadoIgnorar, DayOfWeek.Saturday, contarSabadoComoFeriado) &&
                isDiaUtilFinalDeSemana(data, datasSabadoDomingoFeriadoIgnorar, DayOfWeek.Sunday, contarDomingoComoFeriado) &&
                isDiaUtilFeriado(data, feriados, datasSabadoDomingoFeriadoIgnorar);
        }

        /// <summary>
        /// DateTime para TimeSpan.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan GetTimeSpan(this DateTime data)
        {
            return new TimeSpan(0, data.Hour, data.Minute, data.Second, data.Millisecond);
        }

        /// <summary>
        /// Definir horário de data usando um TimeSpan.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="horario">Horário.</param>
        /// <returns>Data com horário.</returns>
        public static DateTime DefinirHorario(this DateTime data, TimeSpan horario)
        {
            return new DateTime(data.Year, data.Month, data.Day, horario.Hours, horario.Minutes, horario.Seconds, horario.Milliseconds);
        }

        /// <summary>
        /// Retorna o próximo dia útil após a data especificada.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="feriados">Feriados.</param>
        /// <param name="contarSabadoComoFeriado">Contar sábado como feridado?</param>
        /// <param name="contarDomingoComoFeriado">Contar domingo como feriado?</param>
        /// <param name="datasSabadoDomingoFeriadoIgnorar">Dias de sábado ou domingo que serão considerados como dia de trabalho normal.</param>
        /// <returns>Próximo dia útil após a data especificada.</returns>
        public static DateTime ProximoDiaUtil(this DateTime data, IList<DateTime> feriados, bool contarSabadoComoFeriado, bool contarDomingoComoFeriado, IList<DateTime> datasSabadoDomingoFeriadoIgnorar)
        {
            DateTime auxData = data.AddDays(1);

            while (!auxData.IsDiaUtil(feriados, contarSabadoComoFeriado, contarDomingoComoFeriado, datasSabadoDomingoFeriadoIgnorar))
                auxData = auxData.AddDays(1);

            return auxData;
        }

        #endregion

        #endregion
    }
}