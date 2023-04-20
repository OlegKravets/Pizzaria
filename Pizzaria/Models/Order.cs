namespace Pizzaria.Models
{
    public class Order
    {
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
                    total += 2;
                }

                if (IsTomato)
                {
                    total += 1;
                }

                if (IsBeef)
                {
                    total += 5;
                }

                return total;
            }
        }
    }
}
