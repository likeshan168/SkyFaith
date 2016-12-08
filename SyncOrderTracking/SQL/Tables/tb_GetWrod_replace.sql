USE [db_SFI];
GO

/****** Object:  Table [dbo].[tb_GetWrod_replace]    Script Date: 2016-12-08 13:20:36 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[tb_GetWrod_replace]
    (
      [int_id] [INT] IDENTITY(1, 1)
                     NOT NULL ,
      [int_agID] [INT] NOT NULL ,
      [s_Old] [NVARCHAR](200) NOT NULL ,
      [s_New] [NVARCHAR](200) NOT NULL
    )
ON  [PRIMARY];

GO


