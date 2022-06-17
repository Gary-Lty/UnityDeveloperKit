using System.Collections.Generic;
using UnityDeveloperKit.Runtime.Api;
using UnityDeveloperKit.Runtime.Extension;
using UnityEngine;

namespace DeveloperKit.Runtime.Pool
{
    /// <summary>
    /// 组件对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class ComponentPool<T> : IHasContent<T>, ICanPopItem<T>, ICanRecycleItem<T>, ICanNewItem<T>,
        IHasPrefabGameObject where T : ICanRecycle,IComponent,IHasCreator<T>
    {
        public List<T> content = new List<T>();
        [SerializeField] private GameObject prefab;
        
        public IEnumerable<T> Content => content;
        
        public GameObject Prefab => prefab;
        

        public virtual T PopItem()
        {
            var component = content.Find(e => !e.IsInUse);
            if (component.IsNull())
            {
                component = NewItem();
                content.AddObject(component);
            }
            
            component.IsInUse = true;
            return component;
        }

        public virtual void RecycleItem(T item)
        {
            item.OnRecycle();
            item.IsInUse = false;
            content.AddObject(item);
        }

        public virtual T NewItem()
        {
            var t = GameObject.Instantiate(Prefab).GetComponent<T>();
            t.Creator = this;
            return t;
        }
    }
}