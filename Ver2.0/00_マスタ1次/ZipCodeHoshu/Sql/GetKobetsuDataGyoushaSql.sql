SELECT
	GYOUSHA_CD PKEY1,
	CAST(NULL AS varchar) PKEY2,
	CAST('3' AS varchar) MENU_CD,
	CAST('1' AS varchar) ITEM_CD,
	CAST('' AS varchar) MENU_NAME,
	CAST('' AS varchar) ITEM_NAME,
	CAST(0 AS bit) CHANGE_FLG,
	GYOUSHA_POST POST,
	GYOUSHA_ADDRESS1 ADDRESS1,
	GYOUSHA_ADDRESS2 ADDRESS2
  FROM M_GYOUSHA
/*BEGIN*/WHERE
 /*IF data.GYOUSHA_POST != null*/
 GYOUSHA_POST LIKE '%' + /*data.GYOUSHA_POST*/'100-0001' + '%'
 /*END*/
 /*IF data.GYOUSHA_ADDRESS1 != null*/AND (GYOUSHA_ADDRESS1 + GYOUSHA_ADDRESS2) LIKE '%' + /*data.GYOUSHA_ADDRESS1*/'�Z��' + '%'/*END*/
/*END*/

UNION ALL

SELECT
	GYOUSHA_CD PKEY1,
	CAST(NULL AS varchar) PKEY2,
	CAST('3' AS varchar) MENU_CD,
	CAST('2' AS varchar) ITEM_CD,
	CAST('' AS varchar) MENU_NAME,
	CAST('' AS varchar) ITEM_NAME,
	CAST(0 AS bit) CHANGE_FLG,
	SEIKYUU_SOUFU_POST POST,
	SEIKYUU_SOUFU_ADDRESS1 ADDRESS1,
	SEIKYUU_SOUFU_ADDRESS2 ADDRESS2
  FROM M_GYOUSHA
/*BEGIN*/WHERE
 /*IF data.SEIKYUU_SOUFU_POST != null*/
 SEIKYUU_SOUFU_POST LIKE '%' + /*data.SEIKYUU_SOUFU_POST*/'100-0001' + '%'
 /*END*/
 /*IF data.SEIKYUU_SOUFU_ADDRESS1 != null*/AND (SEIKYUU_SOUFU_ADDRESS1 + SEIKYUU_SOUFU_ADDRESS2) LIKE '%' + /*data.SEIKYUU_SOUFU_ADDRESS1*/'�Z��' + '%'/*END*/
/*END*/

UNION ALL

SELECT
	GYOUSHA_CD PKEY1,
	CAST(NULL AS varchar) PKEY2,
	CAST('3' AS varchar) MENU_CD,
	CAST('3' AS varchar) ITEM_CD,
	CAST('' AS varchar) MENU_NAME,
	CAST('' AS varchar) ITEM_NAME,
	CAST(0 AS bit) CHANGE_FLG,
	SHIHARAI_SOUFU_POST POST,
	SHIHARAI_SOUFU_ADDRESS1 ADDRESS1,
	SHIHARAI_SOUFU_ADDRESS2 ADDRESS2
  FROM M_GYOUSHA
/*BEGIN*/WHERE
 /*IF data.SHIHARAI_SOUFU_POST != null*/
 SHIHARAI_SOUFU_POST LIKE '%' + /*data.SHIHARAI_SOUFU_POST*/'100-0001' + '%'
 /*END*/
 /*IF data.SHIHARAI_SOUFU_ADDRESS1 != null*/AND (SHIHARAI_SOUFU_ADDRESS1 + SHIHARAI_SOUFU_ADDRESS2) LIKE '%' + /*data.SHIHARAI_SOUFU_ADDRESS1*/'�Z��' + '%'/*END*/
/*END*/

UNION ALL

SELECT
	GYOUSHA_CD PKEY1,
	CAST(NULL AS varchar) PKEY2,
	CAST('3' AS varchar) MENU_CD,
	CAST('4' AS varchar) ITEM_CD,
	CAST('' AS varchar) MENU_NAME,
	CAST('' AS varchar) ITEM_NAME,
	CAST(0 AS bit) CHANGE_FLG,
	MANI_HENSOUSAKI_POST POST,
	MANI_HENSOUSAKI_ADDRESS1 ADDRESS1,
	MANI_HENSOUSAKI_ADDRESS2 ADDRESS2
  FROM M_GYOUSHA
/*BEGIN*/WHERE
 /*IF data.MANI_HENSOUSAKI_POST != null*/
 MANI_HENSOUSAKI_POST LIKE '%' + /*data.MANI_HENSOUSAKI_POST*/'100-0001' + '%'
 /*END*/
 /*IF data.MANI_HENSOUSAKI_ADDRESS1 != null*/AND (MANI_HENSOUSAKI_ADDRESS1 + MANI_HENSOUSAKI_ADDRESS2) LIKE '%' + /*data.MANI_HENSOUSAKI_ADDRESS1*/'�Z��' + '%'/*END*/
/*END*/
