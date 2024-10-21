namespace FinTrack.Domain.Entities
{
    public class Currency
    {
        public const ushort DEFAULT_DECIMALS = 2;
        public const ushort DEFAULT_NUMBER_TO_MAJOR = 100;

        public uint Id { get; set; }
        public required string Name { get; set; }
        public required string Alpha3Code { get; set; }
        public string? Symbol { get; set; }
        public ushort Decimals { get; set; } = 2;
        public ushort NumberToMajor { get; set; } = 100;
    }
}
