SELECT 
    '���' AS DENSHU_KBN_NAME,
	DENPYOU_DATE,
	UKEIRE_NUMBER AS DENPYOU_NUMBER,
    UNPAN_GYOUSHA_NAME,
    SHASHU_NAME,
    SHARYOU_NAME,
    UNTENSHA_NAME,
    TAIRYUU_BIKOU,
    SYSTEM_ID
FROM T_UKEIRE_ENTRY
WHERE DELETE_FLG = 0 
AND TAIRYUU_KBN = 1
AND CONVERT(nvarchar(10),DENPYOU_DATE,111) = /*date*/