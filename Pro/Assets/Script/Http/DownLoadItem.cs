/**  
* 标    题：   DownLoadItem.cs 
* 描    述：    
* 创建时间：   2020年03月09日 16:57 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
	public class DownLoadItem
    {
        /// <summary>
        /// 网络资源url路径
        /// </summary>
        private string cdnUrl = "http://localhost:81/";
        /// <summary>
        /// 资源下载存放路径，不包含文件名
        /// </summary>
        private string savePath;
        /// <summary>
        /// 文件名,不包含后缀
        /// </summary>
        private string fileNameWithoutExt;
        /// <summary>
        /// 文件后缀
        /// </summary>
        private string fileExt;
        /// <summary>
        /// 下载文件全路径，路径+文件名+后缀
        /// </summary>
        private string filePath;
        /// <summary>
        /// 缓存路径
        /// </summary>
        private string cechePath;
        /// <summary>
        /// 缓存文件后缀
        /// </summary>
        private string cechefileExt = ".ceche";

        /// <summary>
        /// 原文件大小
        /// </summary>
        private long fileLength;
        /// <summary>
        /// 当前下载好了的大小
        /// </summary>
        private long currentLength;
        /// <summary>
        /// 是否开始下载
        /// </summary> 
        private bool isStartDownload;

        private Action onFinish;
        private Action<string> onFail;

        public DownLoadItem(Action finish, Action<string> fail)
        {
            onFinish = finish;
            onFail = fail;
        }

        public void UpdateData(string url, string FilePath, string path)
        {
            cdnUrl = url + path;
            savePath = FilePath;
            fileNameWithoutExt = Path.GetFileNameWithoutExtension(path);
            fileExt = Path.GetExtension(path);
            filePath = string.Format("{0}/{1}{2}", savePath, fileNameWithoutExt, fileExt);
            cechePath = string.Format("{0}/{1}{2}", savePath, fileNameWithoutExt, cechefileExt);
        }


        public void DownLoad()
        {
            if (!string.IsNullOrEmpty(savePath))
            {
                if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

            }
            try
            {

                HttpWebRequest requet = (HttpWebRequest)HttpWebRequest.Create(cdnUrl);
                requet.Method = "GET";
                FileStream fileStream = null;

                if (Directory.Exists(cechePath))
                {
                    fileStream = File.OpenWrite(cechePath);
                    currentLength = fileStream.Length;
                }
                else
                {
                    fileStream = new FileStream(cechePath, FileMode.Create, FileAccess.Write);
                    currentLength = 0;
                }
                HttpWebResponse response = (HttpWebResponse)requet.GetResponse();

                Stream stream = response.GetResponseStream();
                fileLength = currentLength + response.ContentLength; ;
                int lengthOnce;
                while (currentLength < fileLength)
                {

                    byte[] buffer = new byte[1024 * 20];

                    if (stream.CanRead)
                    {
                        lengthOnce = stream.Read(buffer, 0, buffer.Length);
                        currentLength += lengthOnce;
                        fileStream.Write(buffer, 0, lengthOnce);
                    }
                    else
                    {
                        break;
                    }

                }
                response.Close();
                stream.Close();
                fileStream.Close();

                //临时文件转为最终的下载文件
                File.Move(cechePath, filePath);
                if (onFinish != null) onFinish();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Exeption : " + e.ToString());
                if (onFail != null) onFail(e.ToString());
            }
        }
    }
}