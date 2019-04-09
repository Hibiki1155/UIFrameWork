using UnityEngine;
using System.Collections;
using System.Text;

/// <summary>
/// 资源管理
/// </summary>
public class ResourcesMgr : SingleTon<ResourcesMgr>
{
    /// <summary>
    /// 资源缓冲区
    /// </summary>
    private Hashtable m_PrefabTable;

    /// <summary>
    /// 当前资源是否需要缓存
    /// </summary>
    public bool m_IsCache = false;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ResourcesMgr()
    {
        m_PrefabTable = new Hashtable();
    }

    /// <summary>
    /// 资源加载
    /// </summary>
    public GameObject Load(ResourcesType _type, string _resName, bool _isCache = false)
    {
        //给全局变量赋值
        m_IsCache = _isCache;

        GameObject resourcesPrefab = null;

        //检查Key是否存在(Key是资源的名称)
        if (m_PrefabTable.Contains(_resName))
        {
            Debug.Log("从资源缓冲区中读取!");
            //资源从哈希表中获取
            resourcesPrefab = m_PrefabTable[_resName] as GameObject;
        }
        else
        {
            Debug.Log("第一次加载!");

            StringBuilder path = new StringBuilder();

            //根据资源类型，完成加载的分类
            switch (_type)
            {
                case ResourcesType.Audio:
                    path.Append("Audio/");
                    break;
                case ResourcesType.Role:
                    path.Append("Role/");
                    break;
                case ResourcesType.UIScene:
                    path.Append("UIScenes/");
                    break;
                case ResourcesType.UIWindow:
                    path.Append("UIWindows/");
                    break;
            }

            //+=资源名称
            path.Append(_resName);

            //从文件夹中加载资源
            resourcesPrefab = Resources.Load<GameObject>(path.ToString());

            //是否需要存入缓存区中
            if (m_IsCache)
            {
                //需要缓存,存入缓存区
                m_PrefabTable.Add(_resName, resourcesPrefab);
            }
        }

        //返回克隆体
        return GameObject.Instantiate(resourcesPrefab);
    }
}
