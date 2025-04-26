/* Name: Bryan Bong
 * Filename: AutomaticBackgroundScroll.cs
 * Description: A script to have the background scroll automatically over time.
 * Tutorial: https://www.youtube.com/watch?v=Wz3nbQPYwss
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticBackgroundScroll : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Renderer bgRenderer;

    private void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }

}
