﻿UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_A = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B1 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B2 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B4 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_B6 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_C1 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_C2 = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_D = 1

UPDATE M_HIKIAI_GENBA 
SET MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1
AND   MANI_HENSOUSAKI_PLACE_KBN_E = 1

UPDATE M_HIKIAI_GENBA 
SET HIKIAI_TORIHIKISAKI_USE_FLG = 0
,   TORIHIKISAKI_CD = /*newTORIHIKISAKI_CD*/0
WHERE TORIHIKISAKI_CD = /*oldTORIHIKISAKI_CD*/0
AND   HIKIAI_TORIHIKISAKI_USE_FLG = 1