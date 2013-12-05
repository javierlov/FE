/****** Object:  Table [dbo].[OficinaVenta]    Script Date: 11/27/2013 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OficinaVenta](
	[OficinaVentaID] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NULL,
	[Nombre] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](200) NULL
) ON [PRIMARY]
GO
