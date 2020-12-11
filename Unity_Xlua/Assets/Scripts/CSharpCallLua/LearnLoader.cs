using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
using System;
using UnityEngine.Events;

//无参无返回值的委托
//这里是自定义的委托
public delegate void CustomCall();

[CSharpCallLua]
public delegate int CustomCall2(int a);

[CSharpCallLua]
public delegate int CustomCall3(int a, out int b, out bool c, out string d, out int e);
[CSharpCallLua]
public delegate int CustomCall4(int a, ref int b, ref bool c, ref string d, ref int e);


public class LearnLoader : MonoBehaviour
{
    void Start()
    {
        LuaMgr.GetInstance().Init();
        LuaMgr.GetInstance().DoLuaFile("Main");
        //使用Global可以获取lua中的变量
        int i = LuaMgr.GetInstance().Global.Get<int>("haha");
        Debug.Log("testNumber:" + i);
        //lua中的number可以用这边的int short double float都行
        //更改
        LuaMgr.GetInstance().Global.Set("testNumber", 55);
        int i2 = LuaMgr.GetInstance().Global.Get<int>("testNumber");
        Debug.Log("testNumber:" + i2);

        //获取lua中的无参无返回值的方法
        //一共有4中方式
        //1、上面的public delegate void CustomCall();
        CustomCall call = LuaMgr.GetInstance().Global.Get<CustomCall>("testFun");
        //2、Unity自带委托
        UnityAction ua = LuaMgr.GetInstance().Global.Get<UnityAction>("testFun");
        ua();
        //3、C#提供的委托
        Action ac = LuaMgr.GetInstance().Global.Get<Action>("testFun");
        ac();
        //4、Xlua提供的一种 获取函数的方式 少用
        LuaFunction lf = LuaMgr.GetInstance().Global.Get<LuaFunction>("testFun");
        lf.Call();

        //有参有返回
        CustomCall2 call2 = LuaMgr.GetInstance().Global.Get<CustomCall2>("testFun2");
        Debug.Log("有参有返回：" + call2(10));
        //C#自带的泛型委托 方便我们使用
        //第一个是传入类型，第二个是返回类型
        Func<int, int> sFun = LuaMgr.GetInstance().Global.Get<Func<int, int>>("testFun2");
        Debug.Log("有参有返回：" + sFun(20));
        //Xlua提供的
        //返回的是一个数组，因为就1个，所以用[0]
        LuaFunction lf2 = LuaMgr.GetInstance().Global.Get<LuaFunction>("testFun2");
        Debug.Log("有参有返回：" + lf2.Call(30)[0]);


        //多返回值
        //使用 out 和 ref 来接收
        CustomCall3 call3 = LuaMgr.GetInstance().Global.Get<CustomCall3>("testFun3");
        int b;
        bool c;
        string d;
        int e;
        Debug.Log("第一个返回值：" + call3(100, out b, out c, out d, out e));
        Debug.Log(b + "_" + c + "_" + d + "_" + e);
        //ref必须要先进行初始化
        CustomCall4 call4 = LuaMgr.GetInstance().Global.Get<CustomCall4>("testFun3");
        int b1 = 0;
        bool c1 = true;
        string d1 = "";
        int e1 = 0;
        Debug.Log("第一个返回值：" + call4(200, ref b1, ref c1, ref d1, ref e1));
        Debug.Log(b1 + "_" + c1 + "_" + d1 + "_" + e1);
    }


}
