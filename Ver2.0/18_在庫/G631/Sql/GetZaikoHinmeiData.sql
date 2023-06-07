﻿select * from M_ZAIKO_HINMEI
WHERE CONVERT(DATE, ISNULL(TEKIYOU_BEGIN, DATEADD(day,-1,GETDATE()))) <= CONVERT(DATE, GETDATE()) and CONVERT(DATE, GETDATE()) <= CONVERT(DATE, ISNULL(TEKIYOU_END, DATEADD(day,1,GETDATE())))
AND DELETE_FLG = 0
AND ZAIKO_HINMEI_CD = /*data.zaikoHinmeiCd*/