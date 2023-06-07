﻿SELECT M_HIKIAI_TORIHIKISAKI.*,
M_KYOTEN.KYOTEN_NAME, M_BUSHO.BUSHO_NAME, M_FREE_ITEM.FREE_ITEM_NAME, M_GYOUSHU.GYOUSHU_NAME, M_SHAIN.SHAIN_NAME 
FROM M_HIKIAI_TORIHIKISAKI 
LEFT OUTER JOIN M_SHAIN ON M_HIKIAI_TORIHIKISAKI.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD 
LEFT OUTER JOIN M_GYOUSHU ON M_HIKIAI_TORIHIKISAKI.GYOUSHU_CD = M_GYOUSHU.GYOUSHU_CD 
LEFT OUTER JOIN M_KYOTEN ON M_HIKIAI_TORIHIKISAKI.KYOTEN_CD = M_KYOTEN.KYOTEN_CD 
LEFT OUTER JOIN M_BUSHO ON M_HIKIAI_TORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD = M_BUSHO.BUSHO_CD 
LEFT OUTER JOIN M_FREE_ITEM ON M_HIKIAI_TORIHIKISAKI.SHUUKEI_ITEM_CD = M_FREE_ITEM.FREE_ITEM_CD 
LEFT OUTER JOIN M_EIGYOU_TANTOUSHA ON M_SHAIN.SHAIN_CD = M_EIGYOU_TANTOUSHA.SHAIN_CD 
WHERE M_HIKIAI_TORIHIKISAKI.TORIHIKISAKI_CD = /*torihikisakiCd*/000001