using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Lazer : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private float startLineWidth = 0.1f;

    [Header("Visual Effect")]
    [SerializeField] private float minLife = 0.5f;
    [SerializeField] private float particleDisapearInterval = 0.1f;

    private float lifeTime;
    private float lifeRemain;

    private LineRenderer lineRend;
    private VisualEffect visEff;

    private void Awake()
    {
        lineRend = GetComponent<LineRenderer>();
        visEff = GetComponent<VisualEffect>();
    }
    
    public void ChangeLazerSettings(float minLife, float particleDisapearInterval)
    {
        this.minLife = minLife;
        this.particleDisapearInterval = particleDisapearInterval;
    }

    public void InitilizeLazer(Vector3 endPoint)
    {
        //set life
        lifeRemain = lifeTime = minLife + visEff.GetInt("Particle Per Unit") * particleDisapearInterval * Vector3.Distance(Vector3.zero, endPoint);

        //line renderer
        lineRend.SetPositions(new Vector3[]{ transform.position, transform.position + endPoint });
        lineRend.startWidth = lineRend.endWidth = startLineWidth;

        //visual effect
        visEff.SetFloat("Min Lifetime", minLife);
        visEff.SetFloat("Particle Disapear Interval", particleDisapearInterval);

        visEff.SetVector3("End Position", endPoint);

        visEff.Reinit();
        visEff.Play();
    }

    private void Update()
    {
        lineRend.startWidth = lineRend.endWidth = startLineWidth * (lifeRemain/lifeTime);

        //Life Countdown
        lifeRemain -= Time.deltaTime;

        if(lifeRemain <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
