USE [db_SFI];
GO

/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 2016-12-08 13:16:49 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO
CREATE FUNCTION [dbo].[SplitString]
    (
      @Input NVARCHAR(MAX) , --input string to be separated 
      @Separator NVARCHAR(MAX) = ',' , --a string that delimit the substrings in the input string 
      @RemoveEmptyEntries BIT = 1 --the return value does not include array elements that contain an empty string 
    )
RETURNS @TABLE TABLE
    (
      [Id] INT IDENTITY(1, 1) ,
      [Value] NVARCHAR(MAX)
    )
AS
    BEGIN 
        DECLARE @Index INT ,
            @Entry NVARCHAR(MAX); 
        SET @Index = CHARINDEX(@Separator, @Input); 
        WHILE ( @Index > 0 )
            BEGIN 
                SET @Entry = LTRIM(RTRIM(SUBSTRING(@Input, 1, @Index - 1))); 
                IF ( @RemoveEmptyEntries = 0 )
                    OR ( @RemoveEmptyEntries = 1
                         AND @Entry <> ''
                       )
                    BEGIN 
                        INSERT  INTO @TABLE
                                ( [Value] )
                        VALUES  ( @Entry ); 
                    END; 
                SET @Input = SUBSTRING(@Input,
                                       @Index + DATALENGTH(@Separator) / 2,
                                       LEN(@Input)); 
                SET @Index = CHARINDEX(@Separator, @Input); 
            END; 
        SET @Entry = LTRIM(RTRIM(@Input)); 
        IF ( @RemoveEmptyEntries = 0 )
            OR ( @RemoveEmptyEntries = 1
                 AND @Entry <> ''
               )
            BEGIN 
                INSERT  INTO @TABLE
                        ( [Value] )
                VALUES  ( @Entry ); 
            END; 
        RETURN; 
    END; 
GO


