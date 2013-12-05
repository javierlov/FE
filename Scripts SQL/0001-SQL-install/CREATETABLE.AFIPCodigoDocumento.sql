SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AFIPCodigoDocumento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NULL,
	[CodigoEmpresa] [nvarchar](50) NULL,
	[CodigoAFIP] [nvarchar](50) NULL
) ON [PRIMARY]
