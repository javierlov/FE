USE [HFacturaElectronica]
GO
/****** Object:  Table [dbo].[EquivAFIPImpuesto]    Script Date: 11/21/2013 11:32:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquivAFIPImpuesto](
	[EmpresaID] [int] NULL,
	[CodigoEmpresa] [nchar](10) NULL,
	[CodigoAFIP] [nchar](10) NULL,
	[Porcentaje] [nchar](10) NULL,
	[Descripcion] [nvarchar](200) NULL
) ON [PRIMARY]
GO
