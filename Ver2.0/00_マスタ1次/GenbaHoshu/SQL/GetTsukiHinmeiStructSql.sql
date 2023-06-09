﻿SELECT GTH.*
		,ISNULL(H.HINMEI_NAME_RYAKU,N'') AS HINMEI_NAME_RYAKU
		,ISNULL(U.UNIT_NAME_RYAKU,N'') AS UNIT_NAME_RYAKU
		,ISNULL(D.DENPYOU_KBN_NAME_RYAKU,N'') AS DENPYOU_KBN_NAME_RYAKU
  FROM M_GENBA_TSUKI_HINMEI GTH
	LEFT JOIN M_HINMEI H ON H.HINMEI_CD = GTH.HINMEI_CD
	LEFT JOIN M_UNIT U ON U.UNIT_CD = GTH.UNIT_CD
	LEFT JOIN M_DENPYOU_KBN D ON D.DENPYOU_KBN_CD = GTH.DENPYOU_KBN_CD
 WHERE 1 = 0