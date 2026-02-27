using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICounterable
{
    // 询问：是否在反击窗口
    bool CanBeCountered();

    // 执行：被反击了做相应动作
    void HandleCounter();
}
