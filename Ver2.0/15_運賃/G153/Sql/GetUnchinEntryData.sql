﻿
SELECT  T_USSE.*
FROM	T_UNCHIN_ENTRY AS T_USSE
WHERE	T_USSE.DELETE_FLG = 0  
/*IF !data.DENSHU_KBN_CD.IsNull */
 AND	T_USSE.DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/''
/*END*/
/*IF !data.DENPYOU_NUMBER.IsNull */
 AND	T_USSE.DENPYOU_NUMBER = /*data.DENPYOU_NUMBER.Value*/''
/*END*/
/*IF !data.RENKEI_NUMBER.IsNull */
 AND	T_USSE.RENKEI_NUMBER = /*data.RENKEI_NUMBER.Value*/''
/*END*/