using System;

namespace PPM_Decoder
{
    public class FlatFrequencyTable : IFrequencyTable
    {
        private const int NumSymbols = 257;

        public int GetSymbolLimit() => NumSymbols;

        public int GetTotal() => NumSymbols;

        public int GetLow(int symbol) => symbol;

        public int GetHigh(int symbol) => symbol + 1;

        public void Increment(int symbol) => new InvalidOperationException();
    }
}