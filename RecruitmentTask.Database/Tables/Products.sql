CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL, 
    [Sku] VARCHAR(50) NOT NULL, 
    [Name] NVARCHAR(300) NULL, 
    [Ean] VARCHAR(50) NULL, 
    [ProducerName] NVARCHAR(300) NULL, 
    [Category] NVARCHAR(300) NULL, 
    [IsWire] BIT NOT NULL DEFAULT 0,
    [Available] BIT NOT NULL DEFAULT 0,
    [IsVendor] BIT NOT NULL DEFAULT 0, 
    [DefaultImage] NVARCHAR(300) NULL,

    CONSTRAINT PK_Products PRIMARY KEY (Id), 
    CONSTRAINT UQ_Products_Sku UNIQUE (Sku)
)
