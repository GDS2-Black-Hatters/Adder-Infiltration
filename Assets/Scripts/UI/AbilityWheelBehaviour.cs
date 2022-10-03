using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;
using static ActionInputSubscriber.CallbackContext;
using System.Collections;

public class AbilityWheelBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject abilityIconPrefab;
    private List<AbilityPivotBehaviour> pivotBehaviours = new();

    [SerializeField] private int numberOfIconsShown = 3;
    [SerializeField] private float rotationTime = 0.1f;
    public static bool IsRotating { get; private set; } = false;
    private float rotationStep;
    private AbilityPivotBehaviour currentAbility;

    private void Start()
    {
        numberOfIconsShown += 1 - numberOfIconsShown & 1; //& 1 checks for odd and return 1 if true. Much faster than mod.
        //Add scrolling input delegate
        gameObject.AddComponent<ActionInputSubscriber>().AddActions(new()
        {
            new(MainGameScroll, Performed, Scroll),
            new(MainGameAbility, Performed, UseAbility),
        });

        //Calculate constant rotation
        rotationStep = 120 / (numberOfIconsShown + 1);

        //Spawn numIcons + 2 with correct rotation
        for (int i = 0; i < numberOfIconsShown + 2; i++)
        {
            Transform child = Instantiate(abilityIconPrefab, transform).transform;
            pivotBehaviours.Add(child.GetComponent<AbilityPivotBehaviour>());

            Vector3 rot = new()
            {
                z = -60 + rotationStep * i,
            };
            child.eulerAngles = rot;
        }
    }

    private void Scroll(InputAction.CallbackContext callback)
    {
        float value = callback.ReadValue<float>();
        if (IsRotating || value == 0) { return; }
        StartCoroutine(Scroll(value < 0));
    }

    private void UseAbility(InputAction.CallbackContext callback)
    {
        if (currentAbility)
        {
            currentAbility.ActivateAbility();
        }
    }

    private IEnumerator Scroll(bool scrollDown)
    {
        IsRotating = true;
        TimeTracker time = new(rotationTime, 1);
        time.onFinish += () => { IsRotating = false; };
        time.onFinish += () =>
        {
            transform.eulerAngles = new();
            //TODO: Update all icons in wheel
        };
        //TODO: Add delegate of resetting rotation but updating the icons once finished.
        do
        {
            yield return null;
            time.Update(Time.deltaTime);
            //TODO: Update icon sizes.
            //TODO: Update rotation.
        } while (IsRotating);
    }
}
