-- Are both product_id and sku unique in all tables
/*
	truncate table dbo.Products
	truncate table dbo.Inventory
	truncate table dbo.Prices
*/

select count(*) from dbo.Products	-- 379514	372331	91285
select count(*) from dbo.Inventory	-- 680250	97300	97300
select count(*) from dbo.Prices		-- 462699	462699	462699


select top 10 pro.Sku, inv.ShippingTime
from dbo.Products pro
left join dbo.Prices pri on pri.Sku = pro.Sku
left join dbo.Inventory inv on inv.Sku = pro.Sku
where pri.Sku is null


select top 10 * from dbo.Inventory i where i.Quantity <> round(i.Quantity, 0)


select 
		pro.Id
	,	pro.Sku
	,	pro.Name
	,	pro.ProducerName
	,	inv.Manufacturer
from dbo.Products pro
join dbo.Inventory inv on inv.Sku = pro.Sku
where pro.ProducerName <> inv.Manufacturer




------------------------------------------------------------

declare @sku varchar(50) = '1131-191AA-OB034';
--declare @sku varchar(50) = '0001-00000-24016';

select 
		pro.Name
	,	pro.Ean
	,	pro.ProducerName
	,	pro.Category
	,	pro.DefaultImage
	,	isnull(inv.Quantity, 0) as Quantity
	,	inv.Unit
	,	pri.NetDiscountPricePerUnit
	,	inv.ShippingCost
from		dbo.Products	pro
left join	dbo.Inventory	inv on inv.Sku = pro.Sku
left join	dbo.Prices		pri	on pri.Sku = pro.Sku
where pro.Sku = @sku

