﻿SELECT 
T_THE.* 
FROM 
dbo.T_TEIKI_HAISHA_ENTRY AS T_THE
WHERE 
T_THE.DELETE_FLG = 0
AND T_THE.TEIKI_HAISHA_NUMBER = /*data.TEIKI_HAISHA_NUMBER*/