namespace dndhelper.Models
{
    public enum DiceType
    {
        D4 = 4,
        D6 = 6,
        D8 = 8,
        D10 = 10,
        D12 = 12,
        D20 = 20
    }

    public class DiceModel
    {
        public int Id { get; set; }
        public DiceType SelectedDice { get; set; }
        public int RandomNumber { get; set; }
    }
}
