/****** Object:  Table [dbo].[CbteCabecera]    Script Date: 11/27/2013 17:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CbteCabecera](
	[CbteID] [int] IDENTITY(1,1) NOT NULL,
	[EmpresaID] [int] NULL,
	[BatchUniqueId] [nvarchar](50) NULL,
	[UniqueIdentifier] [nvarchar](50) NULL,
	[NroInternoERP] [nvarchar](50) NULL,
	[EstadoTransaccion] [nvarchar](50) NULL,
	[TipoTransaccion] [nvarchar](50) NULL,
	[TipoComprobante] [nvarchar](50) NULL,
	[LetraComprobante] [nvarchar](2) NULL,
	[FechaComprobante] [smalldatetime] NULL,
	[CodigoLetra] [nvarchar](50) NULL,
	[PuntoVenta] [nvarchar](50) NULL,
	[OficinaVentas] [nvarchar](50) NULL,
	[NroComprobanteDesde] [nvarchar](150) NULL,
	[NroComprobanteHasta] [nvarchar](150) NULL,
	[EmisorRazonSocial] [nvarchar](200) NULL,
	[EmisorDireccion] [nvarchar](200) NULL,
	[EmisorCalle] [nvarchar](200) NULL,
	[EmisorCP] [nvarchar](100) NULL,
	[EmisorLocalidad] [nvarchar](200) NULL,
	[EmisorProvincia] [nvarchar](200) NULL,
	[EmisorPais] [nvarchar](200) NULL,
	[EmisorTelefonos] [nvarchar](200) NULL,
	[EmisorEMail] [nvarchar](200) NULL,
	[CompradorCodigoDocumento] [nvarchar](50) NULL,
	[CompradorNroDocumento] [nvarchar](150) NULL,
	[CompradorTipoResponsable] [nvarchar](50) NULL,
	[CompradorTipoResponsableDescripcion] [nvarchar](150) NULL,
	[CompradorRazonSocial] [nvarchar](150) NULL,
	[CompradorDireccion] [nvarchar](150) NULL,
	[CompradorLocalidad] [nvarchar](150) NULL,
	[CompradorProvincia] [nvarchar](150) NULL,
	[CompradorPais] [nvarchar](150) NULL,
	[CompradorCodigoPostal] [nvarchar](150) NULL,
	[CompradorCodigoCliente] [nvarchar](150) NULL,
	[CompradorNroReferencia] [nvarchar](100) NULL,
	[CompradorNroIIBB] [nvarchar](150) NULL,
	[CompradorEmail] [nvarchar](100) NULL,
	[FechaDesdeServicioFacturado] [smalldatetime] NULL,
	[FechaHastaServicioFacturado] [smalldatetime] NULL,
	[CondicionPago] [nvarchar](150) NULL,
	[FormaPagoDescripcion] [nvarchar](150) NULL,
	[FechaVencimientoPago] [smalldatetime] NULL,
	[NroRemito] [nvarchar](150) NULL,
	[Importe] [float] NULL,
	[ImporteComprobanteB] [float] NULL,
	[ImporteNoGravado] [float] NULL,
	[ImporteGravado] [float] NULL,
	[AlicuotaIVA] [float] NULL,
	[ImporteImpuestoLiquidado] [float] NULL,
	[ImporteRNI_Percepcion] [float] NULL,
	[ImporteExento] [float] NULL,
	[ImportePercepciones_PagosCuentaImpuestosNacionales] [float] NULL,
	[ImportePercepcionIIBB] [float] NULL,
	[TasaIIBB] [nvarchar](150) NULL,
	[CodigoJurisdiccionIIBB] [nvarchar](150) NULL,
	[JurisdiccionImpuestosMunicipales] [nvarchar](150) NULL,
	[ImportePercepcionImpuestosMunicipales] [float] NULL,
	[ImporteImpuestosInternos] [float] NULL,
	[ImporteMonedaFacturacion] [float] NULL,
	[ImporteMonedaFacturacionComprobanteB] [float] NULL,
	[ImporteNoGravadoMonedaFacturacion] [float] NULL,
	[ImporteGravadoMonedaFacturacion] [float] NULL,
	[ImporteImpuestoLiquidadoMonedaFacturacion] [float] NULL,
	[ImporteRNI_PercepcionMonedaFacturacion] [float] NULL,
	[ImporteExentoMonedaFacturacion] [float] NULL,
	[ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion] [float] NULL,
	[ImportePercepcionIIBBMonedaFacturacion] [float] NULL,
	[ImportePercepcionImpuestosMunicipalesMonedaFacturacion] [float] NULL,
	[ImporteImpuestosInternosMonedaFacturacion] [float] NULL,
	[CantidadAlicuotasIVA] [float] NULL,
	[CodigoOperacion] [nvarchar](50) NULL,
	[ImporteEscrito] [nvarchar](250) NULL,
	[TasaCambio] [float] NULL,
	[CodigoMoneda] [nvarchar](50) NULL,
	[CantidadRegistrosDetalle] [int] NULL,
	[CodigoMecanismoDistribucion] [nvarchar](50) NULL,
	[TipoExportacion] [nvarchar](50) NULL,
	[PermisoExistente] [nvarchar](50) NULL,
	[PaisDestino] [nvarchar](150) NULL,
	[IncoTerms] [nvarchar](150) NULL,
	[Idioma] [nvarchar](150) NULL,
	[Observaciones1] [nvarchar](1000) NULL,
	[Observaciones2] [nvarchar](1000) NULL,
	[Observaciones3] [nvarchar](1000) NULL,
	[CAE] [nvarchar](150) NULL,
	[FechaVencimiento] [smalldatetime] NULL,
	[NombreObjetoSalida] [nvarchar](250) NULL,
	[RapiPago] [nvarchar](250) NULL,
	[ObservacionRapipago] [nvarchar](1000) NULL,
	[PagoFacil] [nvarchar](250) NULL,
	[OPER] [nvarchar](200) NULL,
	[NOPER] [nvarchar](200) NULL,
	[DAGRUF] [nvarchar](100) NULL,
	[FACTORI] [nvarchar](100) NULL,
	[FACTORI_FORMATEADO] [nvarchar](200) NULL,
	[USUARIO] [nvarchar](50) NULL,
	[FECPG1] [nvarchar](200) NULL,
	[FECPG1_FORMATEADO] [nvarchar](200) NULL,
	[FECPG2] [nvarchar](200) NULL,
	[FECPG2_FORMATEADO] [nvarchar](200) NULL,
	[CUOTAIVA21] [float] NULL,
	[CUOTAIVA105] [float] NULL
) ON [PRIMARY]
GO
