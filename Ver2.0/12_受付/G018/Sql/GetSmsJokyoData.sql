SELECT
    (CASE
        WHEN SMS.SYSTEM_ID IS NOT NULL THEN '���M��'
        ELSE '�����M'
        END)
FROM T_UKETSUKE_MK_ENTRY MK
LEFT JOIN T_SMS SMS
ON MK.UKETSUKE_NUMBER = SMS.DENPYOU_NUMBER
WHERE MK.UKETSUKE_NUMBER = /*uketsukeNumber*/