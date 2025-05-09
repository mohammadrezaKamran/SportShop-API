﻿using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.OrderAgg
{
    public class OrderItem : BaseEntity
    {
        public OrderItem(long productVariantId, int count, decimal price)
        {
            PriceGuard(price);
            CountGuard(count);

            ProductVariantId = productVariantId;
            Count = count;
            Price = price;
        }

        public long OrderId { get; internal set; }
        public long ProductVariantId { get; private set; }
        public int Count { get; private set; }
        public decimal Price { get; private set; }
        public decimal TotalPrice => (Int32)Price * Count;

        public void IncreaseCount(int count)
        {
            if (count <= 0)
                return;
            Count += count;
        }

        public void DecreaseCount(int count)
        {
            if (Count == 1)
                return;
            if (Count - count <= 0)
                return;

            Count -= count;
        }

        public void ChangeCount(int newCount)
        {
            CountGuard(newCount);

            Count = newCount;
        }

        public void SetPrice(int newPrice)
        {
            PriceGuard(newPrice);
            Price = newPrice;
        }

        public void PriceGuard(decimal newPrice)
        {
            if (newPrice < 1)
                throw new InvalidDomainDataException("مبلغ کالا نامعتبر است");
        }

        public void CountGuard(int count)
        {
            if (count < 1)
                throw new InvalidDomainDataException();
        }
    }
}