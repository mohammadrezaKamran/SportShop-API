﻿using Common.Application;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Products.Create
{
    public class CreateProductCommand : IBaseCommand
    {
        public string Title { get; set;}
        public IFormFile ImageFile { get; set;}
		public string AltText { get;  set; }
		public string Description { get; set;}
        public long CategoryId { get; set;}
        public long? SubCategoryId { get; set;}
        public long? SecondarySubCategoryId { get; set;}
        public string Slug { get; set;}
        public SeoData SeoData { get; set;}
        public string BrandName { get; set; }
        public ProductStatus Status { get; set; }
        public Dictionary<string, string> Specifications { get; set;}
    }
}

