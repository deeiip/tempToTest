using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoWipe.types
{
    public enum ExpressionType
    {
        AttributeReference,
        Constant,
        operation
    }
    public class Operand
    {
        public ExpressionType type { get; set; }
        public string value { get; set; }
    }


    public enum ProductAttributeType
    {
        TEXT,
        NUMERIC,
        BOOLEAN,
        CALCULATED
    }
    public class Expression
    {
        public ExpressionType type { get; set; }
        public List<Operand> operands { get; set; }
        private string _op = "+";
        public string op { get { return _op; } set { _op = value; } }
    }
    public class ProductAttributes
    {
        public ProductAttributeType type { get; set; }
        public string name { get; set; }
        public bool mandatory { get; set; }
        public int minValue { get; set; }
        public int maxValue { get; set; }
        public List<string> validValues { get; set; }
        public Expression expression { get; set; }

        public override string ToString()
        {
            var ret = JsonConvert.SerializeObject(this);
            return ret;
        }
        public static ProductAttributes Parse(string str)
        {
            var ret = JsonConvert.DeserializeObject<ProductAttributes>(str);
            return ret;
        }
    }
    public class Product
    {
        public string type { get; set; }
        public List<ProductAttributes> attributes { get; set; }

        public string attributeString()
        {
            string ret = JsonConvert.SerializeObject(attributes);
            return ret;
        }
    }
}
