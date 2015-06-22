using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoWipe.Controllers;

namespace GoWipe.Tests.Controllers
{
    [TestClass]
    public class TestWithV1Def
    {
        [TestMethod]
        public void DoTestWithv1()
        {
            ProductTypeController pt = new ProductTypeController();
            ProductController p = new ProductController();
            pt.Post("http://si-hacker-products.azurewebsites.net/Productdefinition/v1/json");
            // this version do not expect ISBN for books
            p.Put("book", Utility.GenerateAValidBookWithoutISBN());
            p.Get("book");
            //the calculated property is present
            try
            {
                p.Put("book", Utility.GenerateAValidBookOutRange());
                Assert.Fail("Should have thrown exception. Invalid Data was passed.");
            }
            catch(Ex.InavlidData)
            {
            }
        }
    }
}
