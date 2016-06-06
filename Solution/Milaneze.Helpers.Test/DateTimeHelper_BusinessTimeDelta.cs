using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Milaneze.Helpers;
using System.Collections.Generic;

namespace Milaneze.Helpers.Test
{
    [TestClass]
    public class DateTimeHelper_BusinessTimeDelta
    {
        List<DateTime> _feriados;

        [TestInitialize]
        public void Inicializar()
        {
            Utilidades.Clear();

            Utilidades.SetDataAtual(new DateTime(2014, 11, 4, 17, 30, 0));

            _feriados = new List<DateTime>();
            _feriados.Add(new DateTime(2014, 11, 20));
            _feriados.Add(new DateTime(2014, 3, 3));
            _feriados.Add(new DateTime(2014, 3, 4));
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_SemIntervalos1()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_SemIntervalos2()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(8, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DataFinalDepoisDaSaida()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 19, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(8, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DiaSeguinte()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 4, 10, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(9, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_MaisDeUmDia()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 8, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 10, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(1, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DataInicialAntesDaEntrada_DataFinalDepoisDaSaida()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 8, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 20, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(8, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DiaSeguinteFeriado()
        {
            DateTime dataInicial = new DateTime(2014, 11, 19, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 20, 11, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, _feriados);
            TimeSpan esperado = new TimeSpan(8, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_MesmoDiaFeriado()
        {
            DateTime dataInicial = new DateTime(2014, 11, 20, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 20, 20, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, _feriados);
            TimeSpan esperado = TimeSpan.Zero;

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DiaAtualFeriado_DiaSeguinteUtil()
        {
            DateTime dataInicial = new DateTime(2014, 11, 20, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 21, 11, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, _feriados);
            TimeSpan esperado = new TimeSpan(2, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_MaisDeUmDiaUtil()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 5, 17, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(1, 1, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_FimDeSemana()
        {
            DateTime dataInicial = new DateTime(2014, 11, 7, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 17, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(17, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_Feriado_E_Depois_FimDeSemana()
        {
            DateTime dataInicial = new DateTime(2014, 11, 19, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 24, 17, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, _feriados);
            TimeSpan esperado = new TimeSpan(1, 1, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DataInicialMaiorQueDataFinal()
        {
            DateTime dataInicial = new DateTime(2014, 11, 5, 17, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(1, 1, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_HoraInicialMaiorQueHoraFinal()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(8, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DiaSeguinte_HoraInicialMaiorQueHoraFinal()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 15, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 4, 10, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = new TimeSpan(3, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DiaSeguinte_HoraInicialIgualHoraFinal()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 15, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 15, 0, 0); ;

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal);
            TimeSpan esperado = TimeSpan.Zero;

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_IgnorarSabadoDomingo()
        {
            DateTime dataInicial = new DateTime(2014, 11, 7, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<DateTime> ignorarFinalDeSeamana = new List<DateTime>() {
                new DateTime(2014, 11, 8)
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, ignorarFinalDeSeamana);
            TimeSpan esperado = new TimeSpan(1, 1, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DefinirHorarioInicialEFinal()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 15, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 4, 15, 0, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 10, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 16, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal);
            TimeSpan esperado = new TimeSpan(6, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_DiaSeguinteFeriadoIgnorado()
        {
            DateTime dataInicial = new DateTime(2014, 11, 19, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 20, 11, 0, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);

            List<DateTime> ignorarFinalDeSeamana = new List<DateTime>() {
                new DateTime(2014, 11, 20)
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, ignorarFinalDeSeamana);
            TimeSpan esperado = new TimeSpan(10, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_SemDefinirDataFinal()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta();
            TimeSpan esperado = new TimeSpan(17, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_DiaTodo()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_UmDia_InicioDentroDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(4, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_UmDia_FimDentroDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 8, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaComMinutos()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 30, 0),
                new DateTime(2010, 1, 1, 13, 30, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaComSegundos()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 30, 45),
                new DateTime(2010, 1, 1, 13, 30, 45));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_DiaSeguinteTrabalha()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(15, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_DiaSeguinteNaoTrabalha()
        {
            DateTime dataInicial = new DateTime(2014, 11, 7, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 8, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_DiaAtualNaoTrabalhaSeguinteSim()
        {
            DateTime dataInicial = new DateTime(2014, 11, 9, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_DiaAtualNaoTrabalhaSeguinteSim2()
        {
            DateTime dataInicial = new DateTime(2014, 11, 9, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 12, 0, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_DiaSeguinteNaoTrabalhaProximoSim()
        {
            DateTime dataInicial = new DateTime(2014, 11, 8, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(15, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_InicioDoDiaDurantePausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(4, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_InicioDoDiaDepoisDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 15, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(2, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_FimDoDiaAntesDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 0, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_FimDoDiaDurantePausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_InicioDurantePausaDepoisFeriadoDepoisDiaComumDepoisFimDurantePausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 19, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 22, 12, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(15, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_InicioPausaDuranteInicioPeriodoDeTrabalho()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 8, 30, 0),
                new DateTime(2010, 1, 1, 9, 30, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(8, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_FimPausaDuranteFimPeriodoDeTrabalho()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 17, 0, 0),
                new DateTime(2010, 1, 1, 18, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(8, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_ComecoEFimNaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 10, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, horariosInicioFimPausa);
            TimeSpan esperado = TimeSpan.Zero;

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausa_VariosDias()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 12, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                new DateTime(2010, 1, 1, 12, 0, 0),
                new DateTime(2010, 1, 1, 13, 0, 0));

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, null, horariosInicioFimPausa);
            TimeSpan esperado = new TimeSpan(1, 1, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_VariosDias_HoraFinalMaiorHoraInicial()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 16, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 11, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal);
            TimeSpan esperado = new TimeSpan(20, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_MesmoDia()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 23, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true);
            TimeSpan esperado = new TimeSpan(14, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_Zerado()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 13, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true, null, null, null, true, true, null, 
                new Tuple<DateTime,DateTime>(
                    new DateTime().DefinirHorario(new TimeSpan(12, 0, 0)), 
                    new DateTime().DefinirHorario(new TimeSpan(13, 0, 0))), null);
            TimeSpan esperado = TimeSpan.Zero;

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_Exatamente24Horas()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 4, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true);
            TimeSpan esperado = new TimeSpan(24, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_Exatamente48Horas()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 5, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true);
            TimeSpan esperado = new TimeSpan(48, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_Exatamente48Horas2()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 5, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true, null, null, null, true, true, null, null, null);
            TimeSpan esperado = new TimeSpan(48, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_Exatamente48Horas3()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 5, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, false, null, null, null, true, true, null, null, null);
            TimeSpan esperado = new TimeSpan(48, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_Exatamente72Horas()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 9, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true);
            TimeSpan esperado = new TimeSpan(72, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_MeiaNoiteDiaSeguinte()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 4, 0, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true);
            TimeSpan esperado = new TimeSpan(15, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_Nao24Horas_MesmoDia()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 19, 0, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, false);
            TimeSpan esperado = new TimeSpan(8, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_24Horas_VariosDias()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 7, 18, 0, 0, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, true);
            TimeSpan esperado = new TimeSpan(105, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_SomenteHorarioInicial()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 12, 30, 0);
            DateTime? horarioInicial = null;
            DateTime? horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, null, null, null);
            TimeSpan esperado = new TimeSpan(28, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_SomenteHorarioFinal()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 12, 30, 0);
            DateTime? horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime? horarioFinal = null;

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, null, null, null);
            TimeSpan esperado = new TimeSpan(28, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_NenhumHorarioInicialOuFinalDefinido()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 12, 30, 0);
            DateTime? horarioInicial = null;
            DateTime? horarioFinal = null;

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, null, null, null);
            TimeSpan esperado = new TimeSpan(75, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_AmbosHorarioInicialEFinalDefinidos()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 12, 30, 0);
            DateTime? horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime? horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, null, null, null);
            TimeSpan esperado = new TimeSpan(28, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_ComecoEFimNaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 10, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = TimeSpan.Zero;

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia_MaisDeUmaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 13, 0, 0), new DateTime(2014, 11, 3, 14, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(6, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_UmDia_InicioDentroDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(4, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_UmDia_InicioDentroDaPausa_MaisDeUmaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 15, 0, 0), new DateTime(2014, 11, 3, 16, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_UmDia_InicioDentroDaPausa_MaisDeUmaPausaConsecutiva()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 13, 0, 0), new DateTime(2014, 11, 3, 14, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_UmDia_InicioDentroDaPausa_MaisDeUmaPausaConsecutiva2()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 13, 0, 0), new DateTime(2014, 11, 3, 14, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 13, 0, 0), new DateTime(2014, 11, 3, 12, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_UmDia_InicioDentroDaPausa_MaisDeUmaPausaConsecutivaDentroDaOutraPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 30, 0), new DateTime(2014, 11, 3, 14, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_UmDia_InicioDentroDaPausa_MaisDeUmaPausaConsecutivaDentroDaOutraPausa2()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 11, 30, 0), new DateTime(2014, 11, 3, 14, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia_MaisDeUmaPausa4()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 11, 30, 0), new DateTime(2014, 11, 3, 12, 30, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia_MaisDeUmaPausa2()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 30, 0), new DateTime(2014, 11, 3, 14, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(6, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia_MaisDeUmaPausa3()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 11, 0, 0), new DateTime(2014, 11, 3, 14, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(5, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia_MaisDeUmaPausa6()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 11, 0, 0), new DateTime(2014, 11, 3, 14, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 10, 0, 0), new DateTime(2014, 11, 3, 15, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia_MaisDeUmaPausa5()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 10, 0), new DateTime(2014, 11, 3, 12, 50, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_MesmoDia_MaisDeUmaPausa7()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 10, 0), new DateTime(2014, 11, 3, 12, 50, 0)),
                new Tuple<DateTime, DateTime>(new DateTime(2014, 11, 3, 12, 0, 0), new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_DiaTodo()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0))
            };


            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_UmDia_FimDentroDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 8, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_DiaSeguinteTrabalha()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 4, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 4, 12, 0, 0),
                    new DateTime(2014, 11, 4, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(15, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_DiaSeguinteNaoTrabalha()
        {
            DateTime dataInicial = new DateTime(2014, 11, 7, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 8, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 7, 12, 0, 0),
                    new DateTime(2014, 11, 7, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 8, 12, 0, 0),
                    new DateTime(2014, 11, 8, 13, 0, 0))
            };


            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_DiaAtualNaoTrabalhaSeguinteSim()
        {
            DateTime dataInicial = new DateTime(2014, 11, 9, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 9, 12, 0, 0),
                    new DateTime(2014, 11, 9, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 10, 12, 0, 0),
                    new DateTime(2014, 11, 10, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(7, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_DiaAtualNaoTrabalhaSeguinteSim2()
        {
            DateTime dataInicial = new DateTime(2014, 11, 9, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 12, 0, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 9, 12, 0, 0),
                    new DateTime(2014, 11, 9, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 10, 12, 0, 0),
                    new DateTime(2014, 11, 10, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_DiaSeguinteNaoTrabalhaProximoSim()
        {
            DateTime dataInicial = new DateTime(2014, 11, 8, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 10, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 8, 12, 0, 0),
                    new DateTime(2014, 11, 8, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 9, 12, 0, 0),
                    new DateTime(2014, 11, 9, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 10, 12, 0, 0),
                    new DateTime(2014, 11, 10, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(15, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_InicioDoDiaDurantePausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(4, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_InicioDoDiaDepoisDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 15, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(2, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_FimDoDiaAntesDaPausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 0, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_FimDoDiaDurantePausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 12, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(3, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_InicioDurantePausaDepoisFeriadoDepoisDiaComumDepoisFimDurantePausa()
        {
            DateTime dataInicial = new DateTime(2014, 11, 19, 12, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 22, 12, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 19, 12, 0, 0),
                    new DateTime(2014, 11, 19, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 20, 12, 0, 0),
                    new DateTime(2014, 11, 20, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 21, 12, 0, 0),
                    new DateTime(2014, 11, 21, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 22, 12, 0, 0),
                    new DateTime(2014, 11, 22, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(15, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_InicioPausaDuranteInicioPeriodoDeTrabalho()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 8, 30, 0),
                    new DateTime(2014, 11, 3, 9, 30, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(8, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_FimPausaDuranteFimPeriodoDeTrabalho()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 11, 3, 17, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 17, 0, 0),
                    new DateTime(2014, 11, 3, 18, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, null, false, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(8, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_ComPausaData_VariosDias()
        {
            DateTime dataInicial = new DateTime(2014, 11, 3, 9, 30, 0);
            DateTime dataFinal = new DateTime(2014, 11, 6, 12, 30, 0);
            DateTime horarioInicial = new DateTime(2014, 11, 4, 9, 0, 0);
            DateTime horarioFinal = new DateTime(2014, 11, 4, 17, 30, 0);

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 3, 12, 0, 0),
                    new DateTime(2014, 11, 3, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 4, 12, 0, 0),
                    new DateTime(2014, 11, 4, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 5, 12, 0, 0),
                    new DateTime(2014, 11, 5, 13, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 11, 6, 12, 0, 0),
                    new DateTime(2014, 11, 6, 13, 0, 0))
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, null, null, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(1, 1, 0, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_Carnaval2014_TrabalhandoMeioPeriodoSabado()
        {
            DateTime dataInicial = new DateTime(2014, 2, 21, 9, 0, 0);
            DateTime dataFinal = new DateTime(2014, 3, 6, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                    new DateTime().DefinirHorario(new TimeSpan(12, 0, 0)),
                    new DateTime().DefinirHorario(new TimeSpan(13, 0, 0)));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 3, 1, 13, 0, 0),
                    new DateTime(2014, 3, 1, 18, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 3, 5, 0, 0, 0),
                    new DateTime(2014, 3, 5, 13, 30, 0))
            };

            List<DateTime> datasSabadoDomingoFeriadoIgnorar = new List<DateTime>()
            {
                new DateTime(2014, 3, 1, 13, 0, 0)
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(60, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_BusinessTimeDelta_Carnaval2014_TrabalhandoMeioPeriodoSabado2()
        {
            DateTime dataInicial = new DateTime(2014, 2, 21, 11, 0, 0);
            DateTime dataFinal = new DateTime(2014, 3, 6, 17, 30, 0);
            DateTime horarioInicial = new DateTime().DefinirHorario(new TimeSpan(9, 0, 0));
            DateTime horarioFinal = new DateTime().DefinirHorario(new TimeSpan(17, 30, 0));

            Tuple<DateTime, DateTime> horariosInicioFimPausa = new Tuple<DateTime, DateTime>(
                    new DateTime().DefinirHorario(new TimeSpan(12, 0, 0)),
                    new DateTime().DefinirHorario(new TimeSpan(13, 0, 0)));

            List<Tuple<DateTime, DateTime>> datasInicioFimPausa = new List<Tuple<DateTime, DateTime>>()
            {
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 3, 1, 13, 0, 0),
                    new DateTime(2014, 3, 1, 18, 0, 0)),
                new Tuple<DateTime, DateTime>(
                    new DateTime(2014, 3, 5, 0, 0, 0),
                    new DateTime(2014, 3, 5, 13, 30, 0))
            };

            List<DateTime> datasSabadoDomingoFeriadoIgnorar = new List<DateTime>()
            {
                new DateTime(2014, 3, 1, 13, 0, 0)
            };

            TimeSpan resultado = dataInicial.BusinessTimeDelta(dataFinal, horarioInicial, horarioFinal, _feriados, true, true, datasSabadoDomingoFeriadoIgnorar, horariosInicioFimPausa, datasInicioFimPausa);
            TimeSpan esperado = new TimeSpan(58, 30, 0);

            Assert.AreEqual(esperado, resultado);
        }

        [TestMethod]
        public void DateTimeHelper_ProximoDiaUtil()
        {
            DateTime dataInicial = new DateTime(2014, 3, 3, 11, 0, 0);

            DateTime resultado = dataInicial.ProximoDiaUtil(_feriados, true, true, null);
            DateTime esperado = new DateTime(2014, 3, 5);

            Assert.AreEqual(esperado.Date, resultado.Date);
        }

        //[TestMethod]
        //public void DateTimeHelper_Add()
        //{
        //    DateTime dataInicial = new DateTime(2014, 12, 1, 11, 0, 0);

        //    TimeSpan tempoSomar = new TimeSpan(3, 0, 0);

        //    DateTime dataEsperada = new DateTime(2014, 12, 1, 14, 0, 0);
        //    DateTime dataResultante = dataInicial.AddBusinessTime(tempoSomar);


        //    Assert.AreEqual(dataEsperada, dataResultante);
        //}

        [TestCleanup]
        public void FinalizarTeste()
        {
            Utilidades.Clear();
        }
    }
}