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
    public float speed;

    [SerializeField] private RawImage image;
    [SerializeField] private float x, y;

    private void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x, y) * Time.deltaTime, image.uvRect.size);
    }

}
