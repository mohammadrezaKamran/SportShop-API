﻿using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.Edit
{
    public class EditProductCommand : IBaseCommand
    {
		public EditProductCommand(long productId, string title, IFormFile? imageFile, string description, long categoryId,
			long? subCategoryId, ProductStatus status, long? secondarySubCategoryId, string slug, SeoData seoData, string brandName, Dictionary<string, string> specifications, string altText)
		{
			ProductId = productId;
			Title = title;
			ImageFile = imageFile;
			Description = description;
			CategoryId = categoryId;
			SubCategoryId = subCategoryId;
			SecondarySubCategoryId = secondarySubCategoryId;
			Slug = slug;
			SeoData = seoData;
			BrandName = brandName;
			Specifications = specifications;
			Status = status;
			AltText = altText;
		}

		public long ProductId { get; private set; }
        public string Title { get; private set; }
        public IFormFile? ImageFile { get; private set; }
		public string AltText { get; set; }
		public string Description { get; private set; }
        public long CategoryId { get; private set; }
        public long? SubCategoryId { get; private set; }
        public long? SecondarySubCategoryId { get; private set; }
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public string BrandName {  get; private set; }
        public Dictionary<string, string> Specifications { get; private set; }
        public ProductStatus Status { get; private set; }
    }
}