SELECT
	SYUKKINSAKI_CD PKEY1,
	CAST(NULL AS varchar) PKEY2,
	CAST('6' AS varchar) MENU_CD,
	CAST('1' AS varchar) ITEM_CD,
	CAST('' AS varchar) MENU_NAME,
	CAST('' AS varchar) ITEM_NAME,
	CAST(0 AS bit) CHANGE_FLG,
	SYUKKINSAKI_POST POST,
	SYUKKINSAKI_ADDRESS1 ADDRESS1,
	SYUKKINSAKI_ADDRESS2 ADDRESS2
  FROM M_SYUKKINSAKI
/*BEGIN*/WHERE
 /*IF data.SYUKKINSAKI_POST != null*/
 SYUKKINSAKI_POST LIKE '%' + /*data.SYUKKINSAKI_POST*/'100-0001' + '%'
 /*END*/
 /*IF data.SYUKKINSAKI_ADDRESS1 != null*/AND (SYUKKINSAKI_ADDRESS1 + SYUKKINSAKI_ADDRESS2) LIKE '%' + /*data.SYUKKINSAKI_ADDRESS1*/'�Z��' + '%'/*END*/
/*END*/
