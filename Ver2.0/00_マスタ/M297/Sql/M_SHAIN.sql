﻿SELECT            M_BUSHO.BUSHO_CD, M_BUSHO.BUSHO_NAME_RYAKU, M_SHAIN.SHAIN_CD, M_SHAIN.SHAIN_NAME_RYAKU
FROM              M_SHAIN INNER JOIN
                        M_BUSHO ON M_SHAIN.BUSHO_CD = M_BUSHO.BUSHO_CD
WHERE             M_SHAIN.SHAIN_CD = /*data.EigyouTantouCd*/ 
AND M_SHAIN.EIGYOU_TANTOU_KBN = 1
/*IF data.ISNOT_NEED_DELETE_FLG.IsNull || data.ISNOT_NEED_DELETE_FLG.IsFalse*/
AND M_BUSHO.DELETE_FLG = 0
AND M_SHAIN.DELETE_FLG = 0
/*END*/