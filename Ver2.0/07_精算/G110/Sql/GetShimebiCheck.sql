﻿SELECT MTS.TORIHIKISAKI_CD
FROM
M_TORIHIKISAKI_SHIHARAI as MTS
WHERE
 MTS.TORIHIKISAKI_CD = /*torihikisakiCd*/
 AND (MTS.SHIMEBI1 = /*shimebi*/
	  OR MTS.SHIMEBI2 = /*shimebi*/
      OR MTS.SHIMEBI3 = /*shimebi*/)