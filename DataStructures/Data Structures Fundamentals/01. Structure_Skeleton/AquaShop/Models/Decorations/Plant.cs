namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration
    {
        private const int INITAL_COMFORT = 5;
        private const decimal INITAL_PRICE = 10;

        public Plant() : base(INITAL_COMFORT, INITAL_PRICE)
        {
        }
    }
}
