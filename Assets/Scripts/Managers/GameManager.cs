using UnityEngine;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(PoolManager))]
[RequireComponent(typeof(SaveManager))]
[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(VariableManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public static AudioManager AudioManager { get; private set; }
    public static PoolManager PoolManager { get; private set; }
    public static SaveManager SaveManager { get; private set; }
    public static LevelManager LevelManager { get; private set; }
    public static InputManager InputManager { get; private set; }
    public static VariableManager VariableManager { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        AudioManager = GetComponent<AudioManager>();
        PoolManager = GetComponent<PoolManager>();
        SaveManager = GetComponent<SaveManager>();
        LevelManager = GetComponent<LevelManager>();
        InputManager = GetComponent<InputManager>();
        VariableManager = GetComponent<VariableManager>();

        foreach (IManager manager in GetComponents<IManager>())
        {
            manager.StartUp();
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void Save()
    {
        //Grab variables from VariableManager and save them here.
        SaveManager.SaveToFile();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
