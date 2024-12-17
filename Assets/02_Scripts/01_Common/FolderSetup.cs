using UnityEditor;
using UnityEngine;

public class FolderSetup : EditorWindow
{
    [MenuItem("Tools/Setup Folders")]
    static void SetupFolders()
    {
        // 기본 폴더 리스트
        string[] folderNames = new string[]
        {
            "Assets/01_Scenes",
            "Assets/02_Scripts",
            "Assets/02_Scripts/01_Common",
            "Assets/02_Scripts/Editor",
            "Assets/02_Scripts/02_Scriptable",
            "Assets/03_Art/Textures",
            "Assets/03_Art/Models",
            "Assets/03_Art/Animations", 
            "Assets/04_Prefabs",
            "Assets/05_Audio",
            "Assets/Resources",
            "Assets/StreamingAssets",
        };

        // 폴더가 없다면 생성
        foreach (string folder in folderNames)
        {
            string[] folderParts = folder.Split('/'); // 폴더 경로를 '/'로 나눔
            string currentPath = "Assets"; // Assets 폴더부터 시작

            // 루트부터 차례대로 폴더 생성
            for (int i = 1; i < folderParts.Length; i++) // 첫 번째 "Assets"는 제외하고 처리
            {
                currentPath = currentPath + "/" + folderParts[i]; // 현재 폴더 경로 추가

                // 해당 폴더가 없으면 생성
                if (!AssetDatabase.IsValidFolder(currentPath))
                {
                    string parentFolder = currentPath.Substring(0, currentPath.LastIndexOf('/')); // 부모 폴더 경로 추출
                    string newFolderName = folderParts[i]; // 새로 만들 폴더 이름

                    // 부모 폴더가 존재할 때만 폴더를 생성
                    AssetDatabase.CreateFolder(parentFolder, newFolderName);
                }
            }
        }

        // 폴더 생성 후 AssetDatabase 업데이트
        AssetDatabase.Refresh();
    }
}
