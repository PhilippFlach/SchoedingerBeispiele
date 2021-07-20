using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoedingerLINQ
{
    public class ProductCategoryProcessor
    {
        public Product[] Products { get; set; }

        public Category[] Categories { get; set; }

        public ProductCategoryProcessor(Product[] products, Category[] categories)
        {
            Products = products ?? throw new ArgumentNullException(nameof(products));
            Categories = categories ?? throw new ArgumentNullException(nameof(categories));
        }

        /// <summary>
        /// Der Group Join sieht eigentlich aus wie der normale Join
        /// nur dass das IEnumerable-Ergebnis nicht aus genau zwei Elementen, sondern 
        /// aus einem Element und einem IEnumerable von Elementen besteht
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryWithTheirProducts> IterateCategoriesWithTheirProducts()
        {
            IEnumerable<CategoryWithTheirProducts> result1 = Categories.GroupJoin(Products, c => c.Id, p => p.CategoryId,
                (c, productInfos) => new CategoryWithTheirProducts
                {
                    CategoryName = c.Name,
                    ProductInfos = productInfos
                });

            return result1;
        }
        /// <summary>
        /// Für den Left-Join zuerst gruppieren und 
        /// dann mit SelectMany neu heraussuchen
        /// </summary>
        public IEnumerable<ProductWithCategory> GetLeftJoinProductsCategory()
        {
            IEnumerable<ProductWithCategory> result2 = Products.GroupJoin(Categories,
                p => p.CategoryId,
                c => c.Id,
                (x, y) => new { product = x, cat = y })
                .SelectMany(x => x.cat.DefaultIfEmpty(new Category()),
                (x, y) => new ProductWithCategory
                {
                    CategoryName = y.Name,
                    CategoryId = y.Id,
                    ProductName = x.product.Name
                });
            return result2;
        }


    }
}
