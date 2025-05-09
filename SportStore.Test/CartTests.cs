using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Models;

namespace SportStore.Test
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewLines()
        {
            // Arrange - chuẩn bị dữ liệu
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart cart = new Cart();

            //Action - hành động test
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            CartLine[] results = cart.Lines.ToArray();

            // Assert - kiểm tra kết quả
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
            Assert.Equal(1, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void CanAddQuantityForExistingLines()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart cart = new Cart();

            // Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 3);
            CartLine[] results = cart.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(4, results[0].Quantity); // p1: 1 + 3
            Assert.Equal(1, results[1].Quantity); // p2: 1
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void CanRemoveLine()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 25M };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);

            // Action
            cart.RemoveLine(p2); // remove p2

            // Assert
            Assert.Equal(2, cart.Lines.Count); // p1 and p3 remaining
            Assert.Empty(cart.Lines.Where(c => c.Product.ProductID == p2.ProductID));
            Assert.Equal(p1, cart.Lines.First().Product);
            Assert.Equal(p3, cart.Lines.Last().Product);
        }

        [Fact]
        public void CalculateCartTotal()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);

            // Action
            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.Equal(200M, result); // 100 * 1 + 50 * 2 = 200
        }

        [Fact]
        public void CanClearContents()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);

            // Act
            cart.Clear();

            // Assert
            Assert.Empty(cart.Lines);
        }

        [Fact]
        public void AddingZeroQuantityDoesNotChangeCart()
        {
            // Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Cart cart = new Cart();

            // Act
            cart.AddItem(p1, 0);
            CartLine[] results = cart.Lines.ToArray();

            // Assert
            Assert.Empty(results);
        }
    }
}
