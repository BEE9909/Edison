﻿UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_A = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_A = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_B1 = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B1 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_B2 = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B2 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_B4 = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B4 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_B6 = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B6 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_C1 = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_C1 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_C2 = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_C2 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_D = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_D = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_GYOUSHA_CD_E = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
AND   HIKIAI_GYOUSHA_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_E = 1

UPDATE M_HIKIAI_GENBA 
SET HIKIAI_GYOUSHA_USE_FLG = 0, 
    GYOUSHA_CD = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
 AND HIKIAI_GYOUSHA_USE_FLG = 1
 
UPDATE M_HIKIAI_GENBA_TEIKI_HINMEI 
SET HIKIAI_GYOUSHA_USE_FLG = 0, 
    GYOUSHA_CD = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
 AND HIKIAI_GYOUSHA_USE_FLG = 1

UPDATE M_HIKIAI_GENBA_TSUKI_HINMEI 
SET HIKIAI_GYOUSHA_USE_FLG = 0, 
    GYOUSHA_CD = /*newGYOUSHA_CD*/0
WHERE GYOUSHA_CD = /*oldGYOUSHA_CD*/0
 AND HIKIAI_GYOUSHA_USE_FLG = 1
