using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.ValueObjects;
using Common.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.OrderAgg.Events;

namespace Shop.Domain.OrderAgg
{
    public class Order : AggregateRoot
    {
        private Order()
        {
        }

        public Order(long userId)
        {
            UserId = userId;
            Status = OrderStatus.Pending;
            Items = new List<OrderItem>();
        }

        public long UserId { get; private set; }
        public OrderStatus Status { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public OrderAddress? Address { get; private set; }
        public OrderShippingMethod? ShippingMethod { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime? LastUpdate { get; private set; }

		public string? OrderNumber { get; private set; }
        public string? TrackingNumber {  get; private set; }

		public decimal TotalPrice
        {
            get
            {
                var totalPrice = Items.Sum(f => f.TotalPrice);
                if (ShippingMethod != null)
                    totalPrice += ShippingMethod.ShippingCost;

                if (Discount != null)
                    totalPrice -= Discount.DiscountAmount;

                return totalPrice;
            }
        }

        public int ItemCount => Items.Count;

        public void AddItem(OrderItem item)
        {
            ChangeOrderGuard();

            var oldItem = Items.FirstOrDefault(f => f.ProductVariantId == item.ProductVariantId);
            if (oldItem != null)
            {
                oldItem.ChangeCount(item.Count + oldItem.Count);
                return;
            }
            Items.Add(item);

			UpdateLastModified();
		}

        public void RemoveItem(long itemId)
        {
            ChangeOrderGuard();

            var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
            if (currentItem != null)
                Items.Remove(currentItem);

			UpdateLastModified();
		}

        public void IncreaseItemCount(long itemId, int count, int availableStock)
        {
            ChangeOrderGuard();

            var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
            if (currentItem == null)
                throw new NullOrEmptyDomainDataException();

			var newCount = currentItem.Count + count;
			if (newCount > availableStock)
				throw new InvalidDomainDataException("تعداد بیشتر از موجودی است.");

			currentItem.IncreaseCount(count);
			UpdateLastModified();
		}

        public void DecreaseItemCount(long itemId, int count)
        {
            ChangeOrderGuard();

            var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
            if (currentItem == null)
                throw new NullOrEmptyDomainDataException();

            currentItem.DecreaseCount(count);
			UpdateLastModified();
		}

        public void ChangeCountItem(long itemId, int newCount)
        {
            ChangeOrderGuard();

            var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
            if (currentItem == null)
                throw new NullOrEmptyDomainDataException();

            currentItem.ChangeCount(newCount);
			UpdateLastModified();
		}

        public void Finally(string TextForInvoice)
        {
            if (!Items.Any())
                throw new InvalidDomainDataException("سفارشی بدون آیتم نمی‌تواند ثبت نهایی شود");
        
			GenerateOrderNumber(TextForInvoice);

			Status = OrderStatus.Finally;
			UpdateLastModified();

			AddDomainEvent(new OrderFinalized(Id));
        }
		public void ChangeStatus(OrderStatus status)
		{
            if (status == OrderStatus.Shipping)
                throw new InvalidDomainDataException("بعد از ثبت کد رهگیری به صورت اتوماتیک به این وضعیت تغییر میکند");

			Status = status;
			LastUpdate = DateTime.UtcNow;
			UpdateLastModified();
		}

		public void SetAddressAndShippingMethod(OrderAddress orderAddress,OrderShippingMethod shippingMethod)
        {
            ChangeOrderGuard();

            Address = orderAddress;
            ShippingMethod = shippingMethod;
			UpdateLastModified();
		}


        public void ClearItems()
        {
            ChangeOrderGuard();
            Items.Clear();
			UpdateLastModified();
		}

        private void GenerateOrderNumber(string TextForInvoice)
        {
			if (string.IsNullOrEmpty(TextForInvoice))
				throw new NullOrEmptyDomainDataException();

			if (OrderNumber != null)
				throw new NullOrEmptyDomainDataException("شماره فاکتور قبلا ثبت شده");

			OrderNumber = $"{TextForInvoice.Trim()}-{TextHelper.GenerateCode(5)}";

		}
		public void SetTrackingNumber(string trackingNumber)
		{

			if (string.IsNullOrWhiteSpace(trackingNumber))
				throw new NullOrEmptyDomainDataException("کد رهگیری نمی‌تواند خالی باشد.");

			if (Status == OrderStatus.Completed)
				throw new InvalidDomainDataException("سفارش تکمیل‌شده قابل تغییر نیست");

			TrackingNumber = trackingNumber.Trim();
            Status = OrderStatus.Shipping;
			UpdateLastModified();
		}
		public void SetDiscount(OrderDiscount discount)
		{
			ChangeOrderGuard();
			Discount = discount;
			UpdateLastModified();
		}
		private void UpdateLastModified()
		{
			LastUpdate = DateTime.UtcNow;
		}
		public void ChangeOrderGuard()
		{
			if (Status != OrderStatus.Pending)
				throw new InvalidDomainDataException("امکان ویرایش این سفارش وجود ندارد");
		}
	}
}