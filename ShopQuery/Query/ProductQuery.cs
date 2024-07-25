using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using LampshadeQuery.Contracts.Product;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext shopContext;
        private readonly InventoryContext inventoryContext;
        private readonly DiscountContext discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            this.shopContext = shopContext;
            this.inventoryContext = inventoryContext;
            this.discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals(int number)
        {
            //Inventory Report
            var ProductQty = inventoryContext.Inventories.Select(x => new { x.ProductId, x.CurrentCount });

            //LIST OF DEFINED PRICES OF EACH PRODUCT
            var prices = inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice });

            //LIST OF ACTIVE DISCOUNTS
            var discounts = discountContext.CustomerDiscounts
                .Where(x => x.EndDate > DateTime.Now && x.StartDate <= DateTime.Now)
                .Select(x => new { EndDate = x.EndDate, DiscountPercentage = x.DiscountPercentage, ProductId = x.ProductId })
                .AsNoTracking();

            //LIST OF ALL PRODUCTS
            var products = shopContext.Products.Include(x => x.Category).Select(x => new ProductQueryModel
            {
                Slug = x.Slug,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Picture = x.Picture,
                CategoryName = x.Category.Name,
                CategorySlug = x.Category.Slug,
                Name = x.Name,
                Id = x.Id,
                IsNew = DateTime.Now.Subtract(x.CreationDateTime).Days < 30
            }).AsNoTracking().ToList();

            //ITERATING TO JOIN
            foreach (var product in products)
            {
                var price = prices.FirstOrDefault(x => x.ProductId == product.Id);
                product.Price = (price == null ? 0 : price.UnitPrice);
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                product.DiscountPercentage = discount == null ? 0 : discount.DiscountPercentage;
                product.Quantity = ProductQty.FirstOrDefault(x => x.ProductId == product.Id)?.CurrentCount ?? 0;
            }

            return products.OrderByDescending(x => x.Id).Take(number).ToList();
        }

        public ProductQueryModel GetProductBySlug(string slug)
        {
            var product = shopContext.Products.Include(x => x.ProductPictures).Include(x => x.Category).FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return null;

            //LIST OF DEFINED PRICES OF EACH PRODUCT
            var inventory = inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == product.Id);

            //LIST OF ACTIVE DISCOUNTS
            var discount = discountContext.CustomerDiscounts
                .Where(x => x.EndDate > DateTime.Now && x.StartDate <= DateTime.Now).FirstOrDefault(x => x.ProductId == product.Id);

            return new ProductQueryModel
            {
                Id = product.Id,
                Price = inventory?.UnitPrice ?? 0,
                Slug = product.Slug,
                PictureTitle = product.PictureTitle,
                PictureAlt = product.PictureAlt,
                Picture = product.Picture,
                OtherPictures = product.ProductPictures.Select(x => x.Picture).ToList(),
                CategoryName = product.Category.Name,
                Code = product.Code,
                CategorySlug = product.Category.Slug,
                Name = product.Name,
                IsNew = DateTime.Now.Subtract(product.CreationDateTime).Days < 30,
                Quantity = inventory?.CurrentCount ?? 0,
                Description = product.Description,
                DiscountPercentage = discount?.DiscountPercentage ?? 0,
                DiscountEndDate = discount?.EndDate.ToString() ?? DateTime.Now.ToString(),
                ShortDescription = product.ShortDescription
            };
        }
    }
}
