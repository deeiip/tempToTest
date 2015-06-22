using GoWipe.types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace GoWipe.Controllers
{
    public class ProductTypeController : ApiController
    {
        

        // POST: api/ProductType
        // This creates, updates product defination
        public void Post([FromBody]string uriOfProductDescription)
        {
            try
            {
                var json = new WebClient().DownloadString(uriOfProductDescription);
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);

                IStorageAdapter st = new AzureStorageAdapter();
                st.StoreProductType(products);
            }
            catch(Exception)
            {
                // networkError
            }
        }

    }
}
