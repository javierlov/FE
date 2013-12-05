IF  EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[AFIPImpuesto]') 
	AND type in (N'U'))
DROP TABLE [dbo].[AFIPImpuesto]

Go

CREATE TABLE [dbo].[AFIPImpuesto]
(
	Id [int] NULL,
	CodigoAFIP [nvarchar](100) NULL,
	Descripcion [nvarchar](400) NULL
) ON [PRIMARY]
Go

select * from AFIPImpuesto