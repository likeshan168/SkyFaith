USE [db_SFI];
GO

/****** Object:  Table [dbo].[tb_Agent]    Script Date: 2016-12-08 13:19:31 ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

SET ANSI_PADDING ON;
GO

CREATE TABLE [dbo].[tb_Agent]
    (
      [int_AGid] [INT] IDENTITY(1, 1)
                       NOT NULL ,
      [vchar_AGcode] [VARCHAR](50) NOT NULL ,
      [vchar_AGname] [NVARCHAR](200) NOT NULL ,
      [vchar_AGLinkMan] [NVARCHAR](50) NULL ,
      [vchar_AGcontect] [VARCHAR](50) NULL ,
      [int_AGtype] [SMALLINT] NOT NULL ,
      [bit_synOpen] [BIT] NOT NULL ,
      [vchar_synVerify] [VARCHAR](50) NULL ,
      [vchar_synStopKeyWord] [NVARCHAR](50) NULL ,
      [int_synSpacing] [INT] NULL ,
      [bit_synQuery] [BIT] NOT NULL ,
      [vchar_QueryVerify] [VARCHAR](50) NULL ,
      [bit_synPush] [BIT] NOT NULL ,
      [vchar_PushUser] [VARCHAR](50) NULL ,
      [vchar_PushVerify] [VARCHAR](50) NULL ,
      [IsOpen] [BIT] NOT NULL
    )
ON  [PRIMARY];

GO

SET ANSI_PADDING OFF;
GO

ALTER TABLE [dbo].[tb_Agent] ADD  CONSTRAINT [DF_tb_Agent_bit_synOpen]  DEFAULT ((0)) FOR [bit_synOpen];
GO

ALTER TABLE [dbo].[tb_Agent] ADD  CONSTRAINT [DF_tb_Agent_int_synSpacing]  DEFAULT ((30)) FOR [int_synSpacing];
GO

ALTER TABLE [dbo].[tb_Agent] ADD  CONSTRAINT [DF_tb_Agent_bit_synOpen1]  DEFAULT ((0)) FOR [bit_synQuery];
GO

ALTER TABLE [dbo].[tb_Agent] ADD  CONSTRAINT [DF_tb_Agent_bit_synQuery1]  DEFAULT ((0)) FOR [bit_synPush];
GO

ALTER TABLE [dbo].[tb_Agent] ADD  CONSTRAINT [DF_tb_Agent_IsOpen]  DEFAULT ((1)) FOR [IsOpen];
GO


