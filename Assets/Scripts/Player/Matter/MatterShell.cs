using UnityEngine;

public class MatterShell : MonoBehaviour
{
    [SerializeField] private int shellMatterCount;

    [SerializeField] private Matter[] matterObjectPrefabs;

    private Matter[] activeMatters;

    private void Start()
    {
        if (matterObjectPrefabs.Length == 0)
        {
            return;
        }

        activeMatters = new Matter[shellMatterCount];
        for (int i = 0; i < shellMatterCount; i++)
        {
            activeMatters[i] = SpawnMatter();
        }

        //Temporary test code
        GameManager.LevelManager.ActiveSceneController.SetMatterShell(this);
    }

    private Matter SpawnMatter()
    {
        Matter newMatter = Instantiate<Matter>(matterObjectPrefabs[Random.Range(0, matterObjectPrefabs.Length)], transform.position, Quaternion.identity, transform);
        newMatter.InitilizeMatter();
        return newMatter;
    }

    public void WeaponizeMatter()
    {
        for (int i = 0; i < activeMatters.Length; i++)
        {
            activeMatters[i].Weaponize();
        }
    }
}
