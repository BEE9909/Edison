﻿--電子事業者検索
SELECT 
       MD.GYOUSHA_CD
      ,MD.JIGYOUSHA_NAME
      ,MD.EDI_MEMBER_ID
      ,MD.JIGYOUSHA_POST
      ,MD.JIGYOUSHA_TEL
      ,(ISNULL(MD.JIGYOUSHA_ADDRESS1, '') + ISNULL(MD.JIGYOUSHA_ADDRESS2, '') + ISNULL(MD.JIGYOUSHA_ADDRESS3, '')+ISNULL(MD.JIGYOUSHA_ADDRESS4, '')) AS JIGYOUSHA_ADDRESS
      ,MD.JIGYOUSHA_FAX
      ,MD.JIGYOUSHA_ADDRESS1
      ,MD.JIGYOUSHA_ADDRESS2
      ,MD.JIGYOUSHA_ADDRESS3
      ,MD.JIGYOUSHA_ADDRESS4
      ,MD.SBN_KBN
      ,MD.HST_KBN
      ,MD.UPN_KBN
      ,MG.GYOUSHA_CD AS MST_GYOUSHA_CD
      ,MG.HAISHUTSU_NIZUMI_GYOUSHA_KBN AS HAISHUTSU_JIGYOUSHA_KBN
      ,MG.UNPAN_JUTAKUSHA_KAISHA_KBN AS UNPAN_JUTAKUSHA_KBN
      ,MG.SHOBUN_NIOROSHI_GYOUSHA_KBN AS SHOBUN_JUTAKUSHA_KBN
    FROM M_DENSHI_JIGYOUSHA  MD
    LEFT JOIN M_GYOUSHA MG 
      ON MD.GYOUSHA_CD = MG.GYOUSHA_CD
WHERE MD.EDI_MEMBER_ID IN/*data.EDI_MEMBER_IDAry*/('kari')