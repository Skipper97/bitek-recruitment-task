CREATE TABLE [dbo].[Prices]
(
	[InternalId] VARCHAR(30) NOT NULL,
	[Sku] VARCHAR(50) NOT NULL, 
    [NetPrice] DECIMAL(14, 2) NULL, 
    [NetDiscountPrice] DECIMAL(14, 2) NULL, 
    [NetDiscountPricePerUnit] DECIMAL(14, 2) NULL, 

    CONSTRAINT PK_Prices PRIMARY KEY (InternalId), 
    CONSTRAINT UQ_Prices_Sku UNIQUE (Sku)
)
