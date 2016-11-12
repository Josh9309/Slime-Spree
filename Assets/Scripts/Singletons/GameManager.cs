using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles main play cycle logic. Anything within game manager should happen during gameplay
/// and terminate when advancing to other menus.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region Fields
    //Assigned in inspector
    public List<PlayerSlime> players = new List<PlayerSlime>();
    public GameObject Goal;
    #endregion

    #region Properties
    #endregion

    protected GameManager() { }

    void Awake()
    {
        //set goal
        Goal = (GameObject)Instantiate(Goal, new Vector3(0, 0, 0), Quaternion.identity);

        //start game
        StartGame();
    }

    public void StartGame()
    {
        //MenuManager.Instance.GoToScreen("GameScreen");

        // spawn the first wave
        StartRound();
    }

    /// <summary>
    /// Will be called in other places later on
    /// </summary>
    public void StartRound()
    {
        // spawn the first wave
        LevelManager.Instance.SpawnEnemies();
    }



    void Update()
    {


    }
}
