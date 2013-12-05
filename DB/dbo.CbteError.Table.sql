/****** Object:  Table [dbo].[CbteError]    Script Date: 11/27/2013 17:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CbteError](
	[ErrorID] [int] IDENTITY(1,1) NOT NULL,
	[CbteID] [int] NULL,
	[LineaID] [int] NULL,
	[ErrorSeccion] [nvarchar](50) NULL,
	[ErrorCodigo] [nvarchar](50) NULL,
	[ErrorDescripcion] [nvarchar](1000) NULL,
	[Fecha] [datetime] NULL
) ON [PRIMARY]
GO
