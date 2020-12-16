print("lua调用C#类相关知识点")
-- lua中调用C#中的类
-- 固定写法
-- CS.命名空间.类名
-- Unity的类，比如 GameObject Transform等等 CS.UnityEngine.类名
-- CS.CS.UnityEngine.GameObject

-- 通过C#中的类实例化对象 lua中没有new 所以我们直接 类名括号就是实例化对象
-- 默认调用的 相当于就是无参数构造
-- 起别名，节约性能 定居全局变量存储 C#中的类
Debug = CS.UnityEngine.Debug 
GameObject = CS.UnityEngine.GameObject
Vector3 = CS.UnityEngine.Vector3

local obj1 = CS.UnityEngine.GameObject()

local obj2 = CS.UnityEngine.GameObject("dengge")

local obj3 = GameObject("邓哥")

-- 类中的静态变量 可以直接使用.去调用
local obj4 = GameObject.Find("邓哥")

-- 得到对象中的成员变量 直接对象. 
print(obj4.transform.position)

-- 使用对象中的成员方法就要去用 :
obj4.transform:Translate(Vector3.right)

Debug.Log(obj4.transform.position)

-- 自定义类 使用方法 相同 只是命名空间不同而已
local t = CS.Test()
t:Speak("test说话")

local t2 = CS.MrDeng.Test2()
t2:Speak("test2说话")

-- Addcomponent在C#那一侧，两种重载，一种是泛型AddComponent<Image>，另一种是通过AddComponent(type)这种，lua不支持泛型，所以用第二种
-- xlua提供了一个重要的方法，typeof，可以得到类的type
-- xlua中不支持 无参泛型函数，所以用AddComponent(type)这种，传入类的type
local obj5 = GameObject('加脚本测试')
obj5:AddComponent(typeof(CS.LuaCallCSharp))--添加LuaCallCSharp脚本