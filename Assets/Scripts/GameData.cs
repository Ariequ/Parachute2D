using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameData
{
    private const string HIGHT_SCORE = "high_score";
    private const string DOG_COUNT = "cube_count";
    private const string SOUND_ENABLE = "SoundEnable";
    private int score;
    private bool isHight = false;

    public int DogCount
    {
        set
        {
            PlayerPrefs.SetInt(DOG_COUNT, value);
        }
        get
        {
            return PlayerPrefs.GetInt(DOG_COUNT);
        }
    }

    public int HighScore
    {
        set
        {
            PlayerPrefs.SetInt(HIGHT_SCORE, value);
        }
        get
        {
            return PlayerPrefs.GetInt(HIGHT_SCORE);
        }
    }

    public int Score
    {
        set
        {
            score = value;
            if (score > HighScore)
            {
                HighScore = score;
                isHight = true;
            }
        }
        get
        {
            return score;
        }
    }

    public bool isHighScore
    {
        get
        {
            return isHight;
        }
    }

    public bool SoundEnable
    {
        set
        {
            PlayerPrefs.SetInt(SOUND_ENABLE, value ? 0 : -1);
        }
        get
        {
            return PlayerPrefs.GetInt(SOUND_ENABLE) == 0;
        }
    }

#if UNITY_EDITOR
    [MenuItem("Tools/DeleteAllPlayerPrefs")]
    public static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
