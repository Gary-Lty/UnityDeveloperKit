using System.Data.Common;
using UnityDeveloperKit.Runtime.Api;
using UnityDeveloperKit.Runtime.Extension;
using UnityEngine;

namespace UnityDeveloperKit.Tests
{
    public class TestIObject : MonoBehaviour
    {
        private void Start()
        {
            var desk = transform.GetComponent<IDesk>();
            Debug.Log(desk.IsNull());
        }
    }
}