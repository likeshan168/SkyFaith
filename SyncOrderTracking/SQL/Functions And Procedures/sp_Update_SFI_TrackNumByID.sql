CREATE PROC sp_Update_SFI_TrackNumByID @int_Mappingid INT
AS
    BEGIN
        UPDATE  tb_SFI_TrackNum
        SET     int_AG_Syn = 0
        WHERE   ( int_Mappingid = @int_Mappingid );
    END;