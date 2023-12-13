using GiftscopModifier.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GiftscopModifier.Core.Abstractions.SaveData.File
{
    internal class File : IBaseDataInterface
    {
        public RoomType? roomType;
        public float? x;
        public float? z;
        public float? y;
        public string? name;
        public ushort? pieces;

        void IBaseDataInterface.fillInData(byte[] data)
        {
            throw new NotImplementedException();
        }

        List<byte> IBaseDataInterface.rebuildData()
        {
            if (roomType == null)
            {
                throw new InvalidOperationException("Required property is null (roomType).");
            }
            else if (x == null)
            {
                throw new InvalidOperationException("Required property is null (x).");
            }
            else if (y == null)
            {
                throw new InvalidOperationException("Required property is null (y).");
            }
            else if (z == null)
            {
                throw new InvalidOperationException("Required property is null (z).");
            }
            else if (name == null)
            {
                throw new InvalidOperationException("Required property is null (name).");
            }
            else
            {
                if (name.Length < sbyte.MaxValue)
                {
                    throw new InvalidOperationException("Length of name is higher than 127 (max sbyte length).");
                }
            }

            List<byte> data = new List<byte>();

            data.Add(0x00);
            data.Add((byte)roomType);
            data.Add(0x01);
            data.Add(BitConverter.GetBytes(x.Value)[0]);
            data.Add(BitConverter.GetBytes(x.Value)[1]);
            data.Add(BitConverter.GetBytes(x.Value)[2]);
            data.Add(BitConverter.GetBytes(x.Value)[3]);
            data.Add(BitConverter.GetBytes(z.Value)[0]);
            data.Add(BitConverter.GetBytes(z.Value)[1]);
            data.Add(BitConverter.GetBytes(z.Value)[2]);
            data.Add(BitConverter.GetBytes(z.Value)[3]);
            data.Add(BitConverter.GetBytes(y.Value)[0]);
            data.Add(BitConverter.GetBytes(y.Value)[1]);
            data.Add(BitConverter.GetBytes(y.Value)[2]);
            data.Add(BitConverter.GetBytes(y.Value)[3]);
            data.Add(0x62);
            // Temporary. These bytes change with save files.
            // This might be a record of what pieces have been collected in a stage.
            data.Add(0x03);
            data.Add(0xFB);
            data.Add(0x62);
            data.Add(0x3E);

            // These bytes don't exist in a save file without pieces.
            if (pieces == 0)
            {
                data.Add(0x05);
                data.Add(0x02);
            }
        }
    }
}
