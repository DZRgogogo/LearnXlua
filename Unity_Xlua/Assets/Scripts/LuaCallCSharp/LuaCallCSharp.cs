using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{
    public void Speak(string str)
    {
        Debug.Log("Test2" + str);
    }
}
namespace MrDeng
{
    public class Test2
    {
        public void Speak(string str)
        {
            Debug.Log("Test2" + str);
        }
    }
}

/// <summary>
/// lua没办法直接访问C#，要先从C#调用lua脚本
/// 在去lua中调用C#
/// </summary>
public class LuaCallCSharp : MonoBehaviour
{
    void Start()
    {
        LuaMgr.GetInstance().Init();
        LuaMgr.GetInstance().DoLuaFile("Main");
    }
}
