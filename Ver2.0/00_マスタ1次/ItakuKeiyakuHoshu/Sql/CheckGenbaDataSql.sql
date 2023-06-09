SELECT
	M_GENBA.GYOUSHA_CD,
	M_GENBA.GENBA_CD,
	M_GENBA.GENBA_NAME_RYAKU,
	M_GENBA.GENBA_ADDRESS1,
	M_GENBA.GENBA_ADDRESS2,
	M_TODOUFUKEN.TODOUFUKEN_NAME,
	M_TODOUFUKEN.TODOUFUKEN_NAME_RYAKU
FROM
	M_GENBA
		INNER JOIN M_GYOUSHA ON M_GYOUSHA.GYOUSHA_CD = M_GENBA.GYOUSHA_CD AND M_GYOUSHA.DELETE_FLG = 0 
        LEFT JOIN M_TODOUFUKEN ON M_TODOUFUKEN.TODOUFUKEN_CD = M_GENBA.GENBA_TODOUFUKEN_CD
WHERE M_GENBA.GYOUSHA_CD = /*data.GYOUSHA_CD*/'000001'
  AND M_GENBA.GENBA_CD = /*data.GENBA_CD*/'000001'
  AND M_GENBA.DELETE_FLG = 0
  /*IF !data.HAISHUTSU_NIZUMI_GENBA_KBN.IsNull*/AND M_GENBA.HAISHUTSU_NIZUMI_GENBA_KBN = 1/*END*/
  /*IF !data.TSUMIKAEHOKAN_KBN.IsNull*/AND M_GENBA.TSUMIKAEHOKAN_KBN = 1/*END*/
  /*IF !data.SHOBUN_NIOROSHI_GENBA_KBN.IsNull && data.SAISHUU_SHOBUNJOU_KBN.IsNull*/AND M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN = 1/*END*/
  /*IF data.SHOBUN_NIOROSHI_GENBA_KBN.IsNull && !data.SAISHUU_SHOBUNJOU_KBN.IsNull*/AND M_GENBA.SAISHUU_SHOBUNJOU_KBN = 1/*END*/
  /*IF !data.SHOBUN_NIOROSHI_GENBA_KBN.IsNull && !data.SAISHUU_SHOBUNJOU_KBN.IsNull*/AND (M_GENBA.SHOBUN_NIOROSHI_GENBA_KBN = 1 OR M_GENBA.SAISHUU_SHOBUNJOU_KBN = 1)/*END*/
  /*IF !gyousha.HAISHUTSU_NIZUMI_GYOUSHA_KBN.IsNull*/AND M_GYOUSHA.HAISHUTSU_NIZUMI_GYOUSHA_KBN = 1/*END*/
  /*IF !gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsNull*/AND M_GYOUSHA.UNPAN_JUTAKUSHA_KAISHA_KBN = 1/*END*/
  /*IF !gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsNull*/AND M_GYOUSHA.SHOBUN_NIOROSHI_GYOUSHA_KBN = 1/*END*/
