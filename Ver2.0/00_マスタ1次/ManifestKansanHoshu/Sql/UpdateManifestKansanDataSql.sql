UPDATE M_MANIFEST_KANSAN SET
	HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*/'',
	HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/'',
	UNIT_CD = /*data.UNIT_CD*/0,
	NISUGATA_CD = /*data.NISUGATA_CD*/'',
	KANSANSHIKI = /*data.KANSANSHIKI*/0,
	KANSANCHI = /*data.KANSANCHI*/0,
	MANIFEST_KANSAN_BIKOU = /*data.MANIFEST_KANSAN_BIKOU*/'',
	UPDATE_USER = /*data.UPDATE_USER*/'',
	UPDATE_DATE = CONVERT(DATETIME, /*data.UPDATE_DATE*/null, 120),
	UPDATE_PC = /*data.UPDATE_PC*/'',
	DELETE_FLG = /*data.DELETE_FLG*/0
WHERE HOUKOKUSHO_BUNRUI_CD = /*updateKey.HOUKOKUSHO_BUNRUI_CD*/''
  AND HAIKI_NAME_CD = /*updateKey.HAIKI_NAME_CD*/''
  AND UNIT_CD = /*updateKey.UNIT_CD*/0
  AND NISUGATA_CD = /*updateKey.NISUGATA_CD*/''
