using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundFadeEffect : MonoBehaviour
{
    [Min(0), SerializeField] private float transitionSpeed = 5;
    [Min(1), SerializeField] private float cycleHold = 10;
    private Image image;
    private TimeTracker holdTime;
    private TimeTracker transitionTime;

    private bool isTransitioning = false;


    private void Awake()
    {
        image = GetComponent<Image>();
        holdTime = new(cycleHold);
        holdTime.onFinish += () =>
        {
            isTransitioning = true;
            transitionTime.SetTimeScale(Mathf.RoundToInt(image.color.a) == 0 ? 1 : -1);
            transitionTime.Reset();
        };

        transitionTime = new(transitionSpeed);
        transitionTime.onFinish += () =>
        {
            isTransitioning = false;
            holdTime.Reset();
        };
    }

    private void OnEnable()
    {
        Randomise();
    }

    private void Randomise()
    {
        isTransitioning = DoStatic.RandomBool();
        image.color = new(1, 1, 1, Random.Range(0, 1));
        if (isTransitioning)
        {
            transitionTime.SetTimeScale(image.color.a == 0 ? 1 : -1);
            transitionTime.Reset();
            transitionTime.Update(Random.Range(0, transitionSpeed));
        }
        else
        {
            holdTime.Reset();
            holdTime.Update(Random.Range(0, cycleHold));
        }
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        if (isTransitioning)
        {
            transitionTime.Update(delta);
            image.color = new(1, 1, 1, transitionTime.TimePercentage);
        }
        else
        {
            holdTime.Update(delta);
        }
    }
}
