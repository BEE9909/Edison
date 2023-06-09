SELECT GENBA_CD FROM M_GENBA
WHERE DELETE_FLG = 0
AND (MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = /*data.TORIHIKISAKI_CD*/)
UNION
SELECT GENBA_CD FROM M_HIKIAI_GENBA
WHERE DELETE_FLG = 0
AND (MANI_HENSOUSAKI_TORIHIKISAKI_CD_A = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B1 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B2 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B4 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_B6 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_C1 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_C2 = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_D = /*data.TORIHIKISAKI_CD*/
    OR MANI_HENSOUSAKI_TORIHIKISAKI_CD_E = /*data.TORIHIKISAKI_CD*/)