using System;
using System.IO;

namespace PPM_Decoder
{
    public class BitInputStream
    {
        private readonly Stream _input;

        private int _currentByte;

        private int _numBitsRemaining;

        public BitInputStream(Stream inStream)
        {
            _input = inStream;
            _currentByte = 0;
            _numBitsRemaining = 0;
        }

        public int Read()
        {
            if (_currentByte == -1)
                return -1;
            if (_numBitsRemaining == 0)
            {
                _currentByte = _input.ReadByte();
                if (_currentByte == -1)
                    return -1;
                _numBitsRemaining = 8;
            }

            _numBitsRemaining--;
            return (_currentByte >> _numBitsRemaining) & 1;
        }

        public void Close()
        {
            _input.Close();
            _currentByte = -1;
            _numBitsRemaining = 0;
        }
    }
}