using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdBoardController : MonoBehaviour
{
    [SerializeField] private Texture2D[] adTextures;
    [SerializeField] private float objectAspectRatio = 1;

    [SerializeField] private float adStayDuration = 10;
    [SerializeField] private float adLerpDuration = 1.5f;

    private int currentIndex;
    private bool lerping;
    private Lerper imageLerper;
    private TimeTracker imageStayTimer;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    
        imageStayTimer = new(adStayDuration, 1, true);
        imageStayTimer.onFinish += startImageLerp;
        imageStayTimer.Update(Random.Range(0, adStayDuration));
        imageLerper = new();
    }

    private void Start()
    {
        currentIndex = Random.Range(0, adTextures.Length);
        SwapImage();
        rend.material.SetFloat("_QuadAspectRatio", objectAspectRatio);
    }

    private void Update()
    {
        if(lerping)
        {
            float lerpState = imageLerper.Update(Time.deltaTime);
            rend.material.SetFloat("_BlendState", lerpState);
            if(lerpState >= 1)
            {
                lerping = false;
                SwapImage();
            }
        }
        else
        {
            imageStayTimer.Update(Time.deltaTime);
        }
    }

    private void SwapImage()
    {
        currentIndex = (currentIndex + 1) % adTextures.Length;

        rend.material.SetTexture("_ActiveAdTexture", adTextures[currentIndex]);
        rend.material.SetFloat("_BlendState", 0);
        rend.material.SetTexture("_AltAdTexture", adTextures[(currentIndex + 1) % adTextures.Length]);
    }

    private void startImageLerp()
    {
        lerping = true;
        imageLerper.SetValues(0, 1, adLerpDuration);
    }
}
