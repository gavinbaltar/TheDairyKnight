/* Name: Bryan Bong
 * Filename: AutomaticBackgroundScroll.cs
 * Description: A script to have the background scroll automatically over time.
 * Tutorial: https://www.youtube.com/watch?v=-6H-uYh80vc
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticBackgroundScroll : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float x, y;

    private void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x, y) * Time.deltaTime, img.uvRect.size);
    }

}
