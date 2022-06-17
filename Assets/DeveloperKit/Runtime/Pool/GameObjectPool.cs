using System.Collections.Generic;
using UnityDeveloperKit.Runtime.Extension;
using UnityEngine;

namespace DeveloperKit.Runtime.Pool
{
    /// <summary>
    /// Unity object pool
    /// </summary>
    [System.Serializable]
    public class GameObjectPool : IPool<UnityEngine.GameObject>, IHasPrefabGameObject
    {
        [SerializeField] private GameObject prefab;
        private List<GameObject> content = new List<GameObject>();
        public IEnumerable<GameObject> Content => content;
        public GameObject Prefab => prefab;

        public  virtual GameObject PopItem()
        {
            var gameObject = content.Find(e=> !e.activeInHierarchy);
            if (!gameObject)
            {
                gameObject = NewItem();
                content.AddUnityObject(gameObject);
            }
            return gameObject;
        }

        public virtual void RecycleItem(GameObject item)
        {
            content.AddUnityObject(item);
        }

        public virtual GameObject NewItem()
        {
            return GameObject.Instantiate(prefab);
        }

    }

}