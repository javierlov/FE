/*srv local desarrollo*/
/****** Object:  Table [dbo].[EquivAFIPCodigoNCM]    Script Date: 12/02/2013 11:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquivAFIPCodigoNCM](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NULL,
	[CodigoEmpresa] [nvarchar](50) NULL,
	[CodigoAFIP] [nvarchar](50) NULL
) ON [PRIMARY]
