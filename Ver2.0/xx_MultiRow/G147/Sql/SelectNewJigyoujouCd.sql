﻿SELECT 
  RIGHT('0000000000' + convert(nvarchar, ISNULL(MAX(JIGYOUJOU_CD), 0) + 1), 10) 
FROM M_DENSHI_JIGYOUJOU 
WHERE EDI_MEMBER_ID = /*ediMemberID*/
