USE [db_SFI];
GO

/****** Object:  StoredProcedure [dbo].[sp_Logupdate]    Script Date: 2016-12-07 13:39:20 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO


CREATE PROCEDURE [dbo].[sp_Logupdate]
    @type CHAR(1) = NULL ,
    @MappingID INT = NULL ,
    @result VARCHAR(50) = NULL ,
    @error NVARCHAR(1000) = NULL ,
    @message VARCHAR(200) = NULL OUTPUT
AS
    BEGIN
        DECLARE @SFInumber VARCHAR(32);
        DECLARE @AGnumber VARCHAR(32);
        DECLARE @AGid INT;
        SELECT  @SFInumber = vchar_SFInum ,
                @AGnumber = vchar_AGnum ,
                @AGid = int_AGid
        FROM    tb_SFI_TrackNum
        WHERE   ( int_Mappingid = @MappingID );
        INSERT  INTO tb_ServerLog
                ( char_type ,
                  log_time ,
                  SFI_Number ,
                  AG_Number ,
                  AG_id ,
                  vchar_result ,
                  nvchar_Error
                )
        VALUES  ( @type ,
                  GETDATE() ,
                  @SFInumber ,
                  @AGnumber ,
                  @AGid ,
                  @result ,
                  @error
                );
        IF @type = 'G'
            BEGIN
                UPDATE  tb_SFI_TrackNum
                SET     dttm_AG_LastSyn = GETDATE()
                WHERE   ( int_Mappingid = @MappingID );
            END;
    END;
    BEGIN
        IF @@ERROR <> 0
            BEGIN
                ROLLBACK TRAN;
                SET @message = 'F';
                RETURN;
            END;
        ELSE
            BEGIN
                SET @message = 'N';
                RETURN;
            END;
    END;

GO


