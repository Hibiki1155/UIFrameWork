using UnityEngine;
using System.Collections;

/// <summary>
/// 提供窗口打开的函数规范
/// </summary>
public interface ISystemCtrl
{
    void OpenView(WindowType _type);
}
