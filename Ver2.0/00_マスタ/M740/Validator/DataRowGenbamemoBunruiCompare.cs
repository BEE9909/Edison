﻿// $Id: DataRowBunruiCompare.cs 282 2013-07-23 09:15:33Z tecs_suzuki $
using System;
using System.Collections.Generic;
using System.Data;

namespace GenbamemoBunruiHoshu.Validator
{
    /// <summary>
    /// M_GENBAMEMO_BUNRUIが格納されたDataRow専用の比較クラス
    /// </summary>
    public class DataRowGenbamemoBunruiCompare : IEqualityComparer<DataRow>
    {
        /// <summary>
        /// インスタンスが等しいか判定
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>
        /// M_GENBAMEMO_BUNRUIのPKキーであるGENBAMEMO_BUNRUI_CDで判定
        /// </remarks>
        public bool Equals(DataRow x, DataRow y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            var xValue = x[Const.GenbamemoBunruiHoshuConstans.GENBAMEMO_BUNRUI_CD];
            var yValue = y[Const.GenbamemoBunruiHoshuConstans.GENBAMEMO_BUNRUI_CD];

            xValue = xValue.ToString();
            yValue = yValue.ToString();

            if (xValue.Equals(yValue))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public int GetHashCode(DataRow dataRow)
        {
            if (Object.ReferenceEquals(dataRow, null))
            {
                return 0;
            }

            var value = dataRow[Const.GenbamemoBunruiHoshuConstans.GENBAMEMO_BUNRUI_CD];

            return value.GetHashCode();
        }
    }
}
