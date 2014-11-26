/// <summary>
/// Sound manager.
/// This script use for manager all sound(bgm,sfx) in game
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
	
	[System.Serializable]
	public class SoundGroup{
		public AudioClip audioClip;
		public string soundName;
	}
	
	public AudioSource bgmSound;
	
	public List<SoundGroup> sound_List = new List<SoundGroup>();
	
	public static SoundManager instance;
	
	public void Start(){
		instance = this;	
        DontDestroyOnLoad(gameObject);
	}
	
	public void PlayingSound(string _soundName, float volume, Vector3 position){
        AudioSource.PlayClipAtPoint(sound_List[FindSound(_soundName)].audioClip, position, volume);
	}
	
	private int FindSound(string _soundName){
		int i = 0;
		while( i < sound_List.Count ){
			if(sound_List[i].soundName == _soundName){
				return i;	
			}
			i++;
		}
		return i;
	}
	
	//Start BGM when loading complete
	public void startBGM()
	{
		bgmSound.Play();
	}

    public void stopBMG()
    {
        bgmSound.Stop();
    }
	
}
