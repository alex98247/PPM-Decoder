namespace PPM_Decoder
{
    public interface IFrequencyTable
    {
        int GetSymbolLimit();

        void Increment(int symbol);
        int GetTotal();

        int GetLow(int symbol);

        int GetHigh(int symbol);
    }
}