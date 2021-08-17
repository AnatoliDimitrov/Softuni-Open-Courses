namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int INITAL_COMFORT = 1;
        private const decimal INITAL_PRICE = 5;

        public Ornament() : base(INITAL_COMFORT, INITAL_PRICE)
        {

        }
    }
}
