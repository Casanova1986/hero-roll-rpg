using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

namespace MiniOdin
{
    [CustomEditor(typeof(UnityEngine.Object), true)]
    [CanEditMultipleObjects]
    public class ButtonAttributeDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw default inspector first
            DrawDefaultInspector();

            // Then draw buttons for methods with [Button]
            foreach (var obj in targets)
            {
                DrawButtons(obj);
            }
        }

        private void DrawButtons(object target)
        {
            var type = target.GetType();
            var methods = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );

            foreach (var method in methods)
            {
                var button = method.GetCustomAttributes(typeof(ButtonAttribute), true)
                                   .FirstOrDefault() as ButtonAttribute;

                if (button == null)
                    continue;

                // Skip methods with parameters
                if (method.GetParameters().Length > 0)
                {
                    EditorGUILayout.HelpBox(
                        $"Method '{method.Name}' has parameters. MiniOdin Button only supports parameterless methods.",
                        MessageType.Warning
                    );
                    continue;
                }

                string label = string.IsNullOrEmpty(button.Label) ? method.Name : button.Label;

                if (GUILayout.Button(label))
                {
                    try
                    {
                        method.Invoke(method.IsStatic ? null : target, null);
                        
                        if (target is UnityEngine.Object uObj)
                        {
                            EditorUtility.SetDirty(uObj);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
        }
    }
}
