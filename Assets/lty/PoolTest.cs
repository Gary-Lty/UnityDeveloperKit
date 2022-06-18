using System;
using System.Collections.Generic;
using DeveloperKit.Runtime.Pool;
using Sirenix.OdinInspector;
using UnityDeveloperKit.Runtime.Api;
using UnityDeveloperKit.Runtime.Extension;
using UnityEngine;

namespace UnityDeveloperKit.Tests
{

    public interface IDesk : IComponent,ICanRecycle,IHasCreator<IDesk>
    {
        
    }
    
    public class PoolTest : MonoBehaviour
    {
        [System.Serializable]
        public class DeskPool : ComponentPool<IDesk>
        {
            public Transform parent;
            public override IDesk NewItem()
            {
                var t = GameObject.Instantiate(Prefab).GetComponent<IDesk>();
                t.Creator = this;
                return t;
            }
        }

        public DeskPool deskPool = new DeskPool();

        public List<GameObject> poolObjs;
        private IDesk item;

        [Button]
        public void Pop()
        {
            deskPool.PopItem();

            poolObjs = new List<GameObject>();
            foreach (var desk in deskPool.Content)
            {
                poolObjs.Add(desk.gameObject);
            }
        }

        [Button]
        public void Ins()
        {
            this.item = deskPool.PopItem();
            Debug.Log(item);
        }
        [Button]

        void Des()
        {
            Destroy(item.gameObject);
        }
        [Button]

        void Log()
        {
            Debug.Log(item);
            Debug.Log(item == null);
            Debug.Log(item.IsNull());
        }
        
    }
}