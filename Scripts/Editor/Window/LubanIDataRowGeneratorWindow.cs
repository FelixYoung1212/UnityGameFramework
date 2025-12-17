using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityGameFramework.Editor
{
    public class LubanIDataRowGeneratorWindow : EditorWindow
    {
        [MenuItem("Game Framework/Luban IDataRow生成器")]
        public static void ShowWindow()
        {
            GetWindow<LubanIDataRowGeneratorWindow>("Luban IDataRow生成器");
        }

        private Vector2 scrollPosition;
        private List<System.Type> beanBaseTypes = new List<System.Type>();

        private void OnEnable()
        {
            RefreshTypes();
        }

        private void RefreshTypes()
        {
            beanBaseTypes.Clear();

            // 获取所有程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && (t.BaseType?.FullName == "Luban.BeanBase" || t.BaseType?.BaseType?.FullName == "Luban.BeanBase"))
                        .Where(t => t.Namespace == "cfg");

                    foreach (var type in types)
                    {
                        beanBaseTypes.Add(type);
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    // 忽略无法加载的程序集
                }
            }

            beanBaseTypes = beanBaseTypes.OrderBy(t => t.Name).ToList();
        }

        private void OnGUI()
        {
            // 显示类型列表
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            foreach (var type in beanBaseTypes)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"命名空间: {type.Namespace}", GUILayout.Width(150));
                EditorGUILayout.LabelField($"类名: {type.Name}");
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            // 刷新按钮
            if (GUILayout.Button("刷新类型列表", GUILayout.Height(30)))
            {
                RefreshTypes();
            }

            EditorGUILayout.Space();

            // 生成所有按钮
            if (GUILayout.Button("生成所有文件", GUILayout.Height(30)))
            {
                GenerateFiles(beanBaseTypes);
            }
        }

        private void GenerateFiles(List<System.Type> typesToGenerate)
        {
            string outputPath = GetOutputPath();
            if (string.IsNullOrEmpty(outputPath))
            {
                return;
            }

            int successCount = 0;
            int errorCount = 0;

            try
            {
                foreach (var type in typesToGenerate)
                {
                    try
                    {
                        if (GenerateIDataRowFile(type, outputPath))
                        {
                            successCount++;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"生成 {type.Name} 时出错: {ex.Message}");
                        errorCount++;
                    }
                }

                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("完成", $"生成完成！\n成功: {successCount} 个\n失败: {errorCount} 个\n\n文件保存在: {outputPath}", "确定");
            }
            catch (Exception ex)
            {
                Debug.LogError($"生成过程出错: {ex.Message}");
                EditorUtility.DisplayDialog("错误", $"生成过程出错: {ex.Message}", "确定");
            }
        }

        private bool GenerateIDataRowFile(System.Type type, string outputPath)
        {
            try
            {
                // 获取 Id 属性
                var idProperty = type.GetField("Id", BindingFlags.Public | BindingFlags.Instance);
                if (idProperty == null)
                {
                    Debug.LogWarning($"类型 {type.Name} 没有找到 Id 属性，跳过生成");
                    return false;
                }

                // 构建文件内容
                string content = BuildFileContent(type.Name, type.Namespace);

                // 确保输出目录存在
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                // 写入文件
                string fileName = $"{type.Name}.IDataRow.cs";
                string filePath = Path.Combine(outputPath, fileName);

                File.WriteAllText(filePath, content);
                Debug.Log($"已生成: {filePath}");

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"生成 {type.Name} 的文件时出错: {ex}");
                return false;
            }
        }

        private string BuildFileContent(string className, string namespaceName)
        {
            return $@"using GameFramework.DataTable;

namespace {namespaceName}
{{
    public sealed partial class {className} : IDataRow
    {{
        int IDataRow.Id => Id;

        public {className}()
        {{
        }}
    }}
}}";
        }

        private string GetOutputPath()
        {
            // 默认保存在 Assets 目录下
            string defaultPath = "Assets";

            // 检查是否已有自定义路径设置
            string savedPath = EditorPrefs.GetString("IDataRowGenerator_OutputPath", defaultPath);

            // 让用户选择路径
            string selectedPath = EditorUtility.SaveFolderPanel("选择输出目录", savedPath, "");

            if (string.IsNullOrEmpty(selectedPath) || !selectedPath.StartsWith(Application.dataPath))
            {
                return null;
            }

            // 转换为相对路径（相对于项目）
            selectedPath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
            EditorPrefs.SetString("IDataRowGenerator_OutputPath", selectedPath);
            return selectedPath;
        }
    }
}