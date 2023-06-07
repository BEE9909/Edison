SELECT 
    SHA.*
    ,SHO.SHOBUN_TANTOUSHA_BIKOU
    ,SHO.DELETE_FLG
FROM 
    dbo.M_SHAIN SHA
    INNER JOIN dbo.M_SHOBUN_TANTOUSHA SHO ON SHA.SHAIN_CD = SHO.SHAIN_CD
/*BEGIN*/WHERE
 /*IF data.SHAIN_CD != null*/
 SHO.SHAIN_CD LIKE '%' + /*data.SHAIN_CD*/'000001' + '%'
 /*END*/
 /*IF data.SHOBUN_TANTOUSHA_BIKOU != null*/AND SHO.SHOBUN_TANTOUSHA_BIKOU LIKE '%' +  /*data.SHOBUN_TANTOUSHA_BIKOU*/ + '%'/*END*/
 /*IF !deletechuFlg*/AND SHO.DELETE_FLG = 0/*END*/
/*END*/
ORDER BY SHO.SHAIN_CD
