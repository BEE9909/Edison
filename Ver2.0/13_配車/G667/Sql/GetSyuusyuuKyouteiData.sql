﻿SELECT 
    KYOTEN_CD 
FROM 
    T_TEIKI_HAISHA_ENTRY 
WHERE
    DELETE_FLG = 0
    AND TEIKI_HAISHA_NUMBER = /*TEIKI_HAISHA_NUMBER*/