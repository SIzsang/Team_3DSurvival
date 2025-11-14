using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _02_Scripts.Utils
{
    public static class RuntimeInitializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeBeforeSceneLoad()
        {
#if UNITY_EDITOR
            string currentScenePath = SceneManager.GetActiveScene().path;

            if (currentScenePath != Constants.SceneLoader.BootstrapscenePath)
            {
                string previousScene = currentScenePath;
                EditorPrefs.SetString(Constants.SceneLoader.PreviousSceneKey, previousScene);

                string sceneName = System.IO.Path.GetFileNameWithoutExtension(previousScene);
                SceneLoader.SceneToLoadOverride = sceneName;

                SceneManager.LoadScene(System.IO.Path.GetFileNameWithoutExtension(Constants.SceneLoader.BootstrapscenePath), LoadSceneMode.Single);
            }
#endif
        }
    }
}