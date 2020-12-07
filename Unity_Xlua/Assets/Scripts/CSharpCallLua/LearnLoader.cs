using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LearnLoader : MonoBehaviour
{
    void Start()
    {
        LuaMgr.GetInstance().Init();
        LuaMgr.GetInstance().DoString("require('Main')");
    }

 
}
