using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class LearnLuaEnv : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //一般保证解析器的唯一性
        LuaEnv env = new LuaEnv();
        env.DoString("print('你好')", "LearnLuaEnv");

        //执行一个lua脚本
        //注意这里默认调用的是Resources里面的资源，并且只认xxxx.lua.txt
        //估计是Resources.Load去加载的，只支持txt,bytes
        //这样就会很麻烦，一般是在ab包里进行加载，需要文件加载重定向
        env.DoString("require('Main')");

        //lua没有手动释放对象的手，进行垃圾回收
        //帧更新中定时执行或者在切场景时候执行
        env.Tick();//lua的垃圾回收

        env.Dispose();//销毁lua解析器，一般很少用，都是保证解析器的唯一性
    }
}
