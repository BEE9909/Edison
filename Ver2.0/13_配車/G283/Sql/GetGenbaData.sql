﻿SELECT * FROM M_GENBA
 where DELETE_FLG = 0
 /*IF data.GYOUSHA_CD != null*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*//*END*/
 /*IF data.GENBA_CD != null*/ AND GENBA_CD = /*data.GENBA_CD*//*END*/
 AND ((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= TEKIYOU_END) or (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and TEKIYOU_END IS NULL) or (TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= TEKIYOU_END) or (TEKIYOU_BEGIN IS NULL and TEKIYOU_END IS NULL))

