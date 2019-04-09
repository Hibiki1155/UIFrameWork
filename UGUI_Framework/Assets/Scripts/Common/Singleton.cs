using UnityEngine;
using System.Collections;

public class SingleTon<T> where T :new() //泛型约束，只能被可实例化的对象替代
{
    #region 单例
    private static T instance;

    public static T GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    #endregion

}
