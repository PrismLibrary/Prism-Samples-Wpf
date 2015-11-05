

namespace StockTraderRI.Modules.Position.Orders
{
    public class ValueDescription<T> where T: struct
    {
        public ValueDescription()
        {
        }

        public ValueDescription(T value, string description)
        {
            Value = value;
            Description = description;
        }
        public T Value { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return Description;
        }
    }
}