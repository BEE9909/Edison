﻿select 
	SHAIN.SHAIN_NAME 
from 
	dbo.M_SHAIN as SHAIN,
	dbo.M_EIGYOU_TANTOUSHA as TANTOUSHA 
where 
	TANTOUSHA.SHAIN_CD=SHAIN.SHAIN_CD 
and TANTOUSHA.SHAIN_CD = /*shainCd*/000001