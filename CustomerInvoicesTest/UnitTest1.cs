using CustomerInvoices.Controllers;
using CustomerInvoices.Data;
using CustomerInvoices.DTOs.Requests;
using CustomerInvoices.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerInvoicesTest
{
    [TestFixture]
    public class InvoiceControllerTests
    {
        [Test]
        public void GetAllInvoices_ReturnsOkResultWithListOfInvoices()
        {
            var invoices = new List<Invoice>
    {
        new Invoice { InvoiceId = 1, CustomerId = 1, ServiceId = 1, InvoiceDate = DateTime.Now, TotalAmount = 100.0F }, 
        new Invoice { InvoiceId = 2, CustomerId = 2, ServiceId = 2, InvoiceDate = DateTime.Now, TotalAmount = 150.0F } 
    };

            var mockDbContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<Invoice>>();
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoices.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoices.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoices.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(invoices.AsQueryable().GetEnumerator());

            mockDbContext.Setup(m => m.Invoices).Returns(mockDbSet.Object);

            var controller = new InvoiceController(mockDbContext.Object);

           
            var result = controller.GetAllInvoices();

           
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var resultInvoices = okResult.Value as List<InvoiceResponseDTO>;
            Assert.IsNotNull(resultInvoices);

            Assert.AreEqual(invoices.Count, resultInvoices.Count);

            for (int i = 0; i < invoices.Count; i++)
            {
                Assert.AreEqual(invoices[i].InvoiceId, resultInvoices[i].InvoiceId);
                Assert.AreEqual(invoices[i].CustomerId, resultInvoices[i].CustomerId);
                Assert.AreEqual(invoices[i].ServiceId, resultInvoices[i].ServiceId);
                Assert.AreEqual(invoices[i].InvoiceDate, resultInvoices[i].InvoiceDate);
                Assert.AreEqual(invoices[i].TotalAmount, resultInvoices[i].TotalAmount);
            }
        }

        [Test]
        public void GetInvoiceById_ExistingId_ReturnsOkResultWithInvoice()
        {
            
            var invoice = new Invoice { InvoiceId = 1, CustomerId = 1, ServiceId = 1, InvoiceDate = DateTime.Now, TotalAmount = 100.0F };
            var mockDbContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<Invoice>>();
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(new List<Invoice> { invoice }.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(new List<Invoice> { invoice }.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(new List<Invoice> { invoice }.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(new List<Invoice> { invoice }.AsQueryable().GetEnumerator());

            mockDbContext.Setup(m => m.Invoices).Returns(mockDbSet.Object);

            var controller = new InvoiceController(mockDbContext.Object);

            
            var result = controller.GetInvoiceById(1);

            
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var resultInvoice = okResult.Value as InvoiceResponseDTO;
            Assert.IsNotNull(resultInvoice);

            Assert.AreEqual(invoice.InvoiceId, resultInvoice.InvoiceId);
            Assert.AreEqual(invoice.CustomerId, resultInvoice.CustomerId);
            Assert.AreEqual(invoice.ServiceId, resultInvoice.ServiceId);
            Assert.AreEqual(invoice.InvoiceDate, resultInvoice.InvoiceDate);
            Assert.AreEqual(invoice.TotalAmount, resultInvoice.TotalAmount);
        }
    }
}
