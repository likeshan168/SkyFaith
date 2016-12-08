CREATE PROC sp_Get_SFI_TrackNum
    @intProcessCode BIGINT ,--处理状态编码
    @mainSynSign VARCHAR(100) --参数格式为 '1,2'
AS
    BEGIN
        SELECT  int_Mappingid ,
                vchar_SFInum ,
                vchar_AGnum ,
                int_AGid ,
                char_AG_Syn_sign
        FROM    dbo.tb_SFI_TrackNum
        WHERE   ( int_AG_Syn = @intProcessCode )
                AND ( char_IsStop = 'N' )
                AND ( ISNULL(char_AG_Syn_sign, '0') IN ( select col FROM dbo.F_ParasAfterIn(@mainSynSign,',') ) ); 
    END;


