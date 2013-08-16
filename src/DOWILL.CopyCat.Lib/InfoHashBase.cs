
namespace DOWILL.CopyCat.Lib
{
    public class InfoHashBase : IInfoHash
    {
        public InfoHashBase(byte[] bin)
        {
            Bytes = bin;
            Hex = HexEncoding.ToString(bin);
        }
        public byte[] Bytes { get; protected set; }
        public string Hex { get; protected set; }
        public override string ToString()
        {
            return Hex;
        }
    }
}