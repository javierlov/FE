/*srv local desarrollo*/
/****** Object:  Table [dbo].[AFIPCodigoDocumento]    Script Date: 12/02/2013 11:20:52 ******/
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
