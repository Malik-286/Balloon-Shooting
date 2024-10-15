using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using System.Linq;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class PostProcessor : AssetPostprocessor
{
    private static readonly string googleMobileAdsPath = Application.dataPath + "/GoogleMobileAds";
    private static readonly string chartBoostAdsPath = Application.dataPath + "/Chartboost";
    private static readonly string googleMobileAdsDefine = "GOOGLE_MOBILE_ADS;";
    private static readonly string chartBoosteAdsDefine = "CHARTBOOST_ADS;";
    private static readonly string unityAdsDefine = "UNITY_ADS;";
    private static ListRequest Request;

    static void HandleReuqest()
    {
        if (Request.IsCompleted)
        {
            //Unity Package Manager Client Search

            string defines = "";

            if (Request.Result != null && Request.Result.ToList().Where(x => x.name == "com.unity.ads").FirstOrDefault() != null)
            {
                defines += unityAdsDefine;
            }

            if (System.IO.Directory.Exists(googleMobileAdsPath))
            {
                defines += googleMobileAdsDefine;
            }

            if (System.IO.Directory.Exists(chartBoostAdsPath))
            {
                defines += chartBoosteAdsDefine;
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, defines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, defines);

            EditorApplication.update -= HandleReuqest;
        }
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        Request = Client.List();

        EditorApplication.update -= HandleReuqest;
        EditorApplication.update += HandleReuqest;
    }
}