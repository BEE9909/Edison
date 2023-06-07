﻿// $Id: SalesPaymentConstans_SYS_RENBAN_HOUHOU_KBNExtTest.cs 2498 2013-09-25 11:05:33Z sanbongi $
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shougun.Function.ShougunCSCommon.Const;

namespace CommonTestProject.Const
{


    /// <summary>
    /// SalesPaymentConstans_SYS_RENBAN_HOUHOU_KBNExtTest のテスト クラスです。すべての
    /// SalesPaymentConstans_SYS_RENBAN_HOUHOU_KBNExtTest 単体テストをここに含めます
    /// </summary>
    [TestClass()]
    public class SalesPaymentConstans_SYS_RENBAN_HOUHOU_KBNExtTest
    {


        private TestContext testContextInstance;

        /// <summary>
        /// 現在のテストの実行についての情報および機能を
        /// 提供するテスト コンテキストを取得または設定します。
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        /// ToTypeString のテスト
        /// </summary>
        [TestMethod()]
        public void ToTypeStringTest()
        {
            SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN e = new SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN();
            string expected = string.Empty;
            string actual = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(e);
            Assert.AreEqual(expected, actual);

            e = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.HIRENBAN;
            expected = "日連番";
            actual = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(e);
            Assert.AreEqual(expected, actual);

            e = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBN.NENRENBAN;
            expected = "年連番";
            actual = SalesPaymentConstans.SYS_RENBAN_HOUHOU_KBNExt.ToTypeString(e);
            Assert.AreEqual(expected, actual);
        }
    }
}
