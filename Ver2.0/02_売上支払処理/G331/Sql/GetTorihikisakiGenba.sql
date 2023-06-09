﻿SELECT
MG.TORIHIKISAKI_CD,
MG.GYOUSHA_CD,
MG.GENBA_CD,
MG.GENBA_NAME_RYAKU,
MG.EIGYOU_TANTOU_CD,
MGTH.HINMEI_CD,
MGTH.TANKA,
MGTH.GYOUSHA_CD,
MGTH.GENBA_CD,
MT.TORIHIKISAKI_NAME_RYAKU,
MTS.TORIHIKI_KBN_CD,
MTS.SHIMEBI1
FROM
"SELECT
MG.TORIHIKISAKI_CD,
MG.GYOUSHA_CD,
MG.GENBA_CD,
MG.GENBA_NAME_RYAKU,
MG.EIGYOU_TANTOU_CD,
MGTH.HINMEI_CD,
MGTH.TANKA,
MGTH.GYOUSHA_CD,
MGTH.GENBA_CD
FROM
M_GENBA MG
INNER JOIN M_GENBA_TSUKI_HINMEI MGTH 
ON MG.GYOUSHA_CD = MGTH.GYOUSHA_CD 
AND MG.GENBA_CD = MGTH.GENBA_CD
/*BEGIN*/WHERE
/*IF data.GyousyaCD != null && data.GyousyaCD != ''*/					
MGTH.GYOUSHA_CD = /*data.GyousyaCD*//*END*/
/*IF data.GenbaCD != null && data.GenbaCD != ''*/					
AND MGTH.GENBA_CD = /*data.GenbaCD*//*END*/
/*IF data.KyotenCD != null && data.KyotenCD != ''*/					
AND MG.KYOTEN_CD = /*data.KyotenCD*//*END*/
/*IF data.TorihikisakiCD != null && data.TorihikisakiCD != ''*/					
AND MG.TORIHIKISAKI_CD = /*data.TorihikisakiCD*//*END*/
/*END*/
" AS NMG
INNER JOIN M_TORIHIKISAKI MT 
ON NMG.TORIHIKISAKI_CD = MT.TORIHIKISAKI_CD 
INNER JOIN M_TORIHIKISAKI_SEIKYUU MTS 
ON NMG.TORIHIKISAKI_CD = MTS.TORIHIKISAKI_CD
AND MTS.TORIHIKI_KBN_CD = 2
AND MTS.SHIMEBI1 = /*data.Shimebi*/
/*BEGIN*/WHERE
/*IF data.KyotenCD != null && data.KyotenCD != ''*/					
MT.KYOTEN_CD = /*data.KyotenCD*//*END*/
ORDER BY
MG.TORIHIKISAKI_CD ASC,
MG.GYOUSHA_CD ASC,
MG.GENBA_CD ASC,
MGTH.HINMEI_CD ASC
/*END*/