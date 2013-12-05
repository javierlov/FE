INSERT INTO AFIPCodigoDocumento(EmpresaID,CodigoEmpresa,CodigoAFIP)
SELECT EmpresaID,CodigoEmpresa,CodigoAFIP FROM EquivAFIPCodigoDocumento
ORDER BY ID ASC