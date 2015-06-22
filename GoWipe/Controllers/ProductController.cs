using GoWipe.types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GoWipe.Controllers
{

    public class ProductController : ApiController
    {


        // GET: api/Product/book
        public string Get(string productType)
        {
            IStorageAdapter sa = new AzureStorageAdapter();
            var Descriptions = sa.GetProductDesc(productType);
            var attrs = sa.GetAttributes(productType);
            string retStr = "";
            foreach (var desc_str in Descriptions)
            {
                string calcStr = "";
                dynamic deserializedValue = JsonConvert.DeserializeObject(desc_str);
                foreach (var item in attrs)
                {
                    try
                    {

                        if (item.expression.type == ExpressionType.operation)
                        {
                            var oper = item.expression.operands;
                            string ExprStr = "";
                            foreach (var oprnds in oper)
                            {
                                if (oprnds.type == ExpressionType.AttributeReference)
                                {
                                    ExprStr += deserializedValue[oprnds.value];
                                }
                                else if (oprnds.type == ExpressionType.Constant)
                                {
                                    ExprStr += oprnds.value;
                                }
                            }
                            calcStr ="\"" + item.name + "\"" + ":" + "\"" + ExprStr + "\"";
                        }
                    }
                    catch (Exception)
                    {
                        // I know what I am doing. Pass
                    }
                }
                string _tmp = desc_str.TrimEnd('}');
                retStr += (_tmp + "," + calcStr);
                retStr += "}";
            }
            return retStr;
        }



        public void Put(string name, [FromBody]string value)
        {
            IStorageAdapter storageA = new AzureStorageAdapter();
            List<ProductAttributes> reqAttrs = storageA.GetAttributes(name);
            dynamic deserializedValue = JsonConvert.DeserializeObject(value);



            // make sure all mandetory attributes exists
            foreach (var item in reqAttrs)
            {
                try
                {
                    if(item.mandatory==true)
                    {
                        var toCheckIfExist = deserializedValue[item.name];
                        if(toCheckIfExist==null)
                        {
                            throw new Ex.InavlidData();
                        }
                    }
                }
                catch(Exception)
                {
                    // A mandetory field do not esists
                    throw new Ex.InavlidData();
                }
            }


            // If there is a price range. Make sure it is held

            foreach (var item in reqAttrs)
            {
                if((item.minValue!=0)&&(item.maxValue!=0))
                {
                    try
                    {
                        double d = deserializedValue[item.name];
                        if (d < item.minValue || d > item.maxValue)
                        {
                            throw new Ex.InavlidData();
                        }
                    }
                    catch(Exception)
                    {
                        throw new Ex.InavlidData();
                        //invalid value
                    }
                }
            }

            // Now Data is valid, store it
            //string values = deserializedValue["Title"];
            IStorageAdapter storage = new AzureStorageAdapter();
            storage.StoreProduct(name, value);
            
        }

    }
}
