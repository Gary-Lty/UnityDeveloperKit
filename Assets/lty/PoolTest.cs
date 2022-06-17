using System;
using System.Collections.Generic;
using DeveloperKit.Runtime.Pool;
using Sirenix.OdinInspector;
using UnityDeveloperKit.Runtime.Api;
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
        
    }
}