using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
/// <summary>
/// Lua管理器
/// 提供lua解析器
/// 保证解析器的唯一性
/// </summary>
public class LuaMgr :BaseManager<LuaMgr>
{
    //重定向
    //dump
    private LuaEnv luaEnv;
    /// <summary>
    /// 初始化解析器
    /// </summary>
    public void Init()
    {
        if (luaEnv != null)//避免二次创建
        {
            return; 
        }
        luaEnv = new LuaEnv();
        //委托，重定向lua资源
        //特定路径加载lua资源
        luaEnv.AddLoader(MyCustomLoader);
        //ab包中
        luaEnv.AddLoader(MyCustomABLoader);
    }
    /// <summary>
    /// 给定特定路径，进行重定向lua的位置，并加载
    /// </summary>
    /// <param name="filepath">require的lua脚本文件名</param>
    /// <returns></returns>
    public byte[] MyCustomLoader(ref string filepath)
    {
        //传入的参数是require的lua脚本文件名
        //拼接出一个lua文件的路径
        //Application.dataPath 就是当前项目的asset路径
        string path = Application.dataPath + "/lua/" + filepath + ".lua";
        //有路径就去加载
        if (File.Exists(path))
        {
            //Debug.Log("这里进行重定向成功" + path);
            return File.ReadAllBytes(path);
        }
        else
        {
            Debug.Log("这里进行重定向失败了" + path);
        }
        return null;
    }
    /// <summary>
    /// 从ab包中加载lua资源
    /// </summary>
    /// <param name="filepath">require的lua脚本文件名</param>
    /// <returns></returns>
    public byte[] MyCustomABLoader(ref string filepath)
    {
        ////从AB包中加载lua文件
        ////加载AB包
        ////string path = Application.streamingAssetsPath + "/lua";
        //string path = Application.streamingAssetsPath + "/lua";
        //AssetBundle ab = AssetBundle.LoadFromFile(path);

        ////加载lua文件
        //TextAsset tx = ab.LoadAsset<TextAsset>(filepath + ".lua");
        ////加载lua文件，返回byte数组
        //return tx.bytes;

        //必须用同步加载
        TextAsset lua = ABMgr.GetInstance().LoadRes<TextAsset>("lua", filepath + ".lua");
        if (lua != null)
        {
            return lua.bytes;
        }
        else
        {
            Debug.Log("MyCustomABLoader重定向失败，文件名为"+ filepath);
        }
        return null;
    }
    //lua脚本会放到ab包
    //最终会通过先加载ab包，再通过加载其中的lua脚本资源来执行它
    //AB包中如果要加文本，后缀还是有一定的限制， .lua不能被识别
    //打包时，要把lua文件后缀改为txt



    /// <summary>
    /// 执行lua语言
    /// </summary>
    /// <param name="str"></param>
    public void DoString(string str)
    {
        if (luaEnv == null)
        {
            Debug.Log("解析器为空，需要初始化解析器");
            return;
        }
        luaEnv.DoString(str);
    }
    /// <summary>
    /// 释放lua垃圾
    /// </summary>
    /// <param name="str"></param>
    public void Tick(string str)
    {
        if (luaEnv == null)
        {
            Debug.Log("解析器为空，需要初始化解析器");
            return;
        }
        luaEnv.Tick();
    }
    /// <summary>
    /// 销毁lua解释器
    /// </summary>
    /// <param name="str"></param>
    private void Dispose(string str)
    {
        if (luaEnv == null)
        {
            Debug.Log("解析器为空，需要初始化解析器");
            return;
        }
        luaEnv.Dispose();
        luaEnv = null;
    }

}
