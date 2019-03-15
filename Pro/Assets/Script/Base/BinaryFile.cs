/**  
* 标    题：   BinaryFile.cs 
* 描    述：    
* 创建时间：   2017年12月15日 12:00 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Divak.Script.Game
{
    public class BinaryFile
    {
        public static byte[] ReadBinaryFile(string path, string name, TypeEnum type)
        {
            string readPath = path + name;
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(readPath, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException e)
            {
                Debug.Log(e.Message);
            }
            if (fs != null)
            {
                byte[] bytes = null;
                if (type == TypeEnum.ByteArray)
                {
                    bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                }
                else
                {
                    br = new BinaryReader(fs);
                    bytes = (byte[])BinaryReadByType(br, TypeEnum.ByteArray);
                }
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (br != null)
                {
                    br.Close();
                    br = null;
                }
                return bytes;
            }
            if (fs != null)
            {
                fs.Close();
                fs = null;
            }
            if (br != null)
            {
                br.Close();
                br = null;
            }
            return null;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="name">文件名字</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool SaveBinaryFile(string path, string name, object obj, TypeEnum type)
        {
            //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string savePath = path + name;
            if (string.IsNullOrEmpty(savePath))
            {
                Debug.LogError("SaveBinaryFile Fail!! path isNullOrEmpty");
                return false;
            }
            BinaryWriter bw = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(savePath, FileMode.Create);
                bw = new BinaryWriter(fs);
            }
            catch (IOException e)
            {
                Debug.Log(e.Message);
            }
            if (bw != null)
            {
                BinaryWriterByType(bw, obj, type);
                Debug.Log(string.Format("SaveBinaryFile: Success !!, path:{0}", savePath));
                if (bw != null)
                {
                    bw.Close();
                    bw = null;
                }
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                return true;
            }
            if (bw != null)
            {
                bw.Close();
                bw = null;
            }
            if (fs != null)
            {
                fs.Close();
                fs = null;
            }
            return false;
        }

        /// <summary>
        /// 数据类型读
        /// </summary>
        public static object BinaryReadByType(BinaryReader read, TypeEnum type)
        {
            switch (type)
            {
                case TypeEnum.Double:
                    return read.ReadDouble();
                case TypeEnum.UInt64:
                    return read.ReadInt64();
                case TypeEnum.UInt32:
                    return read.ReadInt32();
                case TypeEnum.UInt16:
                    return read.ReadInt16();
                case TypeEnum.Byte:
                    return read.ReadByte();
                default:
                    Debug.LogError("BinaryWriterByType, 类型有误！！");
                    break;
            }
            return null;
        }

        /// <summary>
        /// 数据类型写
        /// </summary>
        public static void BinaryWriterByType(BinaryWriter writer, object value, TypeEnum type)
        {
            switch (type)
            {
                case TypeEnum.Double:
                    writer.Write((double)value);
                    break;
                case TypeEnum.UInt64:
                    writer.Write((ulong)value);
                    break;
                case TypeEnum.UInt32:
                    writer.Write((uint)value);
                    break;
                case TypeEnum.UInt16:
                    writer.Write((ushort)value);
                    break;
                case TypeEnum.Byte:
                    writer.Write((byte)value);
                    break;
                case TypeEnum.ByteArray:
                    writer.Write((byte[])value);
                    break;
                default:
                    Debug.LogError("BinaryWriterByType, 类型有误！！");
                    break;
            }
        }

        #region 字节流转换
        /// <summary>
        /// list 转字节流
        /// </summary>
        /// <returns></returns>
        public static byte[] GetByteByList<T>(List<T> list)
        {
            if (list == null)
            {
                Debug.LogError("GetByteByList Fail!!, list is null");
                return null;
            }
            MemoryStream ms = new MemoryStream();
            //创建序列化的实例
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, list);
            byte[] bytes = ms.GetBuffer();
            ms.Close();
            ms = null;
            return bytes;
        }
        /// <summary>
        /// 字节流转List
        /// </summary>
        /// <returns></returns>
        public static List<T> GetListByByte<T>(byte[] bytes)
        {
            if (bytes == null)
            {
                Debug.LogError("GetListByByte Fail!!, dic is buff");
                return null;
            }
            //利用传来的byte[]创建一个内存流
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            //把流中转换为Dictionary
            object obj = formatter.Deserialize(ms);
            ms.Close();
            ms = null;
            return (List<T>)obj;
        }
        /// <summary>
        /// dic 转字节流
        /// </summary>
        /// <returns></returns>
        public static byte[] GetByteByDic<T, K>(Dictionary<T, K> dic)
        {
            if (dic == null)
            {
                Debug.LogError("GetByteByDic Fail!!, dic is null");
                return null;
            }
            MemoryStream ms = new MemoryStream();
            //创建序列化的实例
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, dic);
            byte[] bytes = ms.GetBuffer();
            ms.Close();
            ms = null;
            return bytes;
        }

        /// <summary>
        /// 字节流转Dic
        /// </summary>
        /// <returns></returns>
        public static Dictionary<T, K> GetDicByByte<T, K>(byte[] bytes)
        {
            if (bytes == null)
            {
                Debug.LogError("GetDicByByte Fail!!, dic is buff");
                return null;
            }
            //利用传来的byte[]创建一个内存流
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            //把流中转换为Dictionary
            object obj = formatter.Deserialize(ms);
            ms.Close();
            ms = null;
            return (Dictionary<T, K>)obj;
        }

        /// <summary>
        /// Obj 转字节流
        /// </summary>
        /// <returns></returns>
        public static byte[] GetByteByObj<T>(T go)
        {
            if (go == null)
            {
                Debug.LogError("GetByteByDic Fail!!, dic is null");
                return null;
            }
            return GetByte<T>(go);
        }

        /// <summary>
        /// 字节流转Obj
        /// </summary>
        /// <returns></returns>
        public static T GetObjByByte<T>(byte[] bytes)
        {
            if (bytes == null)
            {
                Debug.LogError("GetObjByByte Fail!!, dic is buff");
                return default(T);
            }
            return (T)GetType<T>(bytes);
        }
        #endregion

        #region 私有函数
        private static byte[] GetByte<T>(T o)
        {
            MemoryStream ms = new MemoryStream();
            //创建序列化的实例
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, o);
            byte[] bytes = ms.GetBuffer();
            ms.Close();
            ms = null;
            return bytes;
        }

        private static T GetType<T>(byte[] bytes)
        {
            //利用传来的byte[]创建一个内存流
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            //把流中转换为Dictionary
            object obj = formatter.Deserialize(ms);
            ms.Close();
            ms = null;
            return (T)obj;
        }
        #endregion
    }
}