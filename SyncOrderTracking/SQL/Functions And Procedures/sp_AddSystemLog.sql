
CREATE PROCEDURE [sp_AddSystemLog]
    @mainAGid INT ,
    @result NVARCHAR(200)
AS
    BEGIN
        INSERT  INTO tb_SystemLog
                ( dttm, AGid, result )
        VALUES  ( GETDATE(), @mainAGid, @result );
    END;
