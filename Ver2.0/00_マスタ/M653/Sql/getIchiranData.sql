SELECT
    M_DENSHI_MANIFEST_KANSAN.DELETE_FLG
    ,M_DENSHI_MANIFEST_KANSAN.HAIKI_SHURUI_SAIBUNRUI_CD
    ,M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_NAME
    ,M_DENSHI_MANIFEST_KANSAN.UNIT_CD
    ,(CASE WHEN M_DENSHI_MANIFEST_KANSAN.KANSANSHIKI = 1 THEN '��' ELSE '�~' END) AS KANSANSHIKI
    ,M_DENSHI_MANIFEST_KANSAN.KANSANCHI
    ,M_DENSHI_MANIFEST_KANSAN.MANIFEST_KANSAN_BIKOU
    ,M_DENSHI_MANIFEST_KANSAN.CREATE_USER
    ,M_DENSHI_MANIFEST_KANSAN.CREATE_DATE
    ,M_DENSHI_MANIFEST_KANSAN.UPDATE_USER
    ,M_DENSHI_MANIFEST_KANSAN.UPDATE_DATE
    ,M_UNIT.UNIT_NAME_RYAKU
FROM
    M_DENSHI_MANIFEST_KANSAN
LEFT JOIN
    M_DENSHI_HAIKI_SHURUI_SAIBUNRUI
    ON ((M_DENSHI_MANIFEST_KANSAN.EDI_MEMBER_ID = M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.EDI_MEMBER_ID)
    AND (M_DENSHI_MANIFEST_KANSAN.HAIKI_SHURUI_CD = M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_CD)
    AND (M_DENSHI_MANIFEST_KANSAN.HAIKI_SHURUI_SAIBUNRUI_CD = M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_SAIBUNRUI_CD)
    AND (M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.DELETE_FLG = 0))
LEFT JOIN
    M_UNIT
    ON ((M_DENSHI_MANIFEST_KANSAN.UNIT_CD = M_UNIT.UNIT_CD)
    AND (M_UNIT.DELETE_FLG = 0))
WHERE 1=1
/*IF data.entity.EDI_MEMBER_ID != null*/AND M_DENSHI_MANIFEST_KANSAN.EDI_MEMBER_ID = /*data.entity.EDI_MEMBER_ID*//*END*/
/*IF data.entity.HAIKI_SHURUI_CD != null*/AND M_DENSHI_MANIFEST_KANSAN.HAIKI_SHURUI_CD = /*data.entity.HAIKI_SHURUI_CD*//*END*/
/*IF data.entity.HAIKI_SHURUI_SAIBUNRUI_CD != null*/AND M_DENSHI_MANIFEST_KANSAN.HAIKI_SHURUI_SAIBUNRUI_CD LIKE '%' + /*data.entity.HAIKI_SHURUI_SAIBUNRUI_CD*/ + '%'/*END*/
/*IF data.HAIKI_SHURUI_NAME != null*/AND M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_NAME LIKE '%' + /*data.HAIKI_SHURUI_NAME*/ + '%'/*END*/
/*IF data.UNIT_NAME_RYAKU != null*/AND M_UNIT.UNIT_NAME_RYAKU LIKE '%' + /*data.UNIT_NAME_RYAKU*/ + '%'/*END*/
/*IF !data.entity.KANSANCHI.IsNull*/AND M_DENSHI_MANIFEST_KANSAN.KANSANCHI = /*data.entity.KANSANCHI*//*END*/
/*IF data.entity.MANIFEST_KANSAN_BIKOU != null*/AND M_DENSHI_MANIFEST_KANSAN.MANIFEST_KANSAN_BIKOU LIKE '%' + /*data.entity.MANIFEST_KANSAN_BIKOU*/ + '%'/*END*/
/*IF data.entity.CREATE_USER != null*/AND M_DENSHI_MANIFEST_KANSAN.CREATE_USER LIKE '%' + /*data.entity.CREATE_USER*/ + '%'/*END*/
/*IF data.entity.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, M_DENSHI_MANIFEST_KANSAN.CREATE_DATE, 111) LIKE '%' + /*data.entity.SEARCH_CREATE_DATE*/ + '%'/*END*/
/*IF data.entity.UPDATE_USER != null*/AND M_DENSHI_MANIFEST_KANSAN.UPDATE_USER LIKE '%' + /*data.entity.UPDATE_USER*/ + '%'/*END*/
/*IF data.entity.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, M_DENSHI_MANIFEST_KANSAN.UPDATE_DATE, 111) LIKE '%' + /*data.entity.SEARCH_UPDATE_DATE*/ + '%'/*END*/
/*IF !data.SHOW_CONDITION_DELETED*/AND M_DENSHI_MANIFEST_KANSAN.DELETE_FLG = 0/*END*/
ORDER BY
    M_DENSHI_MANIFEST_KANSAN.EDI_MEMBER_ID
    ,M_DENSHI_MANIFEST_KANSAN.HAIKI_SHURUI_CD
    ,M_DENSHI_MANIFEST_KANSAN.HAIKI_SHURUI_SAIBUNRUI_CD
    ,M_DENSHI_MANIFEST_KANSAN.UNIT_CD