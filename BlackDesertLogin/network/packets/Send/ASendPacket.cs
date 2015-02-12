using System;
using System.IO;
using System.Text;
using BDCommon.network;
using BlackDesertLogin.network.Protocol;
using BlackDesertLogin.Utils;
using NLog;

namespace BlackDesertLogin.network.packets.Send
{
    public abstract class ASendPacket : ISendPacket
    {
        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected byte[] Datas;
        protected object WriteLock = new object();


        public void Send(IConnection state,int clientState = 1)
        {
            //if (state == null || !state.IsValid)
            //    return;

            if (!OpCodes.Send.ContainsKey(GetType()))
            {
                Log.Warn("UNKNOWN packet opcode: {0}", GetType().Name);
                return;
            }


            lock (WriteLock)
            {
                ushort opCode = OpCodes.Send[GetType()];
                if (Datas == null)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream, new UTF8Encoding()))
                            {
                                writer.Write((byte)0);
                                writer.Write((ushort)0);
                                writer.Write(opCode);

                                Write(writer);

                                stream.Position = 0;

                                writer.Write((byte)clientState);
                                writer.Write((ushort)stream.Length);
                            }

                            Datas = stream.ToArray();
                            Log.Debug("Send packet '{0}'({2}) with datas:\n{1}", GetType().Name, Datas.ToHex(), opCode.ToString("X4"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warn("Can't write packet: {0}", GetType().Name);
                        Log.WarnException("ASendPacket", ex);
                        return;
                    }
                }
            }

            state.SendDatas(Datas);
        }

        public abstract void Write(BinaryWriter writer);

        protected void WriteD(BinaryWriter writer, int val)
        {
            writer.Write(val);
        }

        protected void WriteH(BinaryWriter writer, short val)
        {
            writer.Write(val);
        }

        protected void WriteC(BinaryWriter writer, byte val)
        {
            writer.Write(val);
        }

        protected void WriteDf(BinaryWriter writer, double val)
        {
            writer.Write(val);
        }

        protected void WriteF(BinaryWriter writer, float val)
        {
            writer.Write(val);
        }

        protected void WriteQ(BinaryWriter writer, long val)
        {
            writer.Write(val);
        }

        protected void WriteS(BinaryWriter writer, String text)
        {
            if (text == null)
            {
                writer.Write((short)0);
            }
            else
            {
                Encoding encoding = Encoding.Unicode;
                writer.Write(encoding.GetBytes(text));
                writer.Write((short)0);
            }
        }

        protected void WriteB(BinaryWriter writer, string hex)
        {
            writer.Write(hex.ToBytes());
        }

        protected void WriteB(BinaryWriter writer, byte[] data)
        {
            writer.Write(data);
        }
    }
}
