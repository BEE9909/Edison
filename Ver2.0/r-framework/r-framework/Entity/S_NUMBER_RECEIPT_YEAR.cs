﻿using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class S_NUMBER_RECEIPT_YEAR : SuperEntity
    {
        public SqlInt16 NUMBERED_YEAR { get; set; }
        public string SEARCH_NUMBERED_DAY { get; set; }
        public SqlInt16 KYOTEN_CD { get; set; }
        public SqlInt32 CURRENT_NUMBER { get; set; }
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
