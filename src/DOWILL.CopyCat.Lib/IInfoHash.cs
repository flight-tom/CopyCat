using System;
using System.Collections.Generic;
using System.Text;

namespace DOWILL.CopyCat.Lib
{
    public interface IInfoHash
    {
        byte[] Bytes { get; }
        string Hex { get; }
        string ToString();
    }
}
