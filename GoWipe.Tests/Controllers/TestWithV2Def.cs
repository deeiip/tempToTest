using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoWipe.Controllers;

namespace GoWipe.Tests.Controllers
{
    [TestClass]
    public class TestWithV2Def
    {
        [TestMethod]
        public void DoTestWithv2()
        {
            ProductTypeController pt = new ProductTypeController();
            ProductController p = new ProductController();
            pt.Post("http://si-hacker-products.azurewebsites.net/Productdefinition/v2/json");
            // this version makes ISBN mandetory

            p.Put("book", Utility.GenerateAValidBookWithISBN());
            try
            {
                p.Put("book", Utility.GenerateAValidBookWithoutISBN());
                Assert.Fail("Should have thrown exception. Invalid Data was passed.");
            }
            catch(Ex.InavlidData)
            {
                try
                {
                    p.Put("book", Utility.GenerateAValidBookOutRange());
                    Assert.Fail("Should have thrown exception. Invalid Data was passed.");
                }
                catch(Ex.InavlidData)
                {
                    p.Get("book");
                    //the calculated property is present
                }

            }
            //ProductTypeController p = new ProductTypeController();
            //p.Post("http://si-hacker-products.azurewebsites.net/Productdefinition/v2/json");
        }
    }
}
