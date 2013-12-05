/*srv 10.0.0.54*/
/****** Object:  Table [dbo].[EquivAFIPImpuesto]    Script Date: 12/02/2013 11:33:17 ******/
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


