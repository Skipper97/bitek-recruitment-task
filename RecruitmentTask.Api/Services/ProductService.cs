using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using RecruitmentTask.Api.Models;
using RecruitmentTask.Data.DbContexts;
using System.Globalization;
using AutoMapper;

namespace RecruitmentTask.Api.Services
{
    public interface IProductService
    {
        ProductInfo? GetProductInfoBySku(string sku);
        Task ReadProductFilesToDatabase();
    }

    public class ProductService : IProductService
    {
        // DI
        private readonly RecruitmentTaskDbContext _dbContext; // EF DbContext
        private readonly IMapper _mapper; // AutoMapper
        private readonly IExternalFilesService _externalFilesService;

        public ProductService(RecruitmentTaskDbContext dbContext, IMapper mapper, IExternalFilesService externalFilesService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _externalFilesService = externalFilesService;
        }


        public ProductInfo? GetProductInfoBySku(string sku)
        {
            // Query db and cast results as ProductInfo obj
            var query =
                from product in _dbContext.Products
                join inventory in _dbContext.Inventories on product.Sku equals inventory.Sku into inv_pro
                from inv in inv_pro.DefaultIfEmpty()
                join prices in _dbContext.Prices on product.Sku equals prices.Sku into inv_prices
                from pri in inv_prices.DefaultIfEmpty()
                where product.Sku == sku
                select new ProductInfo()
                {
                    Name = product.Name,
                    Ean = product.Ean,
                    ProducerName = product.ProducerName,
                    Category = product.Category,
                    DefaultImage = product.DefaultImage,
                    Quantity = inv.Quantity ?? 0, // default Quantity field to 0 if no Inventory record exist
                    Unit = inv.Unit,
                    NetDiscountPricePerUnit = pri.NetDiscountPricePerUnit,
                    ShippingCost = inv.ShippingCost,
                };

            // Will return null if the product does not exist
            var result = query.FirstOrDefault();
            return result;
        }

        private async Task<Inventory[]> LoadInventory()
        {
            var file = await _externalFilesService.DownloadExternalFileToLocal("Inventory.csv");

            using var fileReader = new StreamReader(file.FullName);
            var csvReaderConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
            };
            using var csvReader = new CsvReader(fileReader, csvReaderConfig);

            var recordsEnumerable = csvReader.GetRecords<Inventory>();
            var recordsEnumerableFiltered =
                recordsEnumerable
                .Where(r => r.ShippingTime == "24h" || r.ShippingTime == "Wysyłka w 24h");
            var recordsList = recordsEnumerableFiltered.ToArray();


            // Clear table then add records to the database
            _dbContext.Inventories.ExecuteDelete();
            //_dbContext.Inventories.AddRange(_mapper.Map<IEnumerable<Data.Entities.Inventory>>(recordsEnumerableFiltered));
            _dbContext.Inventories.AddRange(_mapper.Map<Data.Entities.Inventory[]>(recordsList));
            //_dbContext.SaveChanges();

            return recordsList;
        }

        private async Task LoadProducts(Inventory[] inventory)
        {
            var file = await _externalFilesService.DownloadExternalFileToLocal("Products.csv");

            using var fileReader = new StreamReader(file.FullName);
            var csvReaderConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                MissingFieldFound = null,
                ShouldSkipRecord = args => args.Row.Parser.RawRecord.Contains("__empty_line__"),
            };
            using var csvReader = new CsvReader(fileReader, csvReaderConfig);

            var recordsEnumerable = csvReader.GetRecords<Product>();
            var inventorySkus = inventory.Select(i => i.Sku).ToHashSet();
            var recordsEnumerableFiltered =
                recordsEnumerable
                .Where(r => r.IsWire != 1)
                .Where(r => inventorySkus.Contains(r.Sku));

            // Clear table then add records to the database
            _dbContext.Products.ExecuteDelete();
            _dbContext.Products.AddRange(_mapper.Map<IEnumerable<Data.Entities.Product>>(recordsEnumerableFiltered));
            //_dbContext.SaveChanges();
        }

        private async Task LoadPrices()
        {
            var file = await _externalFilesService.DownloadExternalFileToLocal("Prices.csv");

            using var fileReader = new StreamReader(file.FullName);
            var csvReaderConfigCulture = CultureInfo.InvariantCulture.Clone() as CultureInfo;
            csvReaderConfigCulture!.NumberFormat.NumberDecimalSeparator = ",";
            var csvReaderConfig = new CsvConfiguration(csvReaderConfigCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false,
            };
            using var csvReader = new CsvReader(fileReader, csvReaderConfig);

            var recordsEnumerable = csvReader.GetRecords<Price>();

            // Clear table then add records to the database
            _dbContext.Prices.ExecuteDelete();
            _dbContext.Prices.AddRange(_mapper.Map<IEnumerable<Data.Entities.Price>>(recordsEnumerable));
            //_dbContext.SaveChanges();
        }

        public async Task ReadProductFilesToDatabase()
        {
            var inv = await LoadInventory();
            await LoadProducts(inv);
            await LoadPrices();


            await _dbContext.SaveChangesAsync();
        }
    }
}
