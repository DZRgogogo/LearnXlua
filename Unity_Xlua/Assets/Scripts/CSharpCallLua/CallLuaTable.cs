using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class CallLuaTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.GetInstance().Init();
        LuaMgr.GetInstance().DoLuaFile("Main");

        //不建议使用LuaTable和LuaFunction 效率低
        //引用对象
        LuaTable table = LuaMgr.GetInstance().Global.Get<LuaTable>("testClas");
        Debug.Log(table.Get<int>("testInt"));
        Debug.Log(table.Get<bool>("testBool"));
        Debug.Log(table.Get<float>("testFloat"));
        Debug.Log(table.Get<string>("testString"));

        table.Get<LuaFunction>("testFun").Call();
        //改  引用
        table.Set("testInt", 55);
        Debug.Log(table.Get<int>("testInt"));
        LuaTable table2 = LuaMgr.GetInstance().Global.Get<LuaTable>("testClas");
        Debug.Log(table2.Get<int>("testInt"));

        table.Dispose();
        table2.Dispose(); 
    }
}
