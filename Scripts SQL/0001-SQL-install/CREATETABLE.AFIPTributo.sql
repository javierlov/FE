SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFIPTributo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodigoAFIP] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](200) NULL
) ON [PRIMARY]
