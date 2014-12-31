﻿using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ShareCenter : MonoBehaviour {

    //分享本地纹理
    public Texture2D texture;
    public StartUIController startUIController;
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

    public void Share()
    {
#if UNITY_EDITOR
        return;
#else
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
        
//        Social.SetTargetUrl("http://a.app.qq.com/o/simple.jsp?pkgname=com.ariequ.parachute");
        Social.SetTargetUrl("https://itunes.apple.com/cn/app/yi-lu-dao-di/id936408830?l=zh&ls=1&mt=8");
        
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
        string shareString = String.Format("我在#一撸到底#撸了{0}分，来试试吧,iOS点击图片下载", startUIController.CurrentScore.ToString());
        Social.OpenShareWithImagePath(platforms, shareString, Application.persistentDataPath + "/icon.png", callback);
#endif
    }
}
 
