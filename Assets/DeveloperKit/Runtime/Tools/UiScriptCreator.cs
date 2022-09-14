using System.Collections.Generic;
using System.IO;
using System.Text;
// using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UiTools
{
    /// <summary>
    /// 自动化生成ui脚本
    /// </summary>
    public class UiScriptCreator : MonoBehaviour
    {
#if UNITY_EDITOR
        
        public string Namespace = "UI";
        public string fullPath = "";
        public bool button = true;
        public bool text = true;
        public bool inputField = true;
        public bool slider = true;
        public bool image = true;
        public bool scrollRect = true;

        private void Reset()
        {
            fullPath = Path.Combine(Application.dataPath, "Scripts", "UI");
        }

        [System.Serializable]
        public class UiElement
        {
            public string fullType;

            public string name;

            public string type = string.Empty;

            public string feildName => name + "_" + type;

            public string path;
        }

        // [TableList]
        public List<UiElement> ElementsList = new List<UiElement>();

        private void TryGetComponent<T>() where T : Component
        {
            var cs = transform.GetComponentsInChildren<T>();
            foreach (var c in cs)
            {
                var type = c.GetType();
                ElementsList.Add(new UiElement()
                {
                    fullType = type.FullName,
                    type = type.Name,
                    name = c.name,
                    path = GetPath(c.transform)
                });
            }
        }

        string GetPath(Transform node)
        {
            if (node.parent && node != transform)
            {
                var path = node.name;
                node = node.parent;
                while (node != transform)
                {
                    path = node.name + "/" + path;
                    node = node.parent;
                }

                return path;
            }
            else
            {
                return string.Empty;
            }
        }

        // [Button("检测组件")]
        void DetectComponent()
        {
            ElementsList.Clear();
            if (button) TryGetComponent<Button>();
            if(text) TryGetComponent<Text>();
            if(inputField) TryGetComponent<InputField>();
            if(image) TryGetComponent<Image>();
            if(scrollRect) TryGetComponent<ScrollRect>();
            if(slider) TryGetComponent<Slider>();
            TryGetComponent<Mask>();
            // TryGetComponent<TextMeshProUGUI>();
            // TryGetComponent<TMP_InputField>();
            // TryGetComponent<TMP_Dropdown>();
        }


        // [Button("生成脚本")]
        void GenerateScript()
        {
            var sb = new StringBuilder();
            sb.Append("using TMPro;\n");
            sb.Append("using UnityEngine;\n");
            sb.Append("using UnityEngine.UI;\n");
            sb.Append("namespace " + Namespace + "\n");
            sb.Append("{\n");
            sb.Append($"    public class {name} : MonoBehaviour\n");
            sb.Append("    {\n");
            foreach (var e in ElementsList)
            {
                sb.Append($"        public {e.fullType} {e.feildName};\n");
            }
            sb.Append("        public void Reset()\n");
            sb.Append("        {\n");

            foreach (var e in ElementsList)
            {
                if (string.IsNullOrEmpty(e.path))
                {
                    sb.Append($"            {e.feildName} = transform.GetComponent<{e.fullType}>();\n");
                }
                else
                {
                    sb.Append($"            {e.feildName} = transform.Find(\"{e.path}\").GetComponent<{e.fullType}>();\n");
                }
            }

            sb.Append("        }\n");
            sb.Append("    }\n");
            sb.Append("}\n");

            var path = Path.Combine(fullPath, name+ ".cs");
            using var sw = new StreamWriter(path);
            sw.Write(sb.ToString());
            Debug.LogWarning(path);
        }
#endif
    }
}