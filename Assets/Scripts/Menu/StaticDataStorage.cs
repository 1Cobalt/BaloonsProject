using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class StaticDataStorage
{
    private static bool[] levelIsCompleted = new bool[30]
    {false, false, false,false,false,false,false,false,false,false
    ,false,false,false,false,false,false,false,false,false,false
    ,false,false,false,false,false,false,false,false,false,false};

    private static bool isSoundsOn = true;

    private static uint bulletsShot = 0;
    private static uint baloonsDestroyed = 0;

    private static bool lvl5bossIsDestroyed = false;

    private static bool lvl2isBeatenPerfectly = false;

    public static bool GetLevelCompleteStatus(uint levelNumber)
    {
        return levelIsCompleted[levelNumber];
    }

    public static void SetLevelCompleteStatus(uint levelNumber, bool status)
    {
        levelIsCompleted[levelNumber] = status;
    }

    public static uint GetBulletsShot()
    {
        return bulletsShot;
    }
    public static void SetBulletsShot(uint _bulletsShot)
    {
        bulletsShot = _bulletsShot;
    }

    public static void PlayerShot()
    {
        bulletsShot++;
    }

    public static uint GetBaloonDestroyed()
    {
        return baloonsDestroyed;
    }
    public static void SetBaloonDestroyed(uint _baloonsDestroyed)
    {
        baloonsDestroyed = _baloonsDestroyed;
    }

    public static void BaloonDestroyed()
    {
        baloonsDestroyed++;
    }

    public static bool GetBossStatus()
    { 
        return lvl5bossIsDestroyed;
    }

    public static void SetBossStatus(bool _bossStatus)
    {
        lvl5bossIsDestroyed = _bossStatus;
    }

    public static void BossIsDestroyed()
    {
        lvl5bossIsDestroyed = true;
    }

    public static bool GetLevelStatus()
    {
        return lvl2isBeatenPerfectly;
    }

    public static void SetLevelStatus(bool _levelStatus)
    {
        lvl2isBeatenPerfectly = _levelStatus;
    }

    public static void LevelIsBeaten()
    {
        lvl2isBeatenPerfectly = true;
    }

    public static void SetSoundsStatus(bool setSound)
    {
        isSoundsOn = setSound;
    }

    public static bool GetSoundsStatus()
    {
        return isSoundsOn;
    }


}
