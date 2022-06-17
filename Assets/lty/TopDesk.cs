using DeveloperKit.Runtime.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityDeveloperKit.Tests
{
    public class TopDesk : MonoBehaviour,IDesk
    {
        public bool IsInUse { get; set; }
        public void OnRecycle()
        {
            Debug.Log("OnRecycle");
        }
        public ICanRecycleItem<IDesk> Creator { get; set; }

        [Button]
        public void R()
        {
            this.Recycle();
        }
    }
}