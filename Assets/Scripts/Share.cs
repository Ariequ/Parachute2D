using UnityEngine;
using System.Collections;
using System.IO;

    //using UnityEditor;
    //using UnityEditor.Callbacks;
	
    //public class MyBuildPostprocessor {
    //    [PostProcessBuild]
    //    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
    //        Debug.Log( pathToBuiltProject );
    //        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!");
    //    }
    //}

public class Share : MonoBehaviour {

    //分享本地纹理
    public Texture2D texture;
	void Start () {
#if UNITY_EDITOR
		this.enabled = false;
		#elif UNITY_ANDROID
        //设置Umeng Appkey
		Social.SetAppKey("5479f120fd98c577ba000309");
#elif UNITY_IOS
		Social.SetAppKey("547b2189fd98c5437e000ce2");
#endif
	}



    void OnGUI()
    {


        if (GUI.Button(new Rect(150, 100, 500, 100), "打开分享面板"))
        {
            //按需设置您需要的平台
			Platform[] platforms = {Platform.WEIXIN_CIRCLE, Platform.WEIXIN, Platform.SINA, Platform.DOUBAN};

//            //接入twitter
//            Social.OpenTwitter();
//            //接入instagram
//            Social.OpenInstagram();
//            //接入QQ
//            Social.SetQQAppIdAndAppKey("100424468", "c7394704798a158208a74ab60104f0ba");
            //接入微信
			Social.SetWechatAppId("wx6dc874911aca6ad5");
		
			Social.SetTargetUrl("ariequ.github.io");


           
            

            //设置分享回调
            //如果无需回调 OpenShareWithImagePath最后一个参数可不填
            Social.ShareDelegate callback =
                delegate(Platform platform, int stCode, string errorMsg)
                {
                    if (stCode == Social.SUCCESS)
                    {
                        Debug.Log("分享成功");
                    }
                    else
                    {
                        Debug.Log("分享失败" + errorMsg);
                    };
                };



            
           


            //分享纹理
            //纹理 需要为 ARGB32 and RGB24 格式. 而且纹理的import settings/Advanced下的"Read/Write Enabled"需要勾选
            File.WriteAllBytes(Application.persistentDataPath + "/icon.png",texture.EncodeToPNG());
            //打开分享面版
            Social.OpenShareWithImagePath(platforms, "HelloWorld", Application.persistentDataPath + "/icon.png", callback);



        }
        if (GUI.Button(new Rect(150, 300, 500, 100), "授权"))
        {
            //接入QQ
            Social.SetQQAppIdAndAppKey("100424468", "c7394704798a158208a74ab60104f0ba");
            //授权
            Social.Authorize(Platform.QQ, delegate(Platform platform, int stCode, string usid, string token)
            {
                if (stCode == Social.SUCCESS)
                {
                    Debug.Log("授权成功" + "usid:" + usid + "token:" + token);
                }
                else
                {
                    Debug.Log("授权失败");
                }
            });
            
        }

        if (GUI.Button(new Rect(150, 500, 500, 100), "直接分享"))
        {
            //接入QQ
            Social.SetQQAppIdAndAppKey("100424468", "c7394704798a158208a74ab60104f0ba");

            Social.ShareDelegate callback =
                delegate(Platform platform, int stCode, string errorMsg)
                {
                    if (stCode == Social.SUCCESS)
                    {
                        Debug.Log("直接分享");
                    }
                    else
                    {
                        Debug.Log("直接分享" + errorMsg);
                    };
                };
            //截屏
            Application.CaptureScreenshot("Sceenshot.png");
            //分享
            Social.DirectShare(Platform.QQ, "Hello World", Application.persistentDataPath + "/Sceenshot.png", callback);
        }
    }


	
}
 
