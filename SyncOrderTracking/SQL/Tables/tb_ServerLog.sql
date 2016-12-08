USE [db_SFI];
GO

/****** Object:  Table [dbo].[tb_ServerLog]    Script Date: 2016-12-08 13:21:18 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

SET ANSI_PADDING ON;
GO

CREATE TABLE [dbo].[tb_ServerLog]
    (
      [log_id] [INT] IDENTITY(1, 1)
                     NOT NULL ,
      [char_type] [CHAR](1) NOT NULL ,
      [log_time] [DATETIME] NOT NULL ,
      [SFI_Number] [VARCHAR](32) NULL ,
      [AG_Number] [VARCHAR](32) NULL ,
      [AG_id] [INT] NULL ,
      [vchar_result] [VARCHAR](50) NOT NULL ,
      [nvchar_Error] [NVARCHAR](1000) NULL
    )
ON  [PRIMARY];

GO

SET ANSI_PADDING OFF;
GO


