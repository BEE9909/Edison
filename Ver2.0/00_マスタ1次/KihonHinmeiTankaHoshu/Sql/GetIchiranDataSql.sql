SELECT
    KHT.*
    ,ISNULL(HM.HINMEI_NAME_RYAKU,N'') AS HINMEI_NAME_RYAKU
    ,ISNULL(DS.DENSHU_KBN_NAME_RYAKU,N'') AS DENSHU_KBN_NAME_RYAKU
    ,ISNULL(UN.UNIT_NAME_RYAKU,N'') AS UNIT_NAME_RYAKU
    ,ISNULL(GSA.GYOUSHA_NAME_RYAKU,N'') AS UNPAN_GYOUSHA_RYAKU
    ,ISNULL(GS.GYOUSHA_NAME_RYAKU,N'') AS NIOROSHI_GYOUSHA_RYAKU
    ,ISNULL(GB.GENBA_NAME_RYAKU,N'') AS NIOROSHI_GENBA_RYAKU
FROM
    dbo.M_KIHON_HINMEI_TANKA KHT
    LEFT JOIN dbo.M_HINMEI HM ON HM.HINMEI_CD = KHT.HINMEI_CD
    LEFT JOIN dbo.M_DENSHU_KBN DS ON DS.DENSHU_KBN_CD = KHT.DENSHU_KBN_CD
    LEFT JOIN dbo.M_UNIT UN ON UN.UNIT_CD = KHT.UNIT_CD
    LEFT JOIN dbo.M_GYOUSHA GSA ON GSA.GYOUSHA_CD = KHT.UNPAN_GYOUSHA_CD
    LEFT JOIN dbo.M_GYOUSHA GS ON GS.GYOUSHA_CD = KHT.NIOROSHI_GYOUSHA_CD
    LEFT JOIN dbo.M_GENBA GB ON GB.GYOUSHA_CD = KHT.NIOROSHI_GYOUSHA_CD AND GB.GENBA_CD = KHT.NIOROSHI_GENBA_CD
WHERE
	KHT.DENPYOU_KBN_CD =/*data.DENPYOU_KBN_CD*/0
	/*IF data.HINMEI_CD != null*/ AND KHT.HINMEI_CD LIKE '%' + /*data.HINMEI_CD*/ + '%'/*END*/
	/*IF !data.DENSHU_KBN_CD.IsNull*/AND KHT.DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/0/*END*/
	/*IF !data.UNIT_CD.IsNull */AND KHT.UNIT_CD = /*data.UNIT_CD*/0/*END*/
	/*IF data.UNPAN_GYOUSHA_CD != null*/AND KHT.UNPAN_GYOUSHA_CD LIKE '%' + /*data.UNPAN_GYOUSHA_CD*/ + '%'/*END*/
	/*IF data.NIOROSHI_GYOUSHA_CD != null*/AND KHT.NIOROSHI_GYOUSHA_CD LIKE '%' + /*data.NIOROSHI_GYOUSHA_CD*/ + '%'/*END*/
	/*IF data.NIOROSHI_GENBA_CD != null*/AND KHT.NIOROSHI_GENBA_CD LIKE '%' + /*data.NIOROSHI_GENBA_CD*/ + '%'/*END*/
	/*IF !data.TANKA.IsNull*/AND KHT.TANKA= /*data.TANKA*/0/*END*/ 
	/*IF data.BIKOU != null*/AND KHT.BIKOU LIKE '%' + /*data.BIKOU*/ + '%'/*END*/
	/*IF !data.TEKIYOU_BEGIN.IsNull*/ AND KHT.TEKIYOU_BEGIN LIKE '%' +  /*data.TEKIYOU_BEGIN.Value*/ + '%'/*END*/
	/*IF !data.TEKIYOU_END.IsNull*/AND KHT.TEKIYOU_END LIKE '%' +  /*data.TEKIYOU_END.Value*/ + '%'/*END*/
	/*IF data.CREATE_USER != null*/AND KHT.CREATE_USER LIKE '%' +  /*data.CREATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_CREATE_DATE != null*/AND CONVERT(nvarchar, KHT.CREATE_DATE, 120) LIKE '%' +  /*data.SEARCH_CREATE_DATE*/ + '%'/*END*/
	/*IF data.UPDATE_USER != null*/AND KHT.UPDATE_USER LIKE '%' +  /*data.UPDATE_USER*/ + '%'/*END*/
	/*IF data.SEARCH_UPDATE_DATE != null*/AND CONVERT(nvarchar, KHT.UPDATE_DATE, 120) LIKE '%' +  /*data.SEARCH_UPDATE_DATE*/ + '%'/*END*/
	/*IF !deletechuFlg*/AND KHT.DELETE_FLG = 0/*END*/
	/*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/AND (1 = 0/*END*/
	/*IF tekiyounaiFlg*/OR (((KHT.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= KHT.TEKIYOU_END) or (KHT.TEKIYOU_BEGIN <= CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) and KHT.TEKIYOU_END IS NULL) or (KHT.TEKIYOU_BEGIN IS NULL and CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) <= KHT.TEKIYOU_END) or (KHT.TEKIYOU_BEGIN IS NULL and KHT.TEKIYOU_END IS NULL)) and KHT.DELETE_FLG = 0)/*END*/
	/*IF deletechuFlg*/OR KHT.DELETE_FLG = /*deletechuFlg*/0/*END*/
	/*IF tekiyougaiFlg*/OR ((KHT.TEKIYOU_BEGIN > CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) or CONVERT(DATETIME, CONVERT(nvarchar, GETDATE(), 111), 120) > KHT.TEKIYOU_END) and KHT.DELETE_FLG = 0)/*END*/
	/*IF tekiyounaiFlg || deletechuFlg || tekiyougaiFlg*/ )/*END*/
	/*IF syuruishiteiFlg*/AND HM.SHURUI_CD =  /*syurui*/ /*END*/
ORDER BY KHT.DENSHU_KBN_CD, KHT.HINMEI_CD, KHT.UNIT_CD, KHT.UNPAN_GYOUSHA_CD, KHT.NIOROSHI_GYOUSHA_CD, KHT.NIOROSHI_GENBA_CD, KHT.TEKIYOU_BEGIN
