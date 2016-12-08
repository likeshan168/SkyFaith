USE [db_SFI];
GO

/****** Object:  Table [dbo].[tb_SystemLog]    Script Date: 2016-12-08 13:22:42 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[tb_SystemLog]
    (
      [int_id] [INT] IDENTITY(1, 1)
                     NOT NULL ,
      [dttm] [DATETIME] NOT NULL ,
      [AGid] [INT] NULL ,
      [result] [NVARCHAR](MAX) NOT NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY];

GO


