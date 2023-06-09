SELECT
	MAN_KAN.*
   ,ISNULL(HAI.HAIKI_NAME_RYAKU, N'') AS HAIKI_NAME_RYAKU
   ,ISNULL(UNI.UNIT_NAME_RYAKU, N'') AS UNIT_NAME
   ,ISNULL(NIS.NISUGATA_NAME, N'') AS NISUGATA_NAME
   ,MAN_KAN.HOUKOKUSHO_BUNRUI_CD AS UK_HOUKOKUSHO_BUNRUI_CD
   ,MAN_KAN.HAIKI_NAME_CD AS UK_HAIKI_NAME_CD
   ,MAN_KAN.UNIT_CD AS UK_UNIT_CD
   ,MAN_KAN.NISUGATA_CD AS UK_NISUGATA_CD
FROM 
    dbo.M_MANIFEST_KANSAN MAN_KAN
	LEFT JOIN dbo.M_HAIKI_NAME HAI ON HAI.HAIKI_NAME_CD = MAN_KAN.HAIKI_NAME_CD
	LEFT JOIN dbo.M_UNIT UNI ON UNI.UNIT_CD = MAN_KAN.UNIT_CD
	LEFT JOIN dbo.M_NISUGATA NIS ON NIS.NISUGATA_CD = MAN_KAN.NISUGATA_CD
/*BEGIN*/WHERE
	/*IF 0==1*/
	MAN_KAN.HOUKOKUSHO_BUNRUI_CD LIKE '%' + /*data.HOUKOKUSHO_BUNRUI_CD*/'000001' + '%'
	/*END*/
	/*IF data.HAIKI_NAME_CD != null*/AND MAN_KAN.HAIKI_NAME_CD LIKE '%' +  /*data.HAIKI_NAME_CD*/ + '%'/*END*/
	/*IF !data.UNIT_CD.IsNull*/AND MAN_KAN.UNIT_CD = /*data.UNIT_CD*/0/*END*/
	/*IF data.NISUGATA_CD != null*/AND MAN_KAN.NISUGATA_CD LIKE '%' +  /*data.NISUGATA_CD*/ + '%'/*END*/
	/*IF !data.KANSANSHIKI.IsNull*/AND MAN_KAN.KANSANSHIKI = /*data.KANSANSHIKI*/0/*END*/
	/*IF !data.KANSANCHI.IsNull*/AND MAN_KAN.KANSANCHI = /*data.KANSANCHI*/0/*END*/
	/*IF data.MANIFEST_KANSAN_BIKOU != null*/AND MAN_KAN.MANIFEST_KANSAN_BIKOU LIKE '%' +  /*data.MANIFEST_KANSAN_BIKOU*/ + '%'/*END*/
    /*IF data.HAIKI_NAME_RYAKU != null*/AND HAI.HAIKI_NAME_RYAKU LIKE '%' +  /*data.HAIKI_NAME_RYAKU*/ + '%'/*END*/
	/*IF data.UNIT_NAME != null*/AND UNI.UNIT_NAME LIKE '%' +  /*data.UNIT_NAME*/ + '%'/*END*/
	/*IF data.NISUGATA_NAME != null*/AND NIS.NISUGATA_NAME LIKE '%' +  /*data.NISUGATA_NAME*/ + '%'/*END*/
	/*IF data.CREATE_USER != null*/AND MAN_KAN.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, MAN_KAN.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
	/*IF data.UPDATE_USER != null*/AND MAN_KAN.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, MAN_KAN.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
	/*IF !deletechuFlg*/AND MAN_KAN.DELETE_FLG = /*deletechuFlg*/0/*END*/
/*END*/
ORDER BY
	MAN_KAN.HAIKI_NAME_CD
	,MAN_KAN.UNIT_CD
	,MAN_KAN.NISUGATA_CD
