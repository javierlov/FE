using System;
using FacturaElectronica.Utils;
using FacturaElectronica.WSPrint.Reportes.Sprayette;

namespace FacturaElectronica.PrintEngine
{
    public class DocumentoFactory
    {
        /// <summary>
        /// Report Factory: Decide que instancia debe devolver, en base a Empresa y Tipo de Reporte.
        /// </summary>
        /// <param name="tipoDoc"></param>
        /// <returns></returns>
        public static ReporteBase GetReportInstance(Empresa oEmpresa , TipoDocumento tipoDoc)
        {
            switch (Convert.ToInt32(oEmpresa.EmpresaID))
            {
                default:
                    switch (tipoDoc)
                    {
                        //Agregar -si aplica- reportes específicos por TipoDoc.
                        default:
                            return new Sprayette();
                            //return new ReporteBase();
                    }
            }
        }

    }
}