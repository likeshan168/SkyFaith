USE [db_SFI];
GO

/****** Object:  Table [dbo].[tb_SFI_TrackNum]    Script Date: 2016-12-08 13:21:54 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

SET ANSI_PADDING ON;
GO

CREATE TABLE [dbo].[tb_SFI_TrackNum]
    (
      [int_Mappingid] [INT] IDENTITY(1, 1)
                            NOT NULL ,
      [vchar_SFInum] [VARCHAR](32) NOT NULL ,
      [vchar_AGnum] [VARCHAR](32) NOT NULL ,
      [int_AGid] [INT] NOT NULL ,
      [vchar_updateUser] [NVARCHAR](50) NULL ,
      [dttm_updateDttm] [DATETIME] NULL ,
      [dttm_AG_LastSyn] [DATETIME] NULL ,
      [int_AG_Syn] [SMALLINT] NOT NULL ,
      [char_IsStop] [CHAR](1) NOT NULL ,
      [char_AG_Syn_sign] [CHAR](1) NULL
    )
ON  [PRIMARY];

GO

SET ANSI_PADDING OFF;
GO

ALTER TABLE [dbo].[tb_SFI_TrackNum] ADD  CONSTRAINT [DF_tb_SFI_TrackNum_int_AG_Syn1]  DEFAULT ((0)) FOR [int_AG_Syn];
GO

ALTER TABLE [dbo].[tb_SFI_TrackNum] ADD  CONSTRAINT [DF_tb_SFI_TrackNum_char_IsStop]  DEFAULT ('N') FOR [char_IsStop];
GO


