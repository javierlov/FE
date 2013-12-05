USE [HFacturaElectronica]
GO
/****** Object:  Table [dbo].[AFIPCodigoDocumento]    Script Date: 11/27/2013 17:08:31 ******/
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
GO
