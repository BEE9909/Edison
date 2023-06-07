﻿SELECT TSDK.KAGAMI_NUMBER,
       M_USER_ACCOUNT_INFO_INXS.SYS_ID AS USER_SYS_ID,
       M_USER_ACCOUNT_INFO_INXS.USER_ID
FROM   T_SEISAN_DENPYOU TSD
       OUTER APPLY(SELECT TOP 1 Cast(1 AS INT) IS_USE
                   FROM   T_SEISAN_DETAIL TSDE
                   WHERE  TSDE.SEISAN_NUMBER = TSD.SEISAN_NUMBER
                          AND ( TSDE.DENPYOU_SHURUI_CD <> 20
                                 OR TSDE.DENPYOU_SHURUI_CD IS NULL )) SHUKKIN
       CROSS APPLY (SELECT TSDK.*
                    FROM   T_SEISAN_DENPYOU_KAGAMI TSDK
                           OUTER APPLY (SELECT TOP 1 Cast(1 AS INT) IS_DENPYOU
                                        FROM   T_SEISAN_DETAIL TSDE
                                        WHERE  TSDE.SEISAN_NUMBER = TSDK.SEISAN_NUMBER
                                               AND TSDE.KAGAMI_NUMBER = TSDK.KAGAMI_NUMBER
                                               AND ( TSDE.DENPYOU_SHURUI_CD <> 20
                                                      OR TSDE.DENPYOU_SHURUI_CD IS NULL )) DENPYOU
                    WHERE  TSDK.SEISAN_NUMBER = TSD.SEISAN_NUMBER
                           AND ( SHUKKIN.IS_USE IS NULL
                                  OR ( DENPYOU.IS_DENPYOU IS NOT NULL ) )) TSDK
       LEFT JOIN M_USER_ACCOUNT_INFO_INXS
               ON ( ( TSD.SHOSHIKI_KBN = 1
                      AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = TSDK.TORIHIKISAKI_CD )
                     OR ( TSD.SHOSHIKI_KBN = 2
                          AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = TSDK.TORIHIKISAKI_CD
                                 OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = TSDK.GYOUSHA_CD
                                      AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NULL
                                             OR M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = '' ) ) ) )
                     OR ( TSD.SHOSHIKI_KBN = 3
                          AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_TORIHIKISAKI_CD = TSDK.TORIHIKISAKI_CD
                                 OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = TSDK.GYOUSHA_CD
                                      AND ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NULL
                                             OR M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = '' ) )
                                 OR ( M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GYOUSHA_CD = TSDK.GYOUSHA_CD
                                      AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD = TSDK.GENBA_CD
                                      AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD IS NOT NULL
                                      AND M_USER_ACCOUNT_INFO_INXS.SHOUGUN_GENBA_CD != '' ) ) ) )
                  AND ( ( M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG IS NULL
                        OR M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG = 0 )
                        OR ( M_USER_ACCOUNT_INFO_INXS.TOUKETSU_FLG = 1
                            AND CONVERT(DATE, M_USER_ACCOUNT_INFO_INXS.TOUKETSU_DATE) > CONVERT(DATE, Getdate()) ) )
                  AND M_USER_ACCOUNT_INFO_INXS.DELETE_FLG = 0
                  AND M_USER_ACCOUNT_INFO_INXS.REGISTER_STATUS <> 2
                  /*IF ignoreUserSysIds != null*/
                  AND M_USER_ACCOUNT_INFO_INXS.SYS_ID IN /*ignoreUserSysIds*/()
                  /*END*/
                  AND ( 1 = 0
                          /*IF userSysIds != null*/
                          OR M_USER_ACCOUNT_INFO_INXS.SYS_ID IN /*userSysIds*/()
                          /*END*/
                          OR M_USER_ACCOUNT_INFO_INXS.IS_SEND_PAYMENT = Cast(1 AS BIT) ) 
WHERE  TSD.SEISAN_NUMBER = /*seisanNumber*/