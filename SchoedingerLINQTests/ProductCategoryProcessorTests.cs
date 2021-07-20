using NUnit.Framework;
using SchoedingerLINQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoedingerLINQ.Tests
{


    [TestFixture()]
    public class ProductCategoryProcessorTests
    {
        private Product[] _products;
        private Category[] _categories;

        [SetUp]
        public void Init()
        {

            _products = new Product[] {
                new Product() {Name = "Ruebli", CategoryId=2},
                new Product() {Name = "Salat", CategoryId=2},
                new Product() {Name = "WC-Reiniger", CategoryId=1},
                new Product() {Name = "Rindfleisch", CategoryId=3},
                new Product() {Name = "Sonnenschirm", CategoryId=4}
            };
            _categories = new Category[] {
                new Category() {Id = 1, Name ="Haushaltreiniger"},
                new Category() {Id = 2, Name ="Gemüse"},
                new Category() {Id = 3, Name ="Fleisch"}
            };
        }
        [Test()]
        public void IterateCategoriesWithTheirProductsTest()
        {
            //Arrange
            ProductCategoryProcessor productCategoryProcessor = new ProductCategoryProcessor(_products, _categories);


            //Act
            var CategoriesWithTheirProducts = productCategoryProcessor.IterateCategoriesWithTheirProducts();

            //Assert
            
            foreach (var item in CategoriesWithTheirProducts)
            {
                TestContext.WriteLine($"Kategorie: {item.CategoryName} ");
                foreach (Product p in item.ProductInfos)
                {
                    TestContext.WriteLine($"\tProdukt: {p.Name} ");
                }
            }
            Assert.IsTrue(true);
        }
        [Test()]
        public void GetLeftJoinProductsCategoryTest()
        {
            //Arrange
            ProductCategoryProcessor productCategoryProcessor = new ProductCategoryProcessor(_products, _categories);


            //Act
            var productWithCategories = productCategoryProcessor.GetLeftJoinProductsCategory();

            //Assert

            foreach (var item in productWithCategories)
            {
                TestContext.WriteLine($"Product: {item.ProductName} " +
                    $"Kategorie: {item.CategoryId}  {item.CategoryName} ");
            }
            Assert.IsTrue(true);
        }
    }
}