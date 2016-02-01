using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.InteropServices;
using SevenZip.Sdk.Compression;

namespace YGOPro_Tweaker
{
    class cReplayManager
    {
        private BinaryReader _DataReader;
        private byte[] _mFileContent;
        private byte[] _mData;
        public ReplayHeader Header;

        public BinaryReader DataReader
        {
            get
            {
                return _DataReader;
            }
        }

        public bool Compressed
        {
            get
            {
                return ((this.Header.Flag & 1) != 0);
            }
        }

        public bool SingleMode
        {
            get
            {
                return (!((this.Header.Flag & 2) != 0));
            }
        }

        public string ReadString(int length)
        {
            string str = Encoding.Unicode.GetString(this._DataReader.ReadBytes(length));
            return str.Substring(0, str.IndexOf("\0", StringComparison.Ordinal));
        }

        public string ExtractName(string name)
        {
            if (name.Contains("]"))
            {
                return name.Split(new char[] { ']' })[1];
            }
            return name;
        }

        public bool FromFile(string fileName)
        {
            try
            {
                this._mFileContent = File.ReadAllBytes(fileName);
                BinaryReader reader = new BinaryReader(new MemoryStream(this._mFileContent));
                this.HandleHeader(reader);
                this.HandleData(reader);
                reader.Close();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        public struct ReplayHeader
        {
            public uint Id;
            public uint Version;
            public uint Flag;
            public uint Seed;
            public uint DataSize;
            public uint Hash;
            public byte[] Props;
        }

        private void HandleHeader(BinaryReader reader)
        {
            this.Header.Id = reader.ReadUInt32();
            this.Header.Version = reader.ReadUInt32();
            this.Header.Flag = reader.ReadUInt32();
            this.Header.Seed = reader.ReadUInt32();
            this.Header.DataSize = reader.ReadUInt32();
            this.Header.Hash = reader.ReadUInt32();
            this.Header.Props = reader.ReadBytes(8);
        }


        private void HandleData(BinaryReader reader)
        {
            int count = this._mFileContent.Length - 0x20;
            if (!this.Compressed)
            {
                this._mData = reader.ReadBytes(count);
                this._DataReader = new BinaryReader(new MemoryStream(this._mData));
            }
            else
            {
                byte[] destinationArray = new byte[count];
                Array.Copy(this._mFileContent, 0x20, destinationArray, 0, count);
                byte[] buffer = new byte[this.Header.DataSize];
                SevenZip.Sdk.Compression.Lzma.Decoder decoder = new SevenZip.Sdk.Compression.Lzma.Decoder();

                decoder.SetDecoderProperties(this.Header.Props);
                decoder.Code(new MemoryStream(destinationArray), new MemoryStream(buffer), (long)count, (long)this.Header.DataSize, null);
                this._mData = buffer;
                this._DataReader = new BinaryReader(new MemoryStream(this._mData));
            }
        }
    }
}
