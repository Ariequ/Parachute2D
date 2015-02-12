using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlaceCoin : MonoBehaviour 
{
    [MenuItem("Tools/PlaceCoin")]
    public static void Place()
    {
        GameObject[] coins = Selection.gameObjects;

        foreach(GameObject coin in coins)
        {
            CoinController[] controllers = coin.transform.GetComponentsInChildren<CoinController>();
            foreach(CoinController controller in controllers)
            {
                int x = Mathf.RoundToInt(controller.transform.position.x / (6.4f / 7));
                controller.transform.position = new Vector3(x * (6.4f / 7), Mathf.RoundToInt(controller.transform.position.y), 0);
            }

        }
    }
}
