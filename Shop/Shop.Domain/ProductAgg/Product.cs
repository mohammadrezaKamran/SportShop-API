using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Shop.Domain.ProductAgg.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductAgg
{
    public class Product : AggregateRoot
    {
        private Product()
        {
        }

        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string Description { get; private set; }
        public long CategoryId { get; private set; }
        public long? SubCategoryId { get; private set; }
        public long? SecondarySubCategoryId { get; private set; }
        public string Slug { get; private set; }
        public bool IsSpecial { get; set; } = false;
		public SeoData SeoData { get; private set; }
        public string BrandName { get; private set; }
        public List<ProductVariant> ProductVariants { get; private set; } = new List<ProductVariant>();
        public ProductStatus Status { get; private set; }
        public List<ProductImage> Images { get; private set; }
        public List<ProductSpecification> Specifications { get; private set; }

        public Product(string title, string imageName, string description, long categoryId,
           long? subCategoryId, long? secondarySubCategoryId, IProductDomainService domainService,
           string slug, SeoData seoData, string brandName, ProductStatus status)
        {
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            Guard(title, slug, description, brandName, domainService);

            Title = title;
            ImageName = imageName;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SecondarySubCategoryId = secondarySubCategoryId;
            Slug = slug.ToSlug();
            SeoData = seoData;
            BrandName = brandName;
            Status = status;
        }

		public void MarkAsSpecial() => IsSpecial = true;
		public void UnmarkAsSpecial() => IsSpecial = false;

		public void AddVariant(ProductVariant variant)
        {
            ProductVariants.Add(variant);

            if(Status == ProductStatus.Draft)
                Status = ProductStatus.Published;
        }

        public void EditVariant(long variantId, string? color, string? size, int? stock,
              decimal? price, int? discount, string? sku, IProductDomainService domainService)
        {
            var variant = ProductVariants.FirstOrDefault(v => v.Id == variantId);
            if (variant == null)
                throw new NullOrEmptyDomainDataException("تنوع محصول یافت نشد");

            variant.Edit(color, size, stock, price, discount, sku, domainService);
        }
        public void RemoveVariant(long variantId)
        {
            var variant = ProductVariants.FirstOrDefault(v => v.Id == variantId);
            if (variant == null)
                throw new NullOrEmptyDomainDataException("تنوع محصول یافت نشد");

            ProductVariants.Remove(variant);
        }


        public void Edit(string title, string description, long categoryId, string brandName,
            long? subCategoryId, long? secondarySubCategoryId, string slug, IProductDomainService domainService
            , SeoData seoData, ProductStatus status)
        {
            Guard(title, slug, description, brandName, domainService);
            Title = title;
            Description = description;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            SecondarySubCategoryId = secondarySubCategoryId;
            Slug = slug.ToSlug();
            SeoData = seoData;
            BrandName = brandName;
            Status = status;
        }

        public void SetProductImage(string imageName)
        {
            NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
            ImageName = imageName;
        }

        public void AddImage(ProductImage image)
        {
            image.ProductId = Id;
            Images.Add(image);
        }

        public string RemoveImage(long id)
        {
            var image = Images.FirstOrDefault(f => f.Id == id);
            if (image == null)
                throw new NullOrEmptyDomainDataException("عکس یافت نشد");

            Images.Remove(image);
            return image.ImageName;
        }

        public void SetSpecification(List<ProductSpecification> specifications)
        {
            specifications.ForEach(s => s.ProductId = Id);
            Specifications = specifications;
        }

		private void Guard(string title, string slug, string description, string brandName,
            IProductDomainService domainService)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(description, nameof(description));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
            NullOrEmptyDomainDataException.CheckString(brandName, nameof(brandName));

            if (slug != Slug)
                if (domainService.SlugIsExist(slug.ToSlug()))
                    throw new SlugIsDuplicateException();
        }
    }
}
