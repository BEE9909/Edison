﻿SELECT * FROM dbo.M_GYOUSHA 
WHERE 
((TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= TEKIYOU_END) or (TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and TEKIYOU_END IS NULL) or (TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= TEKIYOU_END) or (TEKIYOU_BEGIN IS NULL and TEKIYOU_END IS NULL)) 
AND DELETE_FLG = 0 
AND (SHOBUN_NIOROSHI_GYOUSHA_KBN = 'true') 