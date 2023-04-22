namespace Pizzaria.Models
{
    public class Order
    {
        public const int DoubleCheesePrice = 2;
        public const int BeefPrice = 5;
        public const int TomatoPrice = 1;

        public int Id { get; set; }

        public int CurrentCustomerId { get; set; }

        public int PizzaId { get; set; }

        public bool IsDoubleCheese { get; set; }

        public bool IsTomato { get; set; }

        public bool IsBeef { get; set; }

        public Pizza Pizza { get; set; }

        public float TotalPrice
        {
            get
            {
                if (Pizza is null)
                {
                    return 0;
                }

                float total = Pizza.BasePrice;
                if (IsDoubleCheese)
                {
                    total += DoubleCheesePrice;
                }

                if (IsTomato)
                {
                    total += TomatoPrice;
                }

                if (IsBeef)
                {
                    total += BeefPrice;
                }

                return total;
            }
        }
    }
}
