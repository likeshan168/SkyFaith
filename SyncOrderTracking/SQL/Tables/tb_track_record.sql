USE [db_SFI];
GO

/****** Object:  Table [dbo].[tb_track_record]    Script Date: 2016-12-08 13:23:14 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

SET ANSI_PADDING ON;
GO

CREATE TABLE [dbo].[tb_track_record]
    (
      [int_id] [INT] IDENTITY(1, 1)
                     NOT NULL ,
      [int_MappingId] [INT] NOT NULL ,
      [vchar_SFInum] [VARCHAR](32) NOT NULL ,
      [vchar_AGnum] [VARCHAR](32) NOT NULL ,
      [int_AGid] [INT] NOT NULL ,
      [dttm_Record_Dttm] [DATETIME] NOT NULL ,
      [nvchar_Description] [NVARCHAR](MAX) NULL ,
      [nvchar_Description_Local] [NVARCHAR](MAX) NULL ,
      [nvchar_City] [NVARCHAR](100) NULL ,
      [vchar_synCode] [VARCHAR](32) NOT NULL
    )
ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY];

GO

SET ANSI_PADDING OFF;
GO


