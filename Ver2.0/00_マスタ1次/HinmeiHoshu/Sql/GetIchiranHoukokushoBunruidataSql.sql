SELECT 
    HINMEI.*
    ,ISNULL(MJB.JISSEKI_BUNRUI_NAME_RYAKU,N'') AS JISSEKI_BUNRUI_NAME_RYAKU
    ,ISNULL(UNIT.UNIT_NAME_RYAKU,N'') AS UNIT_NAME_RYAKU
    ,ISNULL(DENSHU.DENSHU_KBN_NAME_RYAKU,N'') AS DENSHU_KBN_NAME_RYAKU
    ,ISNULL(DENPYOU.DENPYOU_KBN_NAME,N'') AS DENPYOU_KBN_NAME_RYAKU
    ,ISNULL(SHURUI.SHURUI_NAME_RYAKU,N'') AS SHURUI_NAME_RYAKU
    ,ISNULL(BUNRUI.BUNRUI_NAME_RYAKU,N'') AS BUNRUI_NAME_RYAKU
    ,ISNULL(HOUKOKUSHO.HOUKOKUSHO_BUNRUI_NAME_RYAKU,N'') AS HOUKOKUSHO_BUNRUI_NAME_RYAKU
    ,ISNULL(SHU1.HAIKI_SHURUI_NAME_RYAKU,N'') AS SP_CHOKKOU_HAIKI_SHURUI_NAME
    ,ISNULL(SHU2.HAIKI_SHURUI_NAME_RYAKU,N'') AS SP_TSUMIKAE_HAIKI_SHURUI_NAME
    ,ISNULL(SHU3.HAIKI_SHURUI_NAME_RYAKU,N'') AS KP_HAIKI_SHURUI_NAME
    ,ISNULL(SHU4.HAIKI_SHURUI_NAME,N'') AS DM_HAIKI_SHURUI_NAME
	,CONVERT(varchar,N'') AS ZEI_KBN_NAME_RYAKU
FROM 
	dbo.M_HOUKOKUSHO_BUNRUI HOUKOKUSHO
	LEFT JOIN dbo.M_HINMEI HINMEI ON HINMEI.HOUKOKUSHO_BUNRUI_CD = HOUKOKUSHO.HOUKOKUSHO_BUNRUI_CD
	LEFT JOIN dbo.M_JISSEKI_BUNRUI MJB ON MJB.JISSEKI_BUNRUI_CD = HINMEI.JISSEKI_BUNRUI_CD
	LEFT JOIN dbo.M_UNIT UNIT ON UNIT.UNIT_CD = HINMEI.UNIT_CD
	LEFT JOIN dbo.M_DENSHU_KBN DENSHU ON DENSHU.DENSHU_KBN_CD = HINMEI.DENSHU_KBN_CD
	LEFT JOIN dbo.M_DENPYOU_KBN DENPYOU ON DENPYOU.DENPYOU_KBN_CD = HINMEI.DENPYOU_KBN_CD
	LEFT JOIN dbo.M_SHURUI SHURUI ON SHURUI.SHURUI_CD = HINMEI.SHURUI_CD
	LEFT JOIN dbo.M_BUNRUI BUNRUI ON BUNRUI.BUNRUI_CD = HINMEI.BUNRUI_CD
	LEFT JOIN dbo.M_HAIKI_SHURUI SHU1 ON SHU1.HAIKI_KBN_CD = 1 AND SHU1.HAIKI_SHURUI_CD = HINMEI.SP_CHOKKOU_HAIKI_SHURUI_CD
	LEFT JOIN dbo.M_HAIKI_SHURUI SHU2 ON SHU2.HAIKI_KBN_CD = 3 AND SHU2.HAIKI_SHURUI_CD = HINMEI.SP_TSUMIKAE_HAIKI_SHURUI_CD
	LEFT JOIN dbo.M_HAIKI_SHURUI SHU3 ON SHU3.HAIKI_KBN_CD = 2 AND SHU3.HAIKI_SHURUI_CD = HINMEI.KP_HAIKI_SHURUI_CD
	LEFT JOIN dbo.M_DENSHI_HAIKI_SHURUI SHU4 ON SHU4.HAIKI_SHURUI_CD = HINMEI.DM_HAIKI_SHURUI_CD
/*BEGIN*/WHERE
 /*IF data.HOUKOKUSHO_BUNRUI_CD != null*/HOUKOKUSHO.HOUKOKUSHO_BUNRUI_CD LIKE '%' + /*data.HOUKOKUSHO_BUNRUI_CD*/'000001' + '%'/*END*/
 /*IF data.HOUKOKUSHO_BUNRUI_NAME_RYAKU != null*/AND HOUKOKUSHO.HOUKOKUSHO_BUNRUI_NAME_RYAKU LIKE '%' +  /*data.HOUKOKUSHO_BUNRUI_NAME_RYAKU*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND HINMEI.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY HINMEI.HINMEI_CD
