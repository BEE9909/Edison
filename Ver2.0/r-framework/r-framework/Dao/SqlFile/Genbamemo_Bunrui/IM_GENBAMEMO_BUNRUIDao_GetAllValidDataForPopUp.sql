﻿SELECT * FROM dbo.M_GENBAMEMO_BUNRUI
WHERE DELETE_FLG = 0
/*IF data.GENBAMEMO_BUNRUI_CD != null*/AND GENBAMEMO_BUNRUI_CD = /*data.GENBAMEMO_BUNRUI_CD*//*END*/
/*IF data.GENBAMEMO_BUNRUI_NAME != null*/AND GENBAMEMO_BUNRUI_NAME = /*data.GENBAMEMO_BUNRUI_NAME*//*END*/
/*IF data.GENBAMEMO_BUNRUI_NAME_RYAKU != null*/AND GENBAMEMO_BUNRUI_NAME_RYAKU = /*data.GENBAMEMO_BUNRUI_NAME_RYAKU*//*END*/
/*IF data.GENBAMEMO_BUNRUI_FURIGANA != null*/AND GENBAMEMO_BUNRUI_FURIGANA = /*data.GENBAMEMO_BUNRUI_FURIGANA*//*END*/
/*IF data.CREATE_USER != null*/AND CREATE_USER = /*data.CREATE_USER*//*END*/
/*IF !data.CREATE_DATE.IsNull*/AND CREATE_DATE = /*data.CREATE_DATE.Value*//*END*/
/*IF data.CREATE_PC != null*/AND CREATE_PC = /*data.CREATE_PC*//*END*/
/*IF data.UPDATE_USER != null*/AND UPDATE_USER = /*data.UPDATE_USER*//*END*/
/*IF !data.UPDATE_DATE.IsNull*/AND UPDATE_DATE = /*data.UPDATE_DATE.Value*//*END*/
/*IF data.UPDATE_PC != null*/AND UPDATE_PC = /*data.UPDATE_PC*//*END*/
