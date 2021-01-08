﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using EdgeFramework;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace EdgeFrameworkEditor
{
    class MenumanagerEditor
    {
        #region OpenTool
        [MenuItem(Constants.ProductName + "/" + Constants.OpenTool + "/AbConfig", false)]
        public static void MenuOpenAbConfig()
        {
            ABConfig instance = ABConfig.LoadAbConfig();
            if (instance == null)
            {

                // Create Resources folder if it doesn't exist.
                Constants.ResourcesFolder.CreateDirIfNotExists();
                // Now create the asset inside the Resources folder.
                instance = ABConfig.Instance; // this will create a new instance of the EMSettings scriptable.
                AssetDatabase.CreateAsset(instance, Constants.AbConfigPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("AbConfig.asset was created at " + Constants.AbConfigPath);
            }
            Selection.activeObject = instance;
        }

        [MenuItem(Constants.ProductName + "/" + Constants.OpenTool + "/RealFramConfig", false)]
        public static void MenuOpenRealFramConfig()
        {
            RealFramConfig instance = RealFramConfig.LoadRealFramConfig();
            if (instance == null)
            {

                // Create Resources folder if it doesn't exist.
                Constants.ResourcesFolder.CreateDirIfNotExists();
                // Now create the asset inside the Resources folder.
                instance = RealFramConfig.Instance; // this will create a new instance of the EMSettings scriptable.
                AssetDatabase.CreateAsset(instance, Constants.RealFramConfigPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("RealFramConfig.asset was created at " + Constants.RealFramConfigPath);
            }
            Selection.activeObject = instance;
        }
        [MenuItem(Constants.ProductName + "/" + Constants.OpenTool + "/OpenPanelExcel", false)]
        public static void MenuOpenPanelJson()
        {
            string datapath = Application.dataPath.Replace("Assets", "");
            string panelpath = datapath + Constants.UIPanelExcel;

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = panelpath;
            p.Start();
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        }
        //[MenuItem(Constants.ProductName + "/" + Constants.OpenTool + "/OpenPanelJson", false)]
        //public static void MenuOpenPanelJson()
        //{
        //    string datapath = Application.dataPath.Replace("Assets", "");
        //    string panelpath = datapath + Constants.UIJsonPath;

        //    System.Diagnostics.Process p = new System.Diagnostics.Process();
        //    p.StartInfo.FileName = panelpath;
        //    p.Start();
        //    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        //}
        //[MenuItem(Constants.ProductName + "/" + Constants.OpenTool + "/OpenAudioConfig", false)]
        //public static void MenuOpenAudioConfig()
        //{
        //    string datapath = Application.dataPath.Replace("Assets", "");
        //    string panelpath = datapath + Constants.AudioConfig;

        //    System.Diagnostics.Process p = new System.Diagnostics.Process();
        //    p.StartInfo.FileName = panelpath;
        //    p.Start();
        //    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        //}


        #endregion

        #region AssetBundle
        [MenuItem(Constants.ProductName + "/" + Constants.AssetsBundle + "/BuildApp")]
        public static void Build()
        {
            BundleEditor.Build();
            BuildApp.Build();
        }
        [MenuItem(Constants.ProductName + "/" + Constants.AssetsBundle + "/BuildBundle")]
        public static void NormalBuild()
        {
            BundleEditor.Build();
        }
        [MenuItem(Constants.ProductName + "/" + Constants.AssetsBundle + "/打包热更包")]
        public static void OpenHotfix()
        {
            BundleHotFix.Init();
        }
        [MenuItem(Constants.ProductName + "/" + Constants.AssetsBundle + "/SaveVersion")]
        public static void SaveVersion()
        {
            BuildApp.SaveVersion(PlayerSettings.bundleVersion, PlayerSettings.applicationIdentifier);
        }
        #endregion

        #region Encryption

        [MenuItem(Constants.ProductName + "/" + Constants.Encryption +"/ 加密AB包")]
        public static void EncryptAB()
        {
            DirectoryInfo directory = new DirectoryInfo(BundleEditor.bundleTargetPath);
            FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Name.EndsWith("meta") && !files[i].Name.EndsWith(".manifest"))
                {
                    AES.AESFileEncrypt(files[i].FullName,Constants.AESKEY);
                }
            }
            Debug.Log("加密完成！");
        }

        [MenuItem(Constants.ProductName + "/" + Constants.Encryption + "/解密AB包")]
        public static void DecrptyAB() 
        {
            DirectoryInfo directory = new DirectoryInfo(BundleEditor.bundleTargetPath);
            FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                if (!files[i].Name.EndsWith("meta") && !files[i].Name.EndsWith(".manifest"))
                {
                    AES.AESFileDecrypt(files[i].FullName, "liangsheng");
                }
            }
            Debug.Log("解密完成！");
        }
        #endregion

        #region  Sheet
        [MenuItem(Constants.ProductName + "/" + Constants.Sheet + "/ExportBytes")]
        private static void ExportBytes()
        {
            SheetEditor.ExportBytes();
        }
        #endregion

        private static string START_SCENE = "Assets/Scenes/StartScene.unity";
        /// <summary>
        /// 游戏开始快捷键
        /// </summary>
        [MenuItem(Constants.ProductName+ "/Play _F5")]
        private static void Play()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                AssetDatabase.Refresh();
                Scene activeScene = EditorSceneManager.GetActiveScene();
                if (!string.IsNullOrEmpty(activeScene.path) && (activeScene.path != START_SCENE)) EditorSceneManager.SaveOpenScenes();
                EditorSceneManager.OpenScene(START_SCENE);
                EditorApplication.isPlaying = true;
            }
        }
    }
   
}