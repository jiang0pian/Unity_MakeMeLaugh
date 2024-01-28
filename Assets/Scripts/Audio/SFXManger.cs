using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManger : MonoBehaviour
{
    public static SFXManger Instance;
    private void Awake()
    {
        Instance = this;
    }
    public List<AudioSource> soundEffects;

    public void PlaySFX(SFX soundEffect)
    {
        soundEffects[(int)soundEffect].Stop();
        soundEffects[(int)soundEffect].Play();
    }
    public void PlaySFXPiched(SFX soundEffect, float min, float max)
    {
        soundEffects[(int)soundEffect].pitch = UnityEngine.Random.Range(min, max);
        PlaySFX(soundEffect);
    }    
}
public enum SFX
{
    collectExp,
    collectCoin,
    enemyDie,
    enemyGetHit,
    playerLevelup,
    weaponFire,
    weponReloadPutIn,
    weponReloadPutOut,
    chooseUpGrade,
    playerGetHit,
    dropReward
}
