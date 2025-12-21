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
            EditorGUILayout.LabelField($"找到 {beanBaseTypes.Count} 个类型:", EditorStyles.boldLabel);

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

            // 生成单个文件按钮
            if (GUILayout.Button("生成到 Tables.IDataRow.cs", GUILayout.Height(30)))
            {
                GenerateSingleFile(beanBaseTypes);
            }
        }

        private void GenerateSingleFile(List<System.Type> typesToGenerate)
        {
            string outputPath = GetOutputPath();
            if (string.IsNullOrEmpty(outputPath))
            {
                return;
            }

            try
            {
                // 检查哪些类型有Id字段
                var validTypes = new List<System.Type>();
                var invalidTypes = new List<string>();

                foreach (var type in typesToGenerate)
                {
                    var idProperty = type.GetField("Id", BindingFlags.Public | BindingFlags.Instance);
                    if (idProperty != null)
                    {
                        validTypes.Add(type);
                    }
                    else
                    {
                        invalidTypes.Add(type.Name);
                    }
                }

                if (validTypes.Count == 0)
                {
                    EditorUtility.DisplayDialog("错误", "没有找到任何有效的类型（包含Id字段）", "确定");
                    return;
                }

                // 生成文件内容
                string content = BuildSingleFileContent(validTypes);

                // 确保输出目录存在
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                // 写入文件
                string fileName = "Tables.IDataRow.cs";
                string filePath = Path.Combine(outputPath, fileName);

                File.WriteAllText(filePath, content);
                Debug.Log($"已生成单个文件: {filePath}");
                Debug.Log($"包含 {validTypes.Count} 个类型的IDataRow实现");

                if (invalidTypes.Count > 0)
                {
                    Debug.LogWarning($"以下类型没有Id字段，已跳过: {string.Join(", ", invalidTypes)}");
                }

                AssetDatabase.Refresh();

                EditorUtility.DisplayDialog("完成",
                    $"已生成 Tables.IDataRow.cs 文件！\n" +
                    $"包含类型: {validTypes.Count} 个\n" +
                    $"跳过类型: {invalidTypes.Count} 个\n\n" +
                    $"文件保存在: {outputPath}",
                    "确定");
            }
            catch (Exception ex)
            {
                Debug.LogError($"生成过程出错: {ex.Message}\n{ex.StackTrace}");
                EditorUtility.DisplayDialog("错误", $"生成过程出错: {ex.Message}", "确定");
            }
        }

        private string BuildSingleFileContent(List<System.Type> types)
        {
            // 使用第一个类型的命名空间（假设所有类型都在同一个命名空间下）
            string namespaceName = types.FirstOrDefault()?.Namespace ?? "cfg";

            // 按类型名称排序
            types = types.OrderBy(t => t.Name).ToList();

            // 开始构建文件内容
            var content = new System.Text.StringBuilder();

            content.AppendLine("using GameFramework.DataTable;");
            content.AppendLine();
            content.AppendLine($"namespace {namespaceName}");
            content.AppendLine("{");

            // 为每个类型添加partial class实现
            foreach (var type in types)
            {
                content.AppendLine($"    public sealed partial class {type.Name} : IDataRow");
                content.AppendLine("    {");
                content.AppendLine("        int IDataRow.Id => Id;");
                content.AppendLine("    }");
                content.AppendLine();
            }

            // 移除最后多余的空行
            if (content.Length > 0)
            {
                content.Length -= 2;
            }

            content.AppendLine("}");

            return content.ToString();
        }

        private string GetOutputPath(bool useSavedPath = false)
        {
            // 默认保存在 Assets 目录下
            string defaultPath = "Assets";

            // 检查是否已有自定义路径设置
            string savedPath = EditorPrefs.GetString("IDataRowGenerator_OutputPath", defaultPath);

            if (useSavedPath && !string.IsNullOrEmpty(savedPath) && Directory.Exists(savedPath))
            {
                return savedPath;
            }

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