USE [db_SFI];
GO

/****** Object:  StoredProcedure [dbo].[sp_RecordUpdate]    Script Date: 2016-12-07 13:39:51 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO


CREATE PROCEDURE [dbo].[sp_RecordUpdate]
    @MappingId INT = NULL ,
    @AGid INT = NULL ,
    @SFInumber VARCHAR(32) = NULL ,
    @AGnumber VARCHAR(32) = NULL ,
    @dttm DATETIME = NULL ,
    @Description NVARCHAR(MAX) = NULL ,
    @DescriptionLocal NVARCHAR(MAX) = NULL ,
    @city NVARCHAR(100) = NULL ,
    @code VARCHAR(32) = NULL ,
    @message VARCHAR(200) = NULL OUTPUT
AS
    BEGIN
        DECLARE @RES CHAR(1);
        DECLARE @stop NVARCHAR(50);
        SELECT  @stop = CASE WHEN vchar_synStopKeyWord IS NULL THEN N'[银河火星]'
                             WHEN vchar_synStopKeyWord = '' THEN N'[银河火星]'
                             ELSE vchar_synStopKeyWord
                        END
        FROM    [dbo].[tb_Agent]
        WHERE   [int_AGid] = @AGid;
        IF NOT ( EXISTS ( SELECT    vchar_synCode
                          FROM      tb_track_record
                          WHERE     vchar_synCode = @code ) )
            BEGIN
                INSERT  INTO tb_track_record
                        ( int_MappingId ,
                          vchar_SFInum ,
                          vchar_AGnum ,
                          int_AGid ,
                          dttm_Record_Dttm ,
                          nvchar_Description ,
                          nvchar_Description_Local ,
                          nvchar_City ,
                          vchar_synCode
                        )
                VALUES  ( @MappingId ,
                          @SFInumber ,
                          @AGnumber ,
                          @AGid ,
                          @dttm ,
                          [dbo].[DescriptionReplace](@AGid, @Description) ,
                          [dbo].[DescriptionReplace](@AGid, @DescriptionLocal) ,
                          @city ,
                          @code
                        );
                SET @RES = 'N';
            END;
        ELSE
            BEGIN
                SET @RES = 'E';
            END;
    END;
    BEGIN
	
        IF EXISTS ( SELECT  A.[Value]
                    FROM    ( SELECT    @DescriptionLocal AS [Value]
                            ) AS A
                            INNER JOIN [dbo].[SplitString](@stop, '|', 1) AS B ON A.[Value] LIKE '%'
                                                              + B.[Value]
                                                              + '%' )
            BEGIN
                UPDATE  [dbo].[tb_SFI_TrackNum]
                SET     [char_IsStop] = 'Y'
                WHERE   [int_Mappingid] = @MappingId;
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
                SET @message = @RES;
                RETURN;
            END;
    END;

GO


