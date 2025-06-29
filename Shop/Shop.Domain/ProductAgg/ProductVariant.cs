﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg
{
    public class ProductVariant : BaseEntity
    {
        public long ProductId { get; private set; }
        public string SKU {  get; private set; }
        public string? Color { get; private set; }
        public string? Size { get; private set; }
        public int StockQuantity { get; private set; }
        public decimal Price { get; private set; }
        public int? DiscountPercentage { get; private set; }

        public ProductVariantStatus Status { get; private set; }
        private ProductVariant() { } //EF Core

        private const decimal MinPrice = 5000;           // 5 هزار تومان
        private const decimal MaxPrice = 50000000;        // 50 میلیون تومان

        public ProductVariant(long productId, string? color, string? size, int stockQuantity, decimal price, int? discountPercentage,string sku,IProductDomainService domainService)
        {
            SetFields(productId, color, size, stockQuantity, price, discountPercentage, sku,  domainService);
            UpdateStatus();
        }

        public void Edit(string? color, string? size, int? stockQuantity, decimal? price, int? discountPercentage, string? sku, IProductDomainService domainService)
        {
                if (!string.IsNullOrWhiteSpace(color))
                    Color = color;

                if (!string.IsNullOrWhiteSpace(size))
                    Size = size;

            if (stockQuantity.HasValue)
            {
                StockQuantity = stockQuantity.Value;
                UpdateStatus();
            }

            if (price.HasValue)
            {
                ValidatePrice(price.Value);
                Price = price.Value;
            }

            DiscountPercentage = discountPercentage;

            if (!string.IsNullOrWhiteSpace(sku) && sku != SKU)
            {
                if (domainService.SKUIsExist(sku.Trim().ToUpperInvariant(), ProductId))
                    throw new InvalidDomainDataException("SKU تکراری است");
                SKU = sku.Trim().ToUpperInvariant();
            }

        }

        private void SetFields(long productId, string? color, string? size, int stockQuantity, decimal price, int? discountPercentage, string sku, IProductDomainService domainService)
        {
            if (stockQuantity < 0 || stockQuantity > 10000)
                throw new InvalidDomainDataException("موجودی نمی‌تواند منفی باشد");

            if (price < MinPrice || price > MaxPrice)
                throw new InvalidDomainDataException($"قیمت باید بین {MinPrice:N0} تا {MaxPrice:N0} تومان باشد");

            if (discountPercentage is < 0 or > 100)
                throw new InvalidDomainDataException("درصد تخفیف باید بین 0 تا 100 باشد");

            if (string.IsNullOrWhiteSpace(sku))
                throw new InvalidDomainDataException("کد SKU نمی‌تواند خالی باشد");

            ProductId = productId;
            Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim();
            Size = size;
            StockQuantity = stockQuantity;
            ValidatePrice(price);
            Price = price;
            DiscountPercentage = discountPercentage;

            if (domainService.SKUIsExist(sku.Trim().ToUpperInvariant(), productId))
                throw new InvalidDomainDataException("SKU تکراری است");
            SKU = sku.Trim().ToUpperInvariant();
           
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0 || quantity > 10000)
                throw new InvalidDomainDataException("تعداد باید بیشتر از صفر باشد");

            StockQuantity += quantity;
            UpdateStatus();
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidDomainDataException("تعداد باید بیشتر از صفر باشد");

            if (quantity > StockQuantity)
                throw new InvalidDomainDataException($"موجودی کافی نیست. موجودی فعلی: {StockQuantity}، مقدار درخواستی: {quantity}");


            StockQuantity -= quantity;

            UpdateStatus();
        }

        public decimal FinalPrice()
        {
            if (DiscountPercentage == null || DiscountPercentage == 0)
                return Price;

            var discount = (Price * DiscountPercentage.Value) / 100;
            return Price - discount;
        }

        public void ChangeVariantStatus(ProductVariantStatus status)
        {
            Status = status;
        }
        private void ValidatePrice(decimal price)
        {
            if (price < MinPrice || price > MaxPrice)
                throw new InvalidDomainDataException($"قیمت باید بین {MinPrice} تا {MaxPrice} تومان باشد.");
        }
        private void UpdateStatus()
        {
            if (StockQuantity <= 0)
                Status = ProductVariantStatus.OutOfStock;
            else if (StockQuantity<5)
                Status = ProductVariantStatus.LowStock;
            else
                Status = ProductVariantStatus.Active;
        }
    }
}
