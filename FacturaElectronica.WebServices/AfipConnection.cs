using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using FacturaElectronica.Utils;

namespace FacturaElectronica.WebServices
{

    #region class AfipConnection

    public class AfipConnection
    {
        public string UrlWsaaWsdl = string.Empty;
        public string IdServicioNegocio = @"wsfe";
        public string RutaCertSigner = string.Empty;
        public bool VerboseMode = true;

        public string ConnectionTicket = string.Empty;
        public string ConnectionErrorDescription = "";
        public bool IsConnected = false;

        public UInt32 UniqueId; // Entero de 32 bits sin signo que identifica el requerimiento
        public DateTime GenerationTime; // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime; // Momento en el que exoira la solicitud
        public string Service = string.Empty; // Identificacion del WSN para el cual se solicita el TA
        public string Sign = string.Empty; // Firma de seguridad recibida en la respuesta
        public string Token = string.Empty; // Token de seguridad recibido en la respuesta
        public long Cuit;

        private DBEngine.SQLEngine sqlEngine = new DBEngine.SQLEngine();

        public AfipConnection(string serviceID, Settings oSettings)
        {
            UrlWsaaWsdl = oSettings.UrlAFIPwsaa;
            RutaCertSigner = oSettings.PathCertificate;

            RutaCertSigner = oSettings.PathCertificate;
            UrlWsaaWsdl = oSettings.UrlAFIPwsaa;
            IdServicioNegocio = serviceID;

            try
            {
                // Si el archivo con info de conexión no existe, lo creo
                if (!System.IO.File.Exists(oSettings.PathConnectionFiles + @"\" + IdServicioNegocio + ".XML"))
                {
                    this.Connect();
                    if (IsConnected)
                    {
                        string connectionTicket = ConnectionTicket;
                        // Grabar el archivo de salida
                        XmlDocument x_xmld = new XmlDocument();
                        x_xmld.LoadXml(connectionTicket);
                        x_xmld.Save(oSettings.PathConnectionFiles + @"\" + IdServicioNegocio + ".XML");
                    }
                }

                // Usar el token del archivo, si es que no está vencido
                if (System.IO.File.Exists(oSettings.PathConnectionFiles + @"\" + IdServicioNegocio + ".XML"))
                {
                    // #### Obtengo del tikcet de acceso TA.xml los campos token y sign ####
                    XmlDocument m_xmld;
                    XmlNodeList m_nodelist;
                    //XmlNode m_expiration_node;

                    m_xmld = new XmlDocument();
                    m_xmld.Load(oSettings.PathConnectionFiles + @"\" + IdServicioNegocio + ".XML"); // Debe indicar la Ruta de su Ticket de acceso
                    string stringExpirationTime = m_xmld.SelectSingleNode("/loginTicketResponse/header/expirationTime").FirstChild.Value;
                    DateTime expirationTime = Convert.ToDateTime(stringExpirationTime);
                    //Uso el ticket existente hasta que la fecha de vencimiento esté a 15 minutos de vencer
                    if (DateTime.Compare(expirationTime, DateTime.Now.AddMinutes(15)) > 0)
                    {
                        m_nodelist = m_xmld.SelectNodes("/loginTicketResponse/credentials");
                        // Loop through the nodes
                        foreach (XmlNode m_node in m_nodelist)
                        {
                            Token = m_node.ChildNodes.Item(0).InnerText;
                            Sign = m_node.ChildNodes.Item(1).InnerText;
                        }
                        XmlNode m_source = m_xmld.SelectSingleNode("/loginTicketResponse/header/destination").FirstChild;
                        Cuit = (long)Convert.ToDouble(m_source.Value.ToString().Substring(m_source.Value.ToString().IndexOf("CUIT ") + 5, 11));
                        IsConnected = true;
                    }
                    else
                    {
                        this.Connect();
                        if (IsConnected)
                        {
                            string connectionTicket = ConnectionTicket;
                            // Grabar el archivo de salida
                            XmlDocument x_xmld = new XmlDocument();
                            x_xmld.LoadXml(connectionTicket);
                            x_xmld.Save(oSettings.PathConnectionFiles + @"\" + IdServicioNegocio + ".XML");

                            // Setear las variables de la clase
                            stringExpirationTime = x_xmld.SelectSingleNode("/loginTicketResponse/header/expirationTime").FirstChild.Value;
                            expirationTime = Convert.ToDateTime(stringExpirationTime);
                            m_nodelist = x_xmld.SelectNodes("/loginTicketResponse/credentials");
                            // Loop through the nodes
                            foreach (XmlNode m_node in m_nodelist)
                            {
                                Token = m_node.ChildNodes.Item(0).InnerText;
                                Sign = m_node.ChildNodes.Item(1).InnerText;
                            }
                            XmlNode m_source = x_xmld.SelectSingleNode("/loginTicketResponse/header/destination").FirstChild;
                            Cuit = (long)Convert.ToDouble(m_source.Value.ToString().Substring(m_source.Value.ToString().IndexOf("CUIT ") + 5, 11));
                        }
                        else
                        {
                            string connectionErrorDescription = ConnectionErrorDescription;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               sqlEngine.LogError("0", "0", "AFIP Connection", ex.Message);
            }
        }


        public void Connect()
        {
            string strTicketRespuesta = "";

            try
            {
                LoginTicket objTicketRespuesta = new LoginTicket();

                strTicketRespuesta = objTicketRespuesta.ObtenerLoginTicketResponse(IdServicioNegocio, UrlWsaaWsdl, RutaCertSigner, VerboseMode);
                IsConnected = true;
                ConnectionTicket = strTicketRespuesta;
                ConnectionErrorDescription = "";
                UniqueId = objTicketRespuesta.UniqueId;
                GenerationTime = objTicketRespuesta.GenerationTime;
                ExpirationTime = objTicketRespuesta.ExpirationTime;
                Service = objTicketRespuesta.Service;
                Sign = objTicketRespuesta.Sign;
                Token = objTicketRespuesta.Token;
            }
            catch (Exception ex)
            {
                ConnectionErrorDescription = "Error conectando con la afip: " + ex.Message;
                IsConnected = false;
                ConnectionTicket = "";
                strTicketRespuesta = "";
            }
        }

    }

    #endregion

    #region class LoginTicket

    class LoginTicket
    {
        public UInt32 UniqueId; // Entero de 32 bits sin signo que identifica el requerimiento
        public DateTime GenerationTime; // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime; // Momento en el que exoira la solicitud
        public string Service; // Identificacion del WSN para el cual se solicita el TA
        public string Sign; // Firma de seguridad recibida en la respuesta
        public string Token; // Token de seguridad recibido en la respuesta

        public XmlDocument XmlLoginTicketRequest = null;
        public XmlDocument XmlLoginTicketResponse = null;
        public string RutaDelCertificadoFirmante;
        public string XmlStrLoginTicketRequestTemplate = @"<loginTicketRequest>
                                                              <header>
                                                                 <uniqueId></uniqueId>
                                                                 <generationTime></generationTime>
                                                                 <expirationTime></expirationTime>
                                                              </header>
                                                              <service>
                                                              </service>
                                                            </loginTicketRequest>";

        private bool _verboseMode = true;

        private Int32 _globalUniqueID = 0; // OJO! NO ES THREAD-SAFE


        public string ObtenerLoginTicketResponse(string argServicio, string argUrlWsaa, string argRutaCertX509Firmante, bool argVerbose)
        {
            this.RutaDelCertificadoFirmante = argRutaCertX509Firmante;
            this._verboseMode = argVerbose;

            CertificadosX509Lib certificadosX509Lib = new CertificadosX509Lib();
            certificadosX509Lib.VerboseMode = argVerbose;

            string cmsFirmadoBase64 = "";
            string strLoginTicketResponse = "";
            XmlNode xmlNodoUniqueId;
            XmlNode xmlNodoGenerationTime;
            XmlNode xmlNodoExpirationTime;
            XmlNode xmlNodoService;
            XmlDocument XmlLoginTicketRequest = new XmlDocument();

            // PASO 1: Genero el Login Ticket Request
            try
            {
                this._globalUniqueID += 1;

                XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);

                xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
                xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
                xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
                xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");

                // Las horas son UTC formato yyyy-MM-ddTHH:mm:ssZ
                xmlNodoGenerationTime.InnerText = DateTime.UtcNow.AddMinutes(-10).ToString("s") + "Z";
                xmlNodoExpirationTime.InnerText = DateTime.UtcNow.AddMinutes(+10).ToString("s") + "Z";
                xmlNodoUniqueId.InnerText = this._globalUniqueID.ToString();
                xmlNodoService.InnerText = argServicio;
                this.Service = argServicio;
            }
            catch (Exception excepcionAlGenerarLoginTicketRequest)
            {
                throw new Exception("***Error generando el LoginTicketRequest: ObtenerLoginTicketResponse: " + excepcionAlGenerarLoginTicketRequest.Message + excepcionAlGenerarLoginTicketRequest.StackTrace);
            }

            // PASO 2: Firmo el Login Ticket Request
            try
            {
                X509Certificate2 certFirmante = certificadosX509Lib.ObtieneCertificadoDesdeArchivo(RutaDelCertificadoFirmante);

                // Convierto el login ticket request a bytes, para firmar
                Encoding EncodedMsg = Encoding.UTF8;
                byte[] msgBytes = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml);

                // Firmo el msg y paso a Base64
                byte[] encodedSignedCms = certificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante);
                cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception("***Error firmando el LoginTicketRequest: ObtenerLoginTicketResponse: " + excepcionAlFirmar.Message);
            }


            // PASO 3: Invoco al WSAA para obtener el Login Ticket Response
            try
            {

                wsaa.LoginCMSService servicioWsaa = new wsaa.LoginCMSService();
                servicioWsaa.Url = argUrlWsaa;

                strLoginTicketResponse = servicioWsaa.loginCms(cmsFirmadoBase64);

            }
            catch (Exception excepcionAlInvocarWsaa)
            {
                throw new Exception("***Error invocando al servicio WSAA: ObtenerLoginTicketResponse: " + excepcionAlInvocarWsaa.Message);
            }


            // PASO 4: Analizo el Login Ticket Response recibido del WSAA
            try
            {
                XmlDocument XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(strLoginTicketResponse);

                this.UniqueId = Convert.ToUInt32(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
                this.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
                this.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
                this.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                this.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;
            }
            catch (Exception excepcionAlAnalizarLoginTicketResponse)
            {
                throw new Exception("***Error analizando respuesta del WSAA: ObtenerLoginTicketResponse: " + excepcionAlAnalizarLoginTicketResponse.Message);
            }

            return strLoginTicketResponse;
        }

    }

    #endregion

    #region class CertificadosX509Lib

    class CertificadosX509Lib
    {
        public bool VerboseMode = false;

        public byte[] FirmaBytesMensaje(byte[] argBytesMsg, X509Certificate2 argCertFirmante)
        {
            try
            {
                // Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms)
                ContentInfo infoCOntenido = new System.Security.Cryptography.Pkcs.ContentInfo(argBytesMsg);
                SignedCms cmsFirmado = new SignedCms(infoCOntenido);

                // Creo objeto CmsSigner que tiene las caracteristicas del firmante
                CmsSigner cmsFirmante = new CmsSigner(argCertFirmante);
                cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

                // Firmo el mensaje PKCS #7
                cmsFirmado.ComputeSignature(cmsFirmante);

                // Encodeo el mensaje PKCS #7.
                return (cmsFirmado.Encode());

            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception("***Error al firmar: FirmaBytesMensaje: " + excepcionAlFirmar.Message);
            }
        }


        public X509Certificate2 ObtieneCertificadoDesdeArchivo(string argArchivo)
        {
            try
            {
                X509Certificate2 objCert = new X509Certificate2(argArchivo, "");

                //objCert.Import(argArchivo);
                return objCert;
            }
            catch (Exception excepcionAlImportarCertificado)
            {
                throw new Exception("***Error al obtener certificado: ObtieneCertificadoDesdeArchivo(" + argArchivo + "): " + excepcionAlImportarCertificado.Message + " " + excepcionAlImportarCertificado.StackTrace);
            }
        }



    }

    #endregion



}
