CREATE PROC sp_Update_SFI_TrackNum
    @intProcessCode INT ,
    @mainSynSign VARCHAR(100) ,
    @mainAGid INT ,--代理编号
    @intSpacing INT ,--同步时间间隔
    @mainOverTime INT --过期天数默认45天，就是45天之内的我们就去同步运单状态，45天之外的，我们不需要去同步运单的状态
AS
    BEGIN
        UPDATE  tb_SFI_TrackNum
        SET     int_AG_Syn = @intProcessCode
        WHERE   ( ISNULL(char_AG_Syn_sign, '0') IN ( select col FROM dbo.F_ParasAfterIn(@mainSynSign,',') ) )
                AND ( int_AG_Syn = 0 )
                AND ( int_AGid = @mainAGid )
                AND ( char_IsStop = 'N' )
                AND DATEDIFF(MINUTE,
                             ISNULL(dttm_AG_LastSyn,
                                    CONVERT(DATETIME, '2015-01-01 00:00:00', 102)),
                             GETDATE()) > @intSpacing
                AND DATEDIFF(DAY, dttm_updateDttm, GETDATE()) < @mainOverTime;
    END;


