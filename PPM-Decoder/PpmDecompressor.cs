using System;
using System.IO;

namespace PPM_Decoder
{
    public class PpmDecompressor
    {
        private const int ModelOrder = 4;

        public void Decompress(BitInputStream inStream, Stream outStream)
        {
            var dec = new ArithmeticDecoder(inStream);
            var model = new PpmModel(ModelOrder, 256);

            var history = new int[0];

            while (true)
            {
                var symbol = DecodeSymbol(dec, model, history);
                if (symbol == 256)
                    break;
                outStream.WriteByte((byte) symbol);
                model.IncrementContexts(history, symbol);

                if (model.ModelOrder >= 1)
                {
                    if (history.Length < model.ModelOrder)
                        Array.Resize(ref history, history.Length + 1);
                    Array.Copy(history, 0, history, 1, history.Length - 1);
                    history[0] = symbol;
                }
            }
        }


        private static int DecodeSymbol(ArithmeticDecoder dec, PpmModel model, int[] history)
        {
            var order = history.Length;
            while (order >= 0)
            {
                var ctx = model.RootContext;
                var isBreak = false;
                for (var i = 0; i < order; i++)
                {
                    ctx = ctx.Subcontexts[history[i]];
                    if (ctx == null)
                    {
                        order--;
                        isBreak = true;
                        break;
                    }
                }

                if (isBreak)
                    continue;

                var symbol = dec.Read(ctx.Frequencies);
                if (symbol < 256)
                    return symbol;

                order--;
            }

            return dec.Read(model.orderMinus1Freqs);
        }
    }
}