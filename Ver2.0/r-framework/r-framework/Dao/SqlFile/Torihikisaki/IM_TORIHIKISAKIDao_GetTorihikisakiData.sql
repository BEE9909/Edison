﻿SELECT M_TORIHIKISAKI.*,
M_KYOTEN.KYOTEN_NAME, M_BUSHO.BUSHO_NAME, M_GYOUSHU.GYOUSHU_NAME, M_SHAIN.SHAIN_NAME 
FROM M_TORIHIKISAKI 
LEFT OUTER JOIN M_SHAIN ON M_TORIHIKISAKI.EIGYOU_TANTOU_CD = M_SHAIN.SHAIN_CD 
LEFT OUTER JOIN M_GYOUSHU ON M_TORIHIKISAKI.GYOUSHU_CD = M_GYOUSHU.GYOUSHU_CD 
LEFT OUTER JOIN M_KYOTEN ON M_TORIHIKISAKI.TORIHIKISAKI_KYOTEN_CD = M_KYOTEN.KYOTEN_CD
LEFT OUTER JOIN M_BUSHO ON M_TORIHIKISAKI.EIGYOU_TANTOU_BUSHO_CD = M_BUSHO.BUSHO_CD 
LEFT OUTER JOIN M_EIGYOU_TANTOUSHA ON M_SHAIN.SHAIN_CD = M_EIGYOU_TANTOUSHA.SHAIN_CD 
WHERE M_TORIHIKISAKI.TORIHIKISAKI_CD =  /*torihikisakiCd*/000001