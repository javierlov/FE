using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using FacturaElectronica.Common;
using System.Web;

namespace FacturaElectronica.DBEngine
{
    public partial class SQLEngine
    {        
        SqlConnection dbConnection = new SqlConnection();

        public struct TablaAfipImpuesto
        {
            public string EmpresaID;
            public string CodigoEmpresa;
            public string CodigoAFIP;
            public string Porcentaje;
            public string Descripcion;
        }
                
        //public SQLEngine(string ConnectionString)
        //{
        //    dbConnestionString = ConnectionString;
        //}

        public bool Open()
        {            
            string dbConnestionString = FacturaElectronica.DBEngine.SQLEngine.GetConnestionStringXMLSetting();                               
            bool bResult = false;

            try
            {
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.ConnectionString = dbConnestionString;
                    dbConnection.Open();
                }
                bResult = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return bResult;
        }

   
        public void Close()
        {
            if (dbConnection.State == ConnectionState.Open)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
        }

        #region Settings

        public Hashtable GetGeneralSettings(string EmpresaID)
        {
            Hashtable parametros = new  Hashtable();
            SqlCommand dbQuery = new SqlCommand();
            SqlDataReader dbReader = null;

            try
            {                  
                
                if (Open())
                {
                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = "select * from ConfiguracionGeneral where EmpresaID = '" + EmpresaID + "'";
                    dbReader = dbQuery.ExecuteReader();

                    if (dbReader.Read())
                    {
                        parametros.Add("EmpresaID", dbReader["EmpresaID"].ToString());
                        parametros.Add("ActivarDebug", dbReader["ActivarDebug"].ToString());
                        parametros.Add("TipoEntrada", dbReader["TipoEntrada"].ToString());
                        parametros.Add("Entrada", dbReader["Entrada"].ToString());
                        parametros.Add("EntradaExtra", dbReader["EntradaExtra"].ToString());
                        parametros.Add("Intervalo", Convert.ToInt16(dbReader["Intervalo"]));
                        parametros.Add("TipoSalida", dbReader["TipoSalida"].ToString());
                        parametros.Add("Salida", dbReader["Salida"].ToString());
                        parametros.Add("PathHistorico", dbReader["PathHistorico"].ToString());
                        parametros.Add("PathDebug", dbReader["PathDebug"].ToString());
                        parametros.Add("PathCertificate", dbReader["PathCertificate"].ToString());
                        parametros.Add("PathConnectionFiles", dbReader["PathConnectionFiles"].ToString());
                        parametros.Add("PathImpresion", dbReader["PathImpresion"].ToString());
                        parametros.Add("PathTemporales", dbReader["PathTemporales"].ToString());
                        parametros.Add("UrlAFIPwsaa", dbReader["UrlAFIPwsaa"].ToString());
                        parametros.Add("UrlAFIPwsfe", dbReader["UrlAFIPwsfe"].ToString());
                        parametros.Add("UrlAFIPwsfex", dbReader["UrlAFIPwsfex"].ToString());
                        parametros.Add("UrlAFIPwsbfe", dbReader["UrlAFIPwsbfe"].ToString());
                        parametros.Add("UrlPrintWebService", dbReader["UrlPrintWebService"].ToString());
                        parametros.Add("UrlFEWebService", dbReader["UrlFEWebService"].ToString());
                        parametros.Add("SMTPServer", dbReader["SMTPServer"].ToString());
                        parametros.Add("SMTPUser", dbReader["SMTPUser"].ToString());
                        parametros.Add("SMTPPassword", dbReader["SMTPPassword"].ToString());
                        parametros.Add("SMTPFrom", dbReader["SMTPFrom"].ToString());
                        parametros.Add("MailSubject", dbReader["MailSubject"].ToString());
                        parametros.Add("MailMessage", dbReader["MailMessage"].ToString());
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
                //this.LogError("0", "0", "SQLEngine.GetGeneralSettings", "Error:" + ex.Message);
            }
            finally
            {
                if (dbReader != null)
                    dbReader.Close();
            }
            return parametros;
        }

        public Hashtable GetEmpresa(string EmpresaID)
        {
            Hashtable parametros = new Hashtable();
            SqlCommand dbQuery = new SqlCommand();
            SqlDataReader dbReader = null;

            try
            {                
                if (Open())
                {
                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = "select * from Empresas where EmpresaID = " + EmpresaID;
                    dbReader = dbQuery.ExecuteReader();

                    if (dbReader.Read())
                    {
                        parametros.Add("EmpresaID", dbReader["EmpresaID"].ToString());
                        parametros.Add("TipoDocumento", dbReader["TipoDocumento"].ToString());
                        parametros.Add("NroDocumento", dbReader["NroDocumento"].ToString());
                        parametros.Add("RazonSocial", dbReader["RazonSocial"].ToString());
                        parametros.Add("InicioActividades", dbReader["InicioActividades"].ToString());
                        parametros.Add("Direccion", dbReader["Direccion"].ToString());
                        parametros.Add("Localidad", dbReader["Localidad"].ToString());
                        parametros.Add("Provincia", dbReader["Provincia"].ToString());
                        parametros.Add("Pais", dbReader["Pais"].ToString());
                        parametros.Add("CodigoPostal", dbReader["CodigoPostal"].ToString());
                        parametros.Add("Telefono", dbReader["Telefono"].ToString());
                        parametros.Add("Fax", dbReader["Fax"].ToString());
                        parametros.Add("Email", dbReader["Email"].ToString());
                        parametros.Add("Contacto", dbReader["Contacto"].ToString());

                        parametros.Add("CodigoTipoResponsableAnteAFIP", dbReader["CodigoTipoResponsableAnteAFIP"].ToString());
                        parametros.Add("NroIIBB", dbReader["NroIIBB"].ToString());
                        parametros.Add("AgRecaudacionIIBB", dbReader["AgRecaudacionIIBB"].ToString());
                        parametros.Add("ImpuestosInternos", dbReader["ImpuestosInternos"].ToString());
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                this.LogError("0", "0", "SQLEngine.GetEmpresa", "Error:" + ex.Message);
            }
            finally
            {
                dbReader.Close();
            }
            return parametros;
        }

        public Hashtable GetEmpresas()
        {
            Hashtable parametros = new Hashtable();
            SqlCommand dbQuery = new SqlCommand();
            SqlDataReader dbReader = null;

            try
            {
                if (Open())
                {
                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = "select * from Empresas";
                    dbReader = dbQuery.ExecuteReader();

                    if (dbReader.Read())
                    {
                        parametros.Add("EmpresaID", dbReader["EmpresaID"].ToString());
                        parametros.Add("TipoDocumento", dbReader["TipoDocumento"].ToString());
                        parametros.Add("NroDocumento", dbReader["NroDocumento"].ToString());
                        parametros.Add("RazonSocial", dbReader["RazonSocial"].ToString());
                        parametros.Add("InicioActividades", dbReader["InicioActividades"].ToString());
                        parametros.Add("Direccion", dbReader["Direccion"].ToString());
                        parametros.Add("Localidad", dbReader["Localidad"].ToString());
                        parametros.Add("Provincia", dbReader["Provincia"].ToString());
                        parametros.Add("Pais", dbReader["Pais"].ToString());
                        parametros.Add("CodigoPostal", dbReader["CodigoPostal"].ToString());
                        parametros.Add("Telefono", dbReader["Telefono"].ToString());
                        parametros.Add("Fax", dbReader["Fax"].ToString());
                        parametros.Add("Email", dbReader["Email"].ToString());
                        parametros.Add("Contacto", dbReader["Contacto"].ToString());

                        parametros.Add("CodigoTipoResponsableAnteAFIP", dbReader["CodigoTipoResponsableAnteAFIP"].ToString());
                        parametros.Add("NroIIBB", dbReader["NroIIBB"].ToString());
                        parametros.Add("AgRecaudacionIIBB", dbReader["AgRecaudacionIIBB"].ToString());
                        parametros.Add("ImpuestosInternos", dbReader["ImpuestosInternos"].ToString());
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
                //this.LogError("0", "0", "SQLEngine.GetEmpresa", "Error:" + ex.Message);
            }
            finally
            {
                dbReader.Close();
            }
            return parametros;
        }

        #endregion

        #region Logueo

        public void LogError(string cabeceraIdentity, string lineaIdentity, string seccion, string mensaje)
        {
            string cmd = "";

            SqlCommand dbQuery = new SqlCommand();

            try
            {                
                if (Open())
                {
                    if (cabeceraIdentity == null)
                        cabeceraIdentity = "0";

                    if (lineaIdentity == null)
                        lineaIdentity = "0";

                    if (seccion == null)
                        seccion = "N/A";

                    if (mensaje == null)
                        mensaje = "N/A";

                    cmd = "insert into CbteError (Fecha, CbteID, LineaID, ErrorSeccion, ErrorDescripcion) values (";
                    cmd += "GetDate(), @cabeceraIdentity, @lineaIdentity, @seccion, @mensaje)";

                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = cmd;
                    dbQuery.Parameters.Add(new SqlParameter("@cabeceraIdentity", System.Data.SqlDbType.Int));
                    dbQuery.Parameters.Add(new SqlParameter("@lineaIdentity", System.Data.SqlDbType.Int));
                    dbQuery.Parameters.Add(new SqlParameter("@seccion", System.Data.SqlDbType.VarChar));
                    dbQuery.Parameters.Add(new SqlParameter("@mensaje", System.Data.SqlDbType.VarChar));
                    dbQuery.Parameters[0].Value = Convert.ToInt32(cabeceraIdentity);
                    dbQuery.Parameters[1].Value = Convert.ToInt32(lineaIdentity);
                    dbQuery.Parameters[2].Value = seccion;
                    dbQuery.Parameters[3].Value = mensaje;
                    dbQuery.ExecuteNonQuery();

                    Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Servicio Factura Electronica.", "SQLEngine.LogError: " + ex.Source + ", " + ex.Message);
            }
        }

        public bool LogBatchStart(ref RequestBatch thisBatch)
        {
            SqlCommand dbQuery = null;

            string cmd = string.Empty;

            bool bResult = false;

            try
            {                
                if (Open())
                {
                    foreach (RequestHeader thisHeader in thisBatch.RequestHeaders)
                    {
                        cmd = "insert into CbteCabecera (UniqueIdentifier, EmpresaID, EstadoTransaccion, TipoTransaccion, FechaComprobante, TipoComprobante, PuntoVenta, ";
                        cmd += "NroComprobanteDesde, NroComprobanteHasta, CompradorCodigoDocumento, CompradorNroDocumento, ";
                        cmd += "CompradorTipoResponsable, CompradorTipoResponsableDescripcion, CompradorRazonSocial, ";
                        cmd += "FechaDesdeServicioFacturado, FechaHastaServicioFacturado, FechaVencimientoPago, CondicionPago, ";
                        cmd += "CompradorDireccion, CompradorLocalidad, CompradorProvincia, CompradorPais, CompradorCodigoPostal, ";
                        cmd += "CompradorNroIIBB, CompradorCodigoCliente, CompradorNroReferencia, CompradorEmail, ";
                        cmd += "NroRemito, Importe, ImporteComprobanteB, ImporteNoGravado, ImporteGravado, AlicuotaIVA, ImporteImpuestoLiquidado, ";
                        cmd += "ImporteRNI_Percepcion, ImporteExento, ImportePercepciones_PagosCuentaImpuestosNacionales, ";
                        cmd += "ImportePercepcionIIBB, TasaIIBB, CodigoJurisdiccionIIBB, ImportePercepcionImpuestosMunicipales, ";
                        cmd += "JurisdiccionImpuestosMunicipales, ImporteImpuestosInternos, ImporteMonedaFacturacion, ImporteMonedaFacturacionComprobanteB, ImporteNoGravadoMonedaFacturacion, ";
                        cmd += "ImporteGravadoMonedaFacturacion, ImporteImpuestoLiquidadoMonedaFacturacion, ImporteRNI_PercepcionMonedaFacturacion, ";
                        cmd += "ImporteExentoMonedaFacturacion, ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion, ";
                        cmd += "ImportePercepcionIIBBMonedaFacturacion, ImportePercepcionImpuestosMunicipalesMonedaFacturacion, ";
                        cmd += "ImporteImpuestosInternosMonedaFacturacion, CantidadAlicuotasIVA, CodigoOperacion, TasaCambio, ";
                        cmd += "CodigoMoneda, ImporteEscrito, CantidadRegistrosDetalle, CodigoMecanismoDistribucion, TipoExportacion, ";
                        cmd += "PermisoExistente, FormaPagoDescripcion, IncoTerms, Idioma, Observaciones1, ";
                        cmd += "Observaciones2, Observaciones3, LetraComprobante, NroInternoERP, ";
                        cmd += "EmisorRazonSocial, EmisorDireccion, EmisorCalle, EmisorCP, EmisorLocalidad, ";
                        cmd += "EmisorProvincia, EmisorPais, EmisorTelefonos, EmisorEMail, OficinaVentas, RapiPago, ObservacionRapiPago, PagoFacil, ";
                        cmd += "OPER, NOPER, DAGRUF, FACTORI, FACTORI_FORMATEADO,USUARIO, FECPG1_FORMATEADO, FECPG2_FORMATEADO, CUOTAIVA105, CUOTAIVA21)";

                        cmd += "values (@NroInternoERP, @EmpresaID, @Iniciada, @TipoTransaccion, @FechaComprobante, @TipoComprobante, @PuntoVenta, ";
                        cmd += "@NroComprobanteDesde, @NroComprobanteHasta, @CompradorCodigoDocumento, @CompradorNroDocumento, ";
                        cmd += "@CompradorTipoResponsable, @CompradorTipoResponsableDescripcion, @CompradorRazonSocial, ";
                        cmd += "@FechaDesdeServicioFacturado, @FechaHastaServicioFacturado, @FechaVencimientoPago, @CondicionPago, ";
                        cmd += "@CompradorDireccion, @CompradorLocalidad, @CompradorProvincia, @CompradorPais, @CompradorCodigoPostal, ";
                        cmd += "@CompradorNroIIBB, @CompradorCodigoCliente, @CompradorNroReferencia, @CompradorEmail, ";
                        cmd += "@NroRemito, @Importe, @ImporteComprobanteB, @ImporteNoGravado, @ImporteGravado, @AlicuotaIVA, @ImporteImpuestoLiquidado, ";
                        cmd += "@ImporteRNI_Percepcion, @ImporteExento, @ImportePercepciones_PagosCuentaImpuestosNacionales, ";
                        cmd += "@ImportePercepcionIIBB, @TasaIIBB, @CodigoJurisdiccionIIBB, @ImportePercepcionImpuestosMunicipales, ";
                        cmd += "@JurisdiccionImpuestosMunicipales, @ImporteImpuestosInternos, @ImporteMonedaFacturacion, @ImporteMonedaFacturacionComprobanteB, @ImporteNoGravadoMonedaFacturacion, ";
                        cmd += "@ImporteGravadoMonedaFacturacion, @ImporteImpuestoLiquidadoMonedaFacturacion, @ImporteRNI_PercepcionMonedaFacturacion, ";
                        cmd += "@ImporteExentoMonedaFacturacion, @ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion, ";
                        cmd += "@ImportePercepcionIIBBMonedaFacturacion, @ImportePercepcionImpuestosMunicipalesMonedaFacturacion, ";
                        cmd += "@ImporteImpuestosInternosMonedaFacturacion, @CantidadAlicuotasIVA, @CodigoOperacion, @TasaCambio, ";
                        cmd += "@CodigoMoneda, @ImporteEscrito, @CantidadRegistrosDetalle, @CodigoMecanismoDistribucion, @TipoExportacion, ";
                        cmd += "@PermisoExistente, @FormaPagoDescripcion, @IncoTerms, @Idioma, @Observaciones1, ";
                        cmd += "@Observaciones2, @Observaciones3, @LetraComprobante, @NroInternoERP, ";
                        cmd += "@EmisorRazonSocial, @EmisorDireccion, @EmisorCalle, @EmisorCP, @EmisorLocalidad, ";
                        cmd += "@EmisorProvincia, @EmisorPais, @EmisorTelefonos, @EmisorEMail, @OficinaVentas, @RapiPago, @ObservacionRapiPago, @PagoFacil, ";
                        cmd += "@OPER, @NOPER, @DAGRUF, @FACTORI, @FACTORI_FORMATEADO, @USUARIO, @FECPG1_FORMATEADO, @FECPG2_FORMATEADO, @CUOTAIVA105, @CUOTAIVA21) ";

                        cmd += "SET @CabeceraID = SCOPE_IDENTITY()";

                        dbQuery = new SqlCommand();
                        dbQuery.Connection = dbConnection;
                        dbQuery.CommandText = cmd;

                        dbQuery.Parameters.Add(new SqlParameter("@CabeceraID", System.Data.SqlDbType.Int));
                        dbQuery.Parameters["@CabeceraID"].Direction = System.Data.ParameterDirection.Output;

                        dbQuery.Parameters.Add(new SqlParameter("@NroInternoERP", thisHeader.NroInternoERP));
                        dbQuery.Parameters.Add(new SqlParameter("@EmpresaID", Convert.ToInt16(thisHeader.EmpresaID)));
                        dbQuery.Parameters.Add(new SqlParameter("@Iniciada", "Iniciada"));
                        dbQuery.Parameters.Add(new SqlParameter("@TipoTransaccion", thisHeader.TipoTransaccion));
                        dbQuery.Parameters.Add(new SqlParameter("@FechaComprobante", Convert.ToDateTime(thisHeader.FechaComprobante)));
                        dbQuery.Parameters.Add(new SqlParameter("@TipoComprobante", thisHeader.TipoComprobante));
                        dbQuery.Parameters.Add(new SqlParameter("@PuntoVenta", thisHeader.PuntoVenta));

                        dbQuery.Parameters.Add(new SqlParameter("@NroComprobanteDesde", thisHeader.NroComprobanteDesde));
                        dbQuery.Parameters.Add(new SqlParameter("@NroComprobanteHasta", thisHeader.NroComprobanteHasta));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorCodigoDocumento", thisHeader.CompradorCodigoDocumento));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorNroDocumento", thisHeader.CompradorNroDocumento));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorTipoResponsable", thisHeader.CompradorTipoResponsable));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorTipoResponsableDescripcion", thisHeader.CompradorTipoResponsableDescripcion));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorRazonSocial", thisHeader.CompradorRazonSocial));
                        dbQuery.Parameters.Add(new SqlParameter("@FechaDesdeServicioFacturado", Convert.ToDateTime(thisHeader.FechaDesdeServicioFacturado)));
                        dbQuery.Parameters.Add(new SqlParameter("@FechaHastaServicioFacturado", Convert.ToDateTime(thisHeader.FechaHastaServicioFacturado)));
                        dbQuery.Parameters.Add(new SqlParameter("@FechaVencimientoPago", Convert.ToDateTime(thisHeader.FechaVencimientoPago)));
                        dbQuery.Parameters.Add(new SqlParameter("@CondicionPago", thisHeader.CondicionPago));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorDireccion", thisHeader.CompradorDireccion));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorLocalidad", thisHeader.CompradorLocalidad));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorProvincia", thisHeader.CompradorProvincia));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorPais", thisHeader.CompradorPais));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorCodigoPostal", thisHeader.CompradorCodigoPostal));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorNroIIBB", thisHeader.CompradorNroIIBB));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorCodigoCliente", thisHeader.CompradorCodigoCliente));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorNroReferencia", thisHeader.CompradorNroReferencia));
                        dbQuery.Parameters.Add(new SqlParameter("@CompradorEmail", thisHeader.CompradorEmail));
                        dbQuery.Parameters.Add(new SqlParameter("@NroRemito", thisHeader.NroRemito));
                        dbQuery.Parameters.Add(new SqlParameter("@CodigoJurisdiccionIIBB", thisHeader.CodigoJurisdiccionIIBB));
                        dbQuery.Parameters.Add(new SqlParameter("@TasaIIBB", thisHeader.TasaIIBB));
                        dbQuery.Parameters.Add(new SqlParameter("@JurisdiccionImpuestosMunicipales", thisHeader.JurisdiccionImpuestosMunicipales));

                        thisHeader.Importe = GetFormatedFloat( thisHeader.Importe);
                        dbQuery.Parameters.Add(new SqlParameter("@Importe", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@Importe"].Value = thisHeader.Importe;

                        thisHeader.ImporteComprobanteB  = GetFormatedFloat( thisHeader.ImporteComprobanteB);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteComprobanteB", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteComprobanteB"].Value = thisHeader.ImporteComprobanteB;

                        thisHeader.ImporteNoGravado = GetFormatedFloat( thisHeader.ImporteNoGravado);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteNoGravado", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteNoGravado"].Value = thisHeader.ImporteNoGravado;

                        thisHeader.ImporteGravado = GetFormatedFloat( thisHeader.ImporteGravado);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteGravado", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteGravado"].Value = thisHeader.ImporteGravado;

                        thisHeader.AlicuotaIVA = GetFormatedFloat( thisHeader.AlicuotaIVA);
                        dbQuery.Parameters.Add(new SqlParameter("@AlicuotaIVA", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@AlicuotaIVA"].Value = thisHeader.AlicuotaIVA;

                        thisHeader.ImporteImpuestoLiquidado = GetFormatedFloat( thisHeader.ImporteImpuestoLiquidado);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteImpuestoLiquidado", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteImpuestoLiquidado"].Value = thisHeader.ImporteImpuestoLiquidado;

                        thisHeader.ImporteRNI_Percepcion = GetFormatedFloat( thisHeader.ImporteRNI_Percepcion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteRNI_Percepcion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteRNI_Percepcion"].Value = thisHeader.ImporteRNI_Percepcion;

                        thisHeader.ImporteExento = GetFormatedFloat( thisHeader.ImporteExento );
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteExento", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteExento"].Value = thisHeader.ImporteExento;

                        thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionales = GetFormatedFloat( thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionales);
                        dbQuery.Parameters.Add(new SqlParameter("@ImportePercepciones_PagosCuentaImpuestosNacionales", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImportePercepciones_PagosCuentaImpuestosNacionales"].Value = thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionales;

                        thisHeader.ImportePercepcionIIBB = GetFormatedFloat(thisHeader.ImportePercepcionIIBB);
                        dbQuery.Parameters.Add(new SqlParameter("@ImportePercepcionIIBB", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImportePercepcionIIBB"].Value = thisHeader.ImportePercepcionIIBB;

                        thisHeader.ImportePercepcionImpuestosMunicipales = GetFormatedFloat( thisHeader.ImportePercepcionImpuestosMunicipales);
                        dbQuery.Parameters.Add(new SqlParameter("@ImportePercepcionImpuestosMunicipales", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImportePercepcionImpuestosMunicipales"].Value = thisHeader.ImportePercepcionImpuestosMunicipales;

                        thisHeader.ImporteImpuestosInternos = GetFormatedFloat( thisHeader.ImporteImpuestosInternos);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteImpuestosInternos", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteImpuestosInternos"].Value = thisHeader.ImporteImpuestosInternos;

                        thisHeader.ImporteMonedaFacturacion = GetFormatedFloat( thisHeader.ImporteMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteMonedaFacturacion"].Value = thisHeader.ImporteMonedaFacturacion;

                        thisHeader.ImporteMonedaFacturacionComprobanteB = GetFormatedFloat( thisHeader.ImporteMonedaFacturacionComprobanteB);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteMonedaFacturacionComprobanteB", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteMonedaFacturacionComprobanteB"].Value = thisHeader.ImporteMonedaFacturacionComprobanteB;

                        thisHeader.ImporteNoGravadoMonedaFacturacion = GetFormatedFloat( thisHeader.ImporteNoGravadoMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteNoGravadoMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteNoGravadoMonedaFacturacion"].Value = thisHeader.ImporteNoGravadoMonedaFacturacion;

                        thisHeader.ImporteGravadoMonedaFacturacion = GetFormatedFloat( thisHeader.ImporteGravadoMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteGravadoMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteGravadoMonedaFacturacion"].Value = thisHeader.ImporteGravadoMonedaFacturacion;

                        thisHeader.ImporteImpuestoLiquidadoMonedaFacturacion = GetFormatedFloat( thisHeader.ImporteImpuestoLiquidadoMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteImpuestoLiquidadoMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteImpuestoLiquidadoMonedaFacturacion"].Value = thisHeader.ImporteImpuestoLiquidadoMonedaFacturacion;

                        thisHeader.ImporteRNI_PercepcionMonedaFacturacion = GetFormatedFloat( thisHeader.ImporteRNI_PercepcionMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteRNI_PercepcionMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteRNI_PercepcionMonedaFacturacion"].Value = thisHeader.ImporteRNI_PercepcionMonedaFacturacion;

                        thisHeader.ImporteExentoMonedaFacturacion = GetFormatedFloat( thisHeader.ImporteExentoMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteExentoMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteExentoMonedaFacturacion"].Value = thisHeader.ImporteExentoMonedaFacturacion;

                        thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion = GetFormatedFloat( thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion"].Value = thisHeader.ImportePercepciones_PagosCuentaImpuestosNacionalesMonedaFacturacion;

                        thisHeader.ImportePercepcionIIBBMonedaFacturacion = GetFormatedFloat( thisHeader.ImportePercepcionIIBBMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImportePercepcionIIBBMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImportePercepcionIIBBMonedaFacturacion"].Value = thisHeader.ImportePercepcionIIBBMonedaFacturacion;

                        thisHeader.ImportePercepcionImpuestosMunicipalesMonedaFacturacion = GetFormatedFloat( thisHeader.ImportePercepcionImpuestosMunicipalesMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImportePercepcionImpuestosMunicipalesMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImportePercepcionImpuestosMunicipalesMonedaFacturacion"].Value = thisHeader.ImportePercepcionImpuestosMunicipalesMonedaFacturacion;

                        thisHeader.ImporteImpuestosInternosMonedaFacturacion = GetFormatedFloat( thisHeader.ImporteImpuestosInternosMonedaFacturacion);
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteImpuestosInternosMonedaFacturacion", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@ImporteImpuestosInternosMonedaFacturacion"].Value = thisHeader.ImporteImpuestosInternosMonedaFacturacion;

                        thisHeader.CantidadAlicuotasIVA = GetFormatedFloat( thisHeader.CantidadAlicuotasIVA);
                        dbQuery.Parameters.Add(new SqlParameter("@CantidadAlicuotasIVA", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@CantidadAlicuotasIVA"].Value = thisHeader.CantidadAlicuotasIVA;

                        thisHeader.TasaCambio = GetFormatedFloat( thisHeader.TasaCambio);
                        dbQuery.Parameters.Add(new SqlParameter("@TasaCambio", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@TasaCambio"].Value = thisHeader.TasaCambio;

                        thisHeader.CantidadRegistrosDetalle = GetFormatedFloat( thisHeader.CantidadRegistrosDetalle);
                        dbQuery.Parameters.Add(new SqlParameter("@CantidadRegistrosDetalle", System.Data.SqlDbType.Int));
                        dbQuery.Parameters["@CantidadRegistrosDetalle"].Value = thisHeader.CantidadRegistrosDetalle;

                        dbQuery.Parameters.Add(new SqlParameter("@CodigoMoneda", thisHeader.CodigoMoneda));
                        dbQuery.Parameters.Add(new SqlParameter("@ImporteEscrito", thisHeader.ImporteEscrito));
                        dbQuery.Parameters.Add(new SqlParameter("@CodigoOperacion", thisHeader.CodigoOperacion));
                        dbQuery.Parameters.Add(new SqlParameter("@CodigoMecanismoDistribucion", thisHeader.CodigoMecanismoDistribucion));
                        dbQuery.Parameters.Add(new SqlParameter("@TipoExportacion", thisHeader.TipoExportacion));
                        dbQuery.Parameters.Add(new SqlParameter("@PermisoExistente", thisHeader.PermisoExistente));
                        dbQuery.Parameters.Add(new SqlParameter("@FormaPagoDescripcion", thisHeader.FormaPagoDescripcion));
                        dbQuery.Parameters.Add(new SqlParameter("@IncoTerms", thisHeader.IncoTerms));
                        dbQuery.Parameters.Add(new SqlParameter("@Idioma", thisHeader.Idioma));
                        dbQuery.Parameters.Add(new SqlParameter("@Observaciones1", thisHeader.Observaciones1));
                        dbQuery.Parameters.Add(new SqlParameter("@Observaciones2", thisHeader.Observaciones2));
                        dbQuery.Parameters.Add(new SqlParameter("@Observaciones3", thisHeader.Observaciones3));
                        dbQuery.Parameters.Add(new SqlParameter("@LetraComprobante", thisHeader.LetraComprobante));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorRazonSocial", thisHeader.EmisorRazonSocial));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorDireccion", thisHeader.EmisorDireccion));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorCalle", thisHeader.EmisorCalle));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorCP", thisHeader.EmisorCP));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorLocalidad", thisHeader.EmisorLocalidad));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorProvincia", thisHeader.EmisorProvincia));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorPais", thisHeader.EmisorPais));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorTelefonos", thisHeader.EmisorTelefonos));
                        dbQuery.Parameters.Add(new SqlParameter("@EmisorEMail", thisHeader.EmisorEMail));
                        dbQuery.Parameters.Add(new SqlParameter("@OficinaVentas", thisHeader.OficinaVentas));
                        dbQuery.Parameters.Add(new SqlParameter("@RapiPago", thisHeader.RapiPago));
                        dbQuery.Parameters.Add(new SqlParameter("@ObservacionRapiPago", thisHeader.ObservacionRapiPago));
                        dbQuery.Parameters.Add(new SqlParameter("@PagoFacil", thisHeader.PagoFacil));
                        dbQuery.Parameters.Add(new SqlParameter("@OPER", thisHeader.OPER));
                        dbQuery.Parameters.Add(new SqlParameter("@NOPER", thisHeader.NOPER));
                        dbQuery.Parameters.Add(new SqlParameter("@DAGRUF", thisHeader.DAGRUF)); 
                        dbQuery.Parameters.Add(new SqlParameter("@FACTORI", thisHeader.FACTORI));
                        dbQuery.Parameters.Add(new SqlParameter("@FACTORI_FORMATEADO", thisHeader.FACTORI_FORMATEADO));
                        dbQuery.Parameters.Add(new SqlParameter("@USUARIO", thisHeader.USUARIO));
                        dbQuery.Parameters.Add(new SqlParameter("@FECPG1_FORMATEADO", thisHeader.FECPG1_FORMATEADO));
                        dbQuery.Parameters.Add(new SqlParameter("@FECPG2_FORMATEADO", thisHeader.FECPG2_FORMATEADO));

                        thisHeader.CUOTAIVA105 = GetFormatedFloat(  thisHeader.CUOTAIVA105);
                        dbQuery.Parameters.Add(new SqlParameter("@CUOTAIVA105", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@CUOTAIVA105"].Value = thisHeader.CUOTAIVA105;

                        thisHeader.CUOTAIVA21 = GetFormatedFloat( thisHeader.CUOTAIVA21);
                        dbQuery.Parameters.Add(new SqlParameter("@CUOTAIVA21", System.Data.SqlDbType.Float));
                        dbQuery.Parameters["@CUOTAIVA21"].Value = thisHeader.CUOTAIVA21;

                        dbQuery.ExecuteNonQuery();

                        thisHeader.SQLID = dbQuery.Parameters["@CabeceraID"].SqlValue.ToString();

                        foreach (RequestLine thisLine in thisHeader.RequestLines)
                        {
                            cmd = "insert into CbteLinea (CbteID, CodigoProductoEmpresa, CodigoProductoNCM, CodigoProductoSecretaria, ";
                            cmd += "Descripcion, Cantidad, UnidadMedida, ImportePrecioUnitario, ImporteBonificacion, ImporteAjuste, ";
                            cmd += "ImporteSubtotal, ImportePrecioUnitarioMonedaFacturacion, ImporteBonificacionMonedaFacturacion, ";
                            cmd += "ImporteAjusteMonedaFacturacion, ImporteSubtotalMonedaFacturacion, ImporteSubtotalMonedaFacturacionConIVA, ";
                            cmd += "AlicuotaIVA, IndicadorExentoGravadoNoGravado, Observaciones, MesPrestacion ) ";

                            cmd += "values (@cabeceraIdentity, @CodigoProductoEmpresa, @CodigoProductoNCM, @CodigoProductoSecretaria, ";
                            cmd += "@Descripcion, @Cantidad, @UnidadMedida, @ImportePrecioUnitario, @ImporteBonificacion, @ImporteAjuste, ";
                            cmd += "@ImporteSubtotal, @ImportePrecioUnitarioMonedaFacturacion, @ImporteBonificacionMonedaFacturacion, ";
                            cmd += "@ImporteAjusteMonedaFacturacion, @ImporteSubtotalMonedaFacturacion, @ImporteSubtotalMonedaFacturacionConIVA, ";
                            cmd += "@AlicuotaIVA, @IndicadorExentoGravadoNoGravado, @Observaciones, @MesPrestacion ) ";

                            cmd += "SET @LineaID = SCOPE_IDENTITY()";

                            dbQuery = new SqlCommand();
                            dbQuery.Connection = dbConnection;
                            dbQuery.CommandText = cmd;

                            dbQuery.Parameters.Add(new SqlParameter("@LineaID", System.Data.SqlDbType.Int));
                            dbQuery.Parameters["@LineaID"].Direction = System.Data.ParameterDirection.Output;

                            dbQuery.Parameters.Add(new SqlParameter("@cabeceraIdentity", Convert.ToInt32(thisHeader.SQLID)));
                            dbQuery.Parameters.Add(new SqlParameter("@CodigoProductoEmpresa", thisLine.CodigoProductoEmpresa));
                            dbQuery.Parameters.Add(new SqlParameter("@CodigoProductoNCM", thisLine.CodigoProductoNCM));
                            dbQuery.Parameters.Add(new SqlParameter("@CodigoProductoSecretaria", thisLine.CodigoProductoSecretaria));
                            dbQuery.Parameters.Add(new SqlParameter("@Descripcion", thisLine.Descripcion));
                            dbQuery.Parameters.Add(new SqlParameter("@UnidadMedida", thisLine.UnidadMedida));

                            thisLine.Cantidad = GetFormatedFloat(thisLine.Cantidad);
                            dbQuery.Parameters.Add(new SqlParameter("@Cantidad", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@Cantidad"].Value = thisLine.Cantidad;

                            thisLine.ImportePrecioUnitario = GetFormatedFloat(thisLine.ImportePrecioUnitario);
                            dbQuery.Parameters.Add(new SqlParameter("@ImportePrecioUnitario", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImportePrecioUnitario"].Value = thisLine.ImportePrecioUnitario;

                            thisLine.ImporteBonificacion = GetFormatedFloat(thisLine.ImporteBonificacion);
                            dbQuery.Parameters.Add(new SqlParameter("@ImporteBonificacion", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImporteBonificacion"].Value = thisLine.ImporteBonificacion;

                            thisLine.ImporteAjuste = GetFormatedFloat(thisLine.ImporteAjuste);
                            dbQuery.Parameters.Add(new SqlParameter("@ImporteAjuste", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImporteAjuste"].Value = thisLine.ImporteAjuste;

                            thisLine.ImporteSubtotal = GetFormatedFloat(thisLine.ImporteSubtotal);
                            dbQuery.Parameters.Add(new SqlParameter("@ImporteSubtotal", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImporteSubtotal"].Value = thisLine.ImporteSubtotal;

                            thisLine.ImportePrecioUnitarioMonedaFacturacion = GetFormatedFloat(thisLine.ImportePrecioUnitarioMonedaFacturacion);
                            dbQuery.Parameters.Add(new SqlParameter("@ImportePrecioUnitarioMonedaFacturacion", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImportePrecioUnitarioMonedaFacturacion"].Value = thisLine.ImportePrecioUnitarioMonedaFacturacion;

                            thisLine.ImporteBonificacionMonedaFacturacion = GetFormatedFloat(thisLine.ImporteBonificacionMonedaFacturacion);
                            dbQuery.Parameters.Add(new SqlParameter("@ImporteBonificacionMonedaFacturacion", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImporteBonificacionMonedaFacturacion"].Value = thisLine.ImporteBonificacionMonedaFacturacion;

                            thisLine.ImporteAjusteMonedaFacturacion = GetFormatedFloat(thisLine.ImporteAjusteMonedaFacturacion);
                            dbQuery.Parameters.Add(new SqlParameter("@ImporteAjusteMonedaFacturacion", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImporteAjusteMonedaFacturacion"].Value = thisLine.ImporteAjusteMonedaFacturacion;

                            thisLine.ImporteSubtotalMonedaFacturacion = GetFormatedFloat(thisLine.ImporteSubtotalMonedaFacturacion);
                            dbQuery.Parameters.Add(new SqlParameter("@ImporteSubtotalMonedaFacturacion", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImporteSubtotalMonedaFacturacion"].Value = thisLine.ImporteSubtotalMonedaFacturacion;

                            thisLine.ImporteSubtotalMonedaFacturacionConIVA = GetFormatedFloat(thisLine.ImporteSubtotalMonedaFacturacionConIVA);
                            dbQuery.Parameters.Add(new SqlParameter("@ImporteSubtotalMonedaFacturacionConIVA", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@ImporteSubtotalMonedaFacturacionConIVA"].Value = thisLine.ImporteSubtotalMonedaFacturacionConIVA;

                            thisLine.AlicuotaIVA = GetFormatedFloat(thisLine.AlicuotaIVA);
                            dbQuery.Parameters.Add(new SqlParameter("@AlicuotaIVA", System.Data.SqlDbType.Float));
                            dbQuery.Parameters["@AlicuotaIVA"].Value = thisLine.AlicuotaIVA;

                            dbQuery.Parameters.Add(new SqlParameter("@IndicadorExentoGravadoNoGravado", thisLine.IndicadorExentoGravadoNoGravado));
                            dbQuery.Parameters.Add(new SqlParameter("@Observaciones ", thisLine.Observaciones));
                            dbQuery.Parameters.Add(new SqlParameter("@MesPrestacion ", thisLine.MesPrestacion));

                            dbQuery.ExecuteNonQuery();

                            thisLine.SQLID = dbQuery.Parameters["@LineaID"].SqlValue.ToString();
                        }

                        if (thisHeader.RequestAlicuotas != null)
                        {
                            foreach (RequestAlicuota thisAlicuota in thisHeader.RequestAlicuotas)
                            {
                                cmd = "insert into CbteImpuesto (CbteID, Id, Tipo, BaseImp, ";
                                cmd += "Importe, ImporteMonedaFacturacion, Descripcion, Codigo ) ";

                                cmd += "values (@CbteID, @Id, @Tipo, @BaseImp, ";
                                cmd += "@Importe, @ImporteMonedaFacturacion, @Descripcion, @Codigo) ";

                                cmd += "SET @ImpuestoID = SCOPE_IDENTITY()";

                                dbQuery = new SqlCommand();
                                dbQuery.Connection = dbConnection;
                                dbQuery.CommandText = cmd;

                                dbQuery.Parameters.Add(new SqlParameter("@ImpuestoID", System.Data.SqlDbType.Int));
                                dbQuery.Parameters["@ImpuestoID"].Direction = System.Data.ParameterDirection.Output;

                                dbQuery.Parameters.Add(new SqlParameter("@CbteID", Convert.ToInt32(thisHeader.SQLID)));
                                
                                thisAlicuota.Id = ObtenerEquivalenciaImpuesto("EquivAFIPImpuesto",
                                    thisHeader.EmpresaID, thisAlicuota.Codigo, ref thisAlicuota.Descripcion, false);
                                
                                if (thisAlicuota.Id != string.Empty)
                                {
                                    dbQuery.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(thisAlicuota.Id)));

                                    dbQuery.Parameters.Add(new SqlParameter("@Tipo", thisAlicuota.Tipo));

                                    dbQuery.Parameters.Add(new SqlParameter("@Codigo", thisAlicuota.Codigo));

                                    thisAlicuota.BaseImp = GetFormatedFloat(thisAlicuota.BaseImp);
                                    dbQuery.Parameters.Add(new SqlParameter("@BaseImp", System.Data.SqlDbType.Float));
                                    dbQuery.Parameters["@BaseImp"].Value = thisAlicuota.BaseImp;

                                    thisAlicuota.Importe = GetFormatedFloat(thisAlicuota.Importe);
                                    dbQuery.Parameters.Add(new SqlParameter("@Importe", System.Data.SqlDbType.Float));
                                    dbQuery.Parameters["@Importe"].Value = thisAlicuota.Importe;

                                    thisAlicuota.ImporteMonedaFacturacion = GetFormatedFloat(thisAlicuota.ImporteMonedaFacturacion);
                                    dbQuery.Parameters.Add(new SqlParameter("@ImporteMonedaFacturacion", System.Data.SqlDbType.Float));
                                    dbQuery.Parameters["@ImporteMonedaFacturacion"].Value = thisAlicuota.ImporteMonedaFacturacion;

                                    dbQuery.Parameters.Add(new SqlParameter("@Descripcion", thisAlicuota.Descripcion));

                                    dbQuery.ExecuteNonQuery();

                                    thisAlicuota.ImpuestoID = dbQuery.Parameters["@ImpuestoID"].SqlValue.ToString();
                                }
                                else
                                {
                                    this.LogError("0", "0", "SQLEngine", "Error (Alicuota) de Equivalencia Alicuota(" + thisAlicuota.Codigo + ")");

                                    return false;
                                }
                            }
                        }

                        if (thisHeader.RequestTributos != null)
                        {
                            foreach (RequestTributo thisTributo in thisHeader.RequestTributos)
                            {
                                cmd = "insert into CbteImpuesto (CbteID, Id, Tipo, BaseImp, ";
                                cmd += "Importe, ImporteMonedaFacturacion, Descripcion, Codigo ) ";

                                cmd += "values (@CbteID, @Id, @Tipo, @BaseImp, ";
                                cmd += "@Importe, @ImporteMonedaFacturacion, @Descripcion, @Codigo ) ";

                                cmd += "SET @ImpuestoID = SCOPE_IDENTITY()";

                                dbQuery = new SqlCommand();
                                dbQuery.Connection = dbConnection;
                                dbQuery.CommandText = cmd;

                                dbQuery.Parameters.Add(new SqlParameter("@ImpuestoID", System.Data.SqlDbType.Int));
                                dbQuery.Parameters["@ImpuestoID"].Direction = System.Data.ParameterDirection.Output;

                                dbQuery.Parameters.Add(new SqlParameter("@CbteID", Convert.ToInt32(thisHeader.SQLID)));

                                TablaAfipImpuesto tafipimp = ObtenerEquivalenciaTributo(thisHeader.EmpresaID, thisTributo.Codigo, false);
                                thisTributo.Id = tafipimp.CodigoAFIP;
                                thisTributo.Descripcion = tafipimp.Descripcion;
                                //thisTributo.Id = ObtenerEquivalenciaImpuesto("EquivAFIPImpuesto", thisHeader.EmpresaID, thisTributo.Codigo, ref thisTributo.Descripcion, false);

                                if (thisTributo.Id != string.Empty)
                                {
                                    dbQuery.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(thisTributo.Id)));

                                    dbQuery.Parameters.Add(new SqlParameter("@Tipo", thisTributo.Tipo));

                                    dbQuery.Parameters.Add(new SqlParameter("@Codigo", thisTributo.Codigo));

                                    thisTributo.BaseImp = GetFormatedFloat(thisTributo.BaseImp);
                                    dbQuery.Parameters.Add(new SqlParameter("@BaseImp", System.Data.SqlDbType.Float));
                                    dbQuery.Parameters["@BaseImp"].Value = thisTributo.BaseImp;

                                    thisTributo.Importe = GetFormatedFloat(thisTributo.Importe);
                                    dbQuery.Parameters.Add(new SqlParameter("@Importe", System.Data.SqlDbType.Float));
                                    dbQuery.Parameters["@Importe"].Value = thisTributo.Importe;

                                    thisTributo.ImporteMonedaFacturacion = GetFormatedFloat(thisTributo.ImporteMonedaFacturacion);
                                    dbQuery.Parameters.Add(new SqlParameter("@ImporteMonedaFacturacion", System.Data.SqlDbType.Float));
                                    dbQuery.Parameters["@ImporteMonedaFacturacion"].Value = thisTributo.ImporteMonedaFacturacion;

                                    dbQuery.Parameters.Add(new SqlParameter("@Descripcion", thisTributo.Descripcion));

                                    thisTributo.Alic = GetFormatedFloat(tafipimp.Porcentaje);

                                    dbQuery.ExecuteNonQuery();

                                    thisTributo.ImpuestoID = dbQuery.Parameters["@ImpuestoID"].SqlValue.ToString();
                                }
                                else
                                {
                                    this.LogError("0", "0", "SQLEngine", "Error (Tributo) de Equivalencia Tributos(" + thisTributo.Codigo + ")");
                                    return false;
                                }
                            }
                        }
                        bResult = true;
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                this.LogError("0", "0", "SQLEngine", "Error Data :" + ex.Message);
                bResult = false;
            }

            return bResult;
        }

        public void LogBatchEnd(string SQLID, string estadoDocumento, string cae, string fechaVencimiento)
        {
            SqlCommand dbQuery = new SqlCommand();

            string cmd = "";

            try
            {
                if (Open())
                {
                    dbQuery.Connection = dbConnection;

                    cmd = "update CbteCabecera set ";
                    cmd += "EstadoTransaccion = @estadoDocumento, ";
                    cmd += "CAE = @cae ";
                    if (fechaVencimiento != string.Empty)
                    {
                        cmd += ", FechaVencimiento = @fechaVencimiento ";
                    }
                    cmd += "where CbteID = @SQLID";

                    dbQuery.Parameters.Add(new SqlParameter("@estadoDocumento", System.Data.SqlDbType.NChar));
                    dbQuery.Parameters.Add(new SqlParameter("@cae", System.Data.SqlDbType.NVarChar));

                    if (fechaVencimiento != string.Empty)
                    {
                        dbQuery.Parameters.Add(new SqlParameter("@fechaVencimiento", System.Data.SqlDbType.SmallDateTime));
                    }

                    dbQuery.Parameters.Add(new SqlParameter("@SQLID", System.Data.SqlDbType.Int));
                    dbQuery.Parameters["@estadoDocumento"].Value = estadoDocumento;
                    dbQuery.Parameters["@cae"].Value = cae;

                    if (fechaVencimiento != string.Empty)
                    {
                        dbQuery.Parameters["@fechaVencimiento"].Value = GetFormatedDate(fechaVencimiento);
                    }

                    dbQuery.Parameters["@SQLID"].Value = Convert.ToInt32(SQLID);

                    dbQuery.CommandText = cmd;
                    dbQuery.ExecuteNonQuery();

                    Close();
                }
            }
            catch (Exception ex)
            {
                this.LogError(SQLID, "0", "SQLEngine", "Error:" + ex.Message + "  |||  Fecha: " + GetFormatedDate(fechaVencimiento));
            }
        }

        public void UpdateCabeceraNombreObjetoSalida(string SQLID, string nombreObjetoSalida)
        {
            SqlCommand dbQuery = new SqlCommand();

            string cmd = "";

            try
            {
                if (Open())
                {
                    dbQuery.Connection = dbConnection;

                    cmd = "update CbteCabecera set NombreObjetoSalida = ";
                    cmd += "@nombreObjetoSalida ";
                    cmd += "where CbteID = @SQLID";

                    dbQuery.Parameters.Add(new SqlParameter("@nombreObjetoSalida", SqlDbType.NVarChar));
                    dbQuery.Parameters.Add(new SqlParameter("@SQLID", SqlDbType.Int));
                    dbQuery.Parameters[0].Value = nombreObjetoSalida;
                    dbQuery.Parameters[1].Value = Convert.ToInt32(SQLID);

                    dbQuery.CommandText = cmd;
                    dbQuery.ExecuteNonQuery();

                    Close();
                }
            }
            catch (Exception ex)
            {
                this.LogError(SQLID, "0", "SQLEngine", "Error:" + ex.Message);
            }
        }

        public void UpdateCabeceraBatchUniqueId(string SQLID, string BatchUniqueId)
        {
            SqlCommand dbQuery = new SqlCommand();

            string cmd = "";

            try
            {
                if (Open())
                {
                    dbQuery.Connection = dbConnection;

                    cmd = "update CbteCabecera set BatchUniqueId = ";
                    cmd += "@BatchUniqueId ";
                    cmd += "where CbteID = @SQLID";

                    dbQuery.Parameters.Add(new SqlParameter("@BatchUniqueId", SqlDbType.NVarChar));
                    dbQuery.Parameters.Add(new SqlParameter("@SQLID", SqlDbType.Int));
                    dbQuery.Parameters[0].Value = BatchUniqueId;
                    dbQuery.Parameters[1].Value = Convert.ToInt32(SQLID);

                    dbQuery.CommandText = cmd;
                    dbQuery.ExecuteNonQuery();

                    Close();    
                }
            }
            catch (Exception ex)
            {
                this.LogError(SQLID, "0", "SQLEngine", "Error:" + ex.Message);
            }
        }

        #endregion

        #region LlenarTablasAFIP

        //TODO: Reemplazar por funcion unica
        public void InsertarCodigo(string TableName, string Codigo, string Descripcion)
        {
            //Primero lo borro
            string cmd = "delete from " + TableName + " where CodigoAFIP = '" + Codigo + "'";
            SqlCommand dbQuery = new SqlCommand();

            if (Open())
            {
                dbQuery.Connection = dbConnection;
                dbQuery.CommandText = cmd;
                dbQuery.ExecuteNonQuery();

                //Luego lo reinserto
                cmd = "insert into " + TableName + "(CodigoAFIP, Descripcion) values ('" + Codigo + "', '" + Descripcion + "')";
                dbQuery = new SqlCommand();
                dbQuery.Connection = dbConnection;
                dbQuery.CommandText = cmd;
                dbQuery.ExecuteNonQuery();

                Close();
            }
        }

        #endregion

        #region Equivalencias

        public string ObtenerEquivalencia(string TablaEquivalencia, string EmpresaID, string codigoEmpresa)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlDataReader dbReader = null;

            string result = string.Empty;

            try
            {
                if (Open())
                {
                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = "select * from " + TablaEquivalencia + " where EmpresaID = " + EmpresaID + " and CodigoEmpresa = '" + codigoEmpresa + "'";

                    dbReader = dbQuery.ExecuteReader();

                    if (dbReader.Read())
                    {
                        result = dbReader["CodigoAFIP"].ToString();
                    }
                    else
                    {
                        result = string.Empty;
                    }

                    Close();
                }
            }
            finally
            {
                dbReader.Close();
            }
            return result.Trim();
        }

        public TablaAfipImpuesto ObtenerEquivalenciaTributo(string EmpresaID, string codigoEmpresa, bool CloseConnection)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlDataReader dbReader = null;

            TablaAfipImpuesto result;
            result.EmpresaID = string.Empty;
            result.CodigoEmpresa = string.Empty;
            result.CodigoAFIP = string.Empty;
            result.Porcentaje = string.Empty;
            result.Descripcion = string.Empty;

            try
            {
                if (Open())
                {
                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = "select CodigoEmpresa,CodigoAFIP,Porcentaje,Descripcion from EquivAFIPImpuesto " +
                        "  where EmpresaID = " + EmpresaID.Trim() +
                        "  and CodigoEmpresa = '" + codigoEmpresa.ToUpper().Trim() + "' ";

                    dbReader = dbQuery.ExecuteReader();

                    if (dbReader.Read())
                    {
                        result.EmpresaID = EmpresaID.Trim();
                        if (dbReader["CodigoEmpresa"] != null) 
                            result.CodigoEmpresa = dbReader["CodigoEmpresa"].ToString().Trim();
                        if (dbReader["CodigoAFIP"] != null) 
                            result.CodigoAFIP = dbReader["CodigoAFIP"].ToString().Trim();
                        if (dbReader["Porcentaje"] != null) 
                            result.Porcentaje = dbReader["Porcentaje"].ToString().Trim();
                        if (dbReader["Descripcion"] != null) 
                            result.Descripcion = dbReader["Descripcion"].ToString().Trim();
                    }
                    
                    if (CloseConnection)
                        Close();
                }
            }
            catch (Exception ex)
            {
                String aerror = ex.Message;
            }
            finally
            {
                dbReader.Close();
            }
            return result;
        }

        public string ObtenerEquivalenciaImpuesto(string TablaEquivalencia, string EmpresaID, string codigoEmpresa, ref string Descripcion,
            bool CloseConnection)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlDataReader dbReader = null;
            
            string result = string.Empty;

            try
            {
                if (Open())
                {
                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = "select * from " + TablaEquivalencia + 
                        "  where EmpresaID = " + EmpresaID.Trim() + 
                        "  and CodigoEmpresa = '" + codigoEmpresa.ToUpper().Trim() + "' ";

                    dbReader = dbQuery.ExecuteReader();

                    if (dbReader.Read())
                    {
                        if (dbReader["CodigoAFIP"] != null)
                            result = dbReader["CodigoAFIP"].ToString().Trim();

                        if (dbReader["Descripcion"] != null)
                            Descripcion = dbReader["Descripcion"].ToString().Trim();
                    }
                    else
                    {
                        result = string.Empty;
                        Descripcion = string.Empty;
                    }

                    if (CloseConnection)
                        Close();
                }
            }
            catch (Exception ex)
            {
                String aerror = ex.Message;
            }
            finally
            {
                dbReader.Close();
            }
            return result.Trim();
        }
        #endregion

        public byte[] LoadPdfFromDB(string strId, string strCopyType)
        {
            byte[] ret = null;
            string query = "select " + strCopyType + " from CbteFiles where CbteID = '" + strId + "' and " + strCopyType + " is not null";

            try
            {
                if (Open())
                {
                    SqlCommand cmd = new SqlCommand(query, dbConnection);
                    var dr = cmd.ExecuteReader();

                    if (dr.Read())
                        ret = (byte[])dr[0];

                    if (dr != null)
                        dr.Close();

                    Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ret;
        }

        public bool ExistsPdfInDB(string strId)
        {
            int cant = 0;
            string query = "select count(1) as cant from CbteFiles where CbteID = '" + strId + "'";

            SqlCommand cmd = new SqlCommand(query, dbConnection);
            cant = Convert.ToInt32(cmd.ExecuteScalar());

            return (cant == 1);
        }

        public void SavePdfInDB(byte[] pdfbyte, string strId, string strCopyType)
        {
            SqlParameter pdfparameter = null;
            SqlCommand cmd = null;
            string query = string.Empty;

            try
            {
                //Defino entre hacer Insert o Update.

                if (!ExistsPdfInDB(strId))
                    query = "insert into CbteFiles (CbteID, " + strCopyType + ", Tipo) values (" + strId + ", @pdf, '.pdf')";
                else
                    query = "update CbteFiles set " + strCopyType + " = @pdf where CbteID = " + strId;

                pdfparameter = new SqlParameter();

                pdfparameter.SqlDbType = SqlDbType.Image;
                pdfparameter.ParameterName = "pdf";
                pdfparameter.Value = pdfbyte;

                cmd = new SqlCommand(query, dbConnection);
                cmd.Parameters.Add(pdfparameter);
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetItems(string TableName, string ListOfFields, string FieldCondition, int iTop)
        {
            SqlCommand dbQuery = new SqlCommand();

            DataTable dTable = new DataTable();

            string commandString = string.Empty;

            try
            {
                if (Open())
                {
                    commandString = "select " + ListOfFields + " from " + TableName;

                    if (FieldCondition != null && FieldCondition != string.Empty)
                        commandString += " where " + FieldCondition;

                    if (TableName == "CbteImpuesto")
                    {
                        commandString += " ORDER BY Tipo, id ";
                    }

                    dbQuery.Connection = dbConnection;
                    dbQuery.CommandText = commandString;

                    dTable.Load(dbQuery.ExecuteReader());

                    Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dTable;
        }

        static public DataTable GetItems(string DBServer, string DBName, string TableName, string ListOfFields, string FieldCondition, int iTop)
        {
            SqlCommand dbQuery = new SqlCommand();
            SqlConnection dbConnection = new SqlConnection();

            DataTable dTable = new DataTable();

            string commandString = string.Empty;

            try
            {
                commandString = "select ";

                if (iTop > 0)
                    commandString += "top(" + iTop + ")";

                commandString += ListOfFields + " from " + TableName;

                if (FieldCondition != null && FieldCondition != string.Empty)
                    commandString += " where " + FieldCondition;

                dbConnection.ConnectionString = "server=" + DBServer + ";Integrated Security=true;Database=" + DBName;
                dbConnection.Open();
              
                dbQuery.Connection = dbConnection;
                dbQuery.CommandText = commandString;
                
                dTable.Load(dbQuery.ExecuteReader());

                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dTable;
        }

        static public bool UpdateItem(string DBServer, string DBName, string TableName, string KeyName, string KeyType, string KeyValue, string[] AttributesNames, string[] AttributesTypes, string[] AttributesValues)
        {
            SqlConnection dbConnection = new SqlConnection();
            SqlCommand dbQuery = new SqlCommand();

            bool bResult = false;

            string cmd = string.Empty;

            try
            {
                dbConnection.ConnectionString = "server=" + DBServer + ";Integrated Security=true;Database=" + DBName;
                dbConnection.Open();

                dbQuery.Connection = dbConnection;

                cmd = "update " + TableName + " set ";

                for (int i = 0; i < AttributesNames.Length; i++)
                {
                    if (AttributesValues[i] != string.Empty && AttributesValues[i] != null && AttributesValues[i] != "NULL")
                    {
                        cmd += AttributesNames[i] + " = @" + AttributesNames[i];

                        if (i != AttributesNames.Length - 1)
                            cmd += ",";

                        dbQuery.Parameters.Add(GetParameter(AttributesNames[i], AttributesTypes[i], AttributesValues[i]));
                    }
                }

                if (cmd.LastIndexOf(",") == cmd.Length - 1)
                    cmd = cmd.Remove(cmd.Length - 1, 1);

                cmd += " where " + KeyName + " = @" + KeyName;
                dbQuery.Parameters.Add(GetParameter(KeyName, KeyType, KeyValue));

                dbQuery.CommandText = cmd;

                if (dbQuery.ExecuteNonQuery() > 0)
                    bResult = true;

                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return bResult;
        }

        static public SqlParameter GetParameter(string Name, string Type, string Value)
        {
            SqlParameter pResult = new SqlParameter();

            pResult.ParameterName = "@" + Name;

            switch (Type)
            {
                case "NVarChar":
                    pResult.SqlDbType = SqlDbType.NVarChar;
                    pResult.Value = Value;
                    break;

                case "NChar":
                    pResult.SqlDbType = SqlDbType.NChar;
                    pResult.Value = Value;
                    break;

                case "Int":
                    pResult.SqlDbType = SqlDbType.Int;

                    if (Value != string.Empty)
                        pResult.Value = Convert.ToInt32(Value);
                    else
                        pResult.Value = null;
                    break;

                case "Float":
                    pResult.SqlDbType = SqlDbType.Float;

                    if (Value != string.Empty)
                        pResult.Value = Convert.ToInt64(Value);
                    else
                        pResult.Value = null;
                    break;

                case "Bit":
                    pResult.SqlDbType = SqlDbType.Bit;

                    if (Value != string.Empty)
                        pResult.Value = Convert.ToBoolean(Value);
                    else
                        pResult.Value = null;
                    break;

                case "SmallDateTime":
                    pResult.SqlDbType = SqlDbType.SmallDateTime;

                    if (Value != string.Empty)
                        pResult.Value = Convert.ToDateTime(GetFormatedDate(Value));
                    else
                        pResult.Value = null;
                    break;
                default:
                    pResult.SqlDbType = SqlDbType.NVarChar;
                    pResult.Value = Value;
                    break;
            }

            return pResult;
        }

        static private string GetFormatedDate(string DueDate)
        {
            string fechaVencimiento = string.Empty;

            if (DueDate != string.Empty)
            {
                try
                {
                    DateTime dummyDate = Convert.ToDateTime("25/12/2010");

                    fechaVencimiento = DueDate.Substring(6, 2) + "/" + DueDate.Substring(4, 2) + "/" + DueDate.Substring(0, 4);
                }
                catch
                {
                    fechaVencimiento = DueDate.Substring(4, 2) + "/" + DueDate.Substring(6, 2) + "/" + DueDate.Substring(0, 4);
                }
            }

            return fechaVencimiento;
        }

        static private string GetFormatedFloat(string FloatValue)
        {
            string Aux = string.Empty;

            if (FloatValue != null && FloatValue != string.Empty)
            {
                Aux = FloatValue.Replace(".", ",");
                Aux = (Aux != string.Empty) ? Aux : "0";
            }
            else
            {
                Aux = "0";
            }
            return Aux;
        }
    }
}
