﻿SELECT *
  FROM T_MOBILE_SYOGUN_DATA_INSERT AS TB1 INNER JOIN
      (SELECT MAX(SEQ_NO) AS SEQ_NO
         FROM T_MOBILE_SYOGUN_DATA_INSERT
        WHERE
        /*IF data.EDABAN != 0*/ (EDABAN = /*data.EDABAN*/ ) /*END*/
        /*IF data.NODE_EDABAN != 0*/ AND (NODE_EDABAN = /*data.NODE_EDABAN*/ ) /*END*/
	   ) AS TB2
	   ON   TB1.SEQ_NO = TB2.SEQ_NO