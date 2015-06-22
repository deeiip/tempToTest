using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoWipe.types
{
    public interface IStorageAdapter
    {
        void StoreProductType(List<Product> products);
        List<ProductAttributes> GetAttributes(string forProductName);
        List<string> GetProductDesc(string productType);
        void StoreProduct(string productType, string productDesc);
    }
    public class AzureStorageAdapter : IStorageAdapter
    {
        public void StoreProductType(List<Product> products)
        {
            using (var context = new heEntities())
            {
                foreach (var item in products)
                {
                    var res = from e in context.ProductPrototypes where e.product_type == item.type select e;
                    if(res.Count()==0)
                    {
                        context.ProductPrototypes.Add(new ProductPrototype()
                        {
                            product_type = item.type,
                            product_attr = item.attributeString()
                        });
                        context.SaveChanges();
                    }
                    else
                    {
                        var target = res.First();
                        target.product_attr = item.attributeString() ;
                        context.SaveChanges();
                    }
                }
            }
        }



        public List<ProductAttributes> GetAttributes(string forProductName)
        {
            List<ProductAttributes> ret = new List<ProductAttributes>();
            using(var context = new heEntities())
            {
                var res = from e in context.ProductPrototypes where e.product_type == forProductName select e;
                if(res.Count()!=0)
                {
                    ret = JsonConvert.DeserializeObject<List<ProductAttributes>>(res.First().product_attr.Trim());
                }
            }
            return ret;
        }


        public void StoreProduct(string productType, string productDesc)
        {
            using (var context = new heEntities())
            {
                context.ActualProducts.Add(new ActualProduct()
                {
                    product_type = productType,
                    product_desc = productDesc
                });
                context.SaveChanges();
            }
        }



        public List<string> GetProductDesc(string productType)
        {
            List<string> ret = new List<string>();
            using (var context = new heEntities())
            {
                var res = from p in context.ActualProducts where p.product_type == productType select p.product_desc;
                foreach (var item in res)
                {
                    ret.Add(item);
                }
            }
            return ret;
        }
    }
}
