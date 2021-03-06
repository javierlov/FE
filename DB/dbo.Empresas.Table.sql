/****** Object:  Table [dbo].[Empresas]    Script Date: 11/27/2013 17:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresas](
	[EmpresaID] [int] NOT NULL,
	[TipoDocumento] [nvarchar](50) NULL,
	[NroDocumento] [nvarchar](50) NULL,
	[RazonSocial] [nvarchar](100) NULL,
	[InicioActividades] [smalldatetime] NULL,
	[Direccion] [nvarchar](50) NULL,
	[Localidad] [nvarchar](50) NULL,
	[Provincia] [nvarchar](50) NULL,
	[Pais] [nvarchar](50) NULL,
	[CodigoPostal] [nvarchar](50) NULL,
	[Telefono] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Contacto] [nvarchar](50) NULL,
	[Logo] [image] NULL,
	[FirmaElectronica] [image] NULL,
	[FirmanteNombre] [nvarchar](50) NULL,
	[FirmanteTipoDoc] [nvarchar](50) NULL,
	[FirmanteNroDoc] [nvarchar](50) NULL,
	[AutorizacionAFIP] [smalldatetime] NULL,
	[CodigoTipoResponsableAnteAFIP] [nvarchar](50) NULL,
	[DescTipoResponsableImpresion] [nvarchar](100) NULL,
	[TipoInscripcionIIBB] [nvarchar](50) NULL,
	[CodigoProvinciaIIBB] [nvarchar](50) NULL,
	[NroIIBB] [nvarchar](50) NULL,
	[TextoFactura] [nvarchar](50) NULL,
	[TextoNotaDebito] [nvarchar](50) NULL,
	[TextoNotaCredito] [nvarchar](50) NULL,
	[TexstoPieComprobante] [nvarchar](50) NULL,
	[PeriodoDesdeAutorizAFIP] [nvarchar](50) NULL,
	[TiempoMaximoEsperaAFIP] [nvarchar](50) NULL,
	[EnviaMailenError] [nvarchar](50) NULL,
	[Activo] [bit] NULL,
	[AgRecaudacionIIBB] [nvarchar](100) NULL,
	[ImpuestosInternos] [nvarchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
