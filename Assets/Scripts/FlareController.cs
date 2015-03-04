using UnityEngine;
using System.Collections;

public class FlareController : MonoBehaviour
{
    public void OnAnimationComplete()
    {
        GameObject.Destroy(gameObject);
    }
}
