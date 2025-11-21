//using _02_Scripts.Utils;
//using UnityEditor;
//using UnityEditor.SceneManagement;

//namespace Editor
//{
//    [InitializeOnLoad]
//    public static class BootstrapSceneLoader
//    {

//        static BootstrapSceneLoader()
//        {
//            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
//        }

//        private static void OnPlayModeStateChanged(PlayModeStateChange state)
//        {

//            if (state == PlayModeStateChange.ExitingEditMode)
//            {
//                EditorPrefs.SetString(Constants.SceneLoader.PreviousSceneKey, EditorSceneManager.GetActiveScene().path);
//            }
//            if (state == PlayModeStateChange.EnteredEditMode)
//            {
//                string previousScenePath = EditorPrefs.GetString(Constants.SceneLoader.PreviousSceneKey);
//                if (!string.IsNullOrEmpty(previousScenePath))
//                {
//                    EditorSceneManager.OpenScene(previousScenePath);
//                    EditorPrefs.DeleteKey(Constants.SceneLoader.PreviousSceneKey);
//                }
//            }
//        }
//    }
//}