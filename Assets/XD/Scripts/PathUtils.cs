using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PathUtils
{
    public static Dictionary<string, byte> mHasInPersistent = new Dictionary<string, byte>();

    public static string GetWebPath(string url)
    {
        byte hasInPersistent = 0;
        string path = GetPersistentPath(url);
        if (mHasInPersistent.TryGetValue(path, out hasInPersistent))
        {
            if (1 == hasInPersistent)
            {
                return GetWWWPath(path);
            }
        }

        if (File.Exists(path))
        {
            mHasInPersistent[path] = 1;
            return GetWWWPath(path);
        }
       
        return GetStreamingAssetsPath(url);
    }

    /// <summary>
    /// 获取持久存储路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetPersistentPath(string path)
    {
        return Application.persistentDataPath + "/" + path;
    }

    public static string GetStreamingAssetsPath(string path)
    {
        if (Application.isEditor)
        {
            return "file:///" + Application.streamingAssetsPath + "/" + path;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            return Application.streamingAssetsPath + "/" + path;
        }
        else
        {
          
            return "file:///" + Application.streamingAssetsPath + "/" + path;
        }


    }

    public static string GetWWWPath(string path)
    {
        if (path.StartsWith("http://") || path.StartsWith("ftp://") || path.StartsWith("https://") ||
            path.StartsWith("file://") || path.StartsWith("files://"))
            return path;
        if (Application.platform == RuntimePlatform.Android)
        {
            return path.Insert(0, "file://");
        }

        if (Application.platform != RuntimePlatform.WindowsPlayer)
        {
            return path.Insert(0, "file:///");
        }

        return path;
    }

    public static string GetResName(string url)
    {
        if (string.IsNullOrEmpty(url))
            return "";
        int slashIndex = url.LastIndexOf('/');
        int dotIndex = url.LastIndexOf('.');
        if (0 > dotIndex)
            dotIndex = url.Length;

        return url.Substring(slashIndex + 1, dotIndex - slashIndex - 1);
    }

}
