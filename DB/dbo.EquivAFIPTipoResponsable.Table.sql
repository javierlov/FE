/****** Object:  Table [dbo].[EquivAFIPTipoResponsable]    Script Date: 11/27/2013 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquivAFIPTipoResponsable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NULL,
	[CodigoEmpresa] [nvarchar](50) NULL,
	[CodigoAFIP] [nvarchar](50) NULL
) ON [PRIMARY]
GO
