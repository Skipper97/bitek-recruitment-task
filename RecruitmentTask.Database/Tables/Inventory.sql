CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL, 
    [Sku] VARCHAR(50) NOT NULL, 
    [Unit] VARCHAR(50) NULL, 
    [Quantity] DECIMAL(14, 3) NULL, 
    [Manufacturer] NVARCHAR(300) NULL, 
    [ShippingTime] NVARCHAR(50) NULL, 
    [ShippingCost] DECIMAL(14, 2) NULL, 

    CONSTRAINT PK_Inventory PRIMARY KEY (Id), 
    CONSTRAINT UQ_Inventory_Sku UNIQUE (Sku)
)
