﻿select 
	FILE_ID,
	FILE_PATH,
	FILE_EXTENTION,
	FILE_LENGTH,
	FILE_CREATION_TIME,
	FILE_LAST_WRITE_TIME,
	IS_READ_ONLY,
	WINDOW_NAME,
	CREATE_USER,
	CREATE_DATE,
	CREATE_PC,
	UPDATE_USER,
	UPDATE_DATE,
	UPDATE_PC,
	TIME_STAMP
FROM T_FILE_DATA
WHERE FILE_ID IN /*fileIdList*/(1,2)