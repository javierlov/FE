/*base local desarrollo*/
/****** Object:  Table [dbo].[CbteImpuesto]    Script Date: 12/02/2013 14:45:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CbteImpuesto](
	[ImpuestoID] [int] IDENTITY(1,1) NOT NULL,
	[CbteID] [int] NULL,
	[Id] [int] NULL,
	[Tipo] [nvarchar](15) NULL,
	[BaseImp] [float] NULL,
	[Importe] [float] NULL,
	[ImporteMonedaFacturacion] [float] NULL,
	[Codigo] [nvarchar](50) NULL,
	[Descripcion] [nvarchar](150) NULL
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Alicuota, Tributo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CbteImpuesto', @level2type=N'COLUMN',@level2name=N'Tipo'