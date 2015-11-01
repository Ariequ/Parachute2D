using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ContinueUIController : MonoBehaviour
{
    public Text contentText;
    public Button comfirmBtn;

    public void UpdateUI(bool usingCoin,int cost)
    {
        if (usingCoin)
        {
            contentText.text = string.Format("使用{0}个邪眼狗狗继续游戏？", cost);
        }
        else
        {
            contentText.text = "看段广告，免费复活？";
            comfirmBtn.interactable = Advertisement.IsReady("rewardedVideo");
        }            
    }
}
