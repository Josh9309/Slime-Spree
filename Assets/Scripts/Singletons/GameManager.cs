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
    public List<GameObject> players = new List<GameObject>();
    public GameObject Goal;
    #endregion

    #region Properties
    #endregion

    protected GameManager() { }

    void Awake()
    {
        //make players
        players.Clear();
        players.Add((GameObject)Instantiate(Resources.Load("Prefabs/RedSlime", typeof(GameObject)), new Vector3(-2, 2, 0), Quaternion.identity));
        players.Add((GameObject)Instantiate(Resources.Load("Prefabs/BlueSlime", typeof(GameObject)), new Vector3(2, -2, 0), Quaternion.identity));
        players.Add((GameObject)Instantiate(Resources.Load("Prefabs/GreenSlime", typeof(GameObject)), new Vector3(2, 2, 0), Quaternion.identity));
        players.Add((GameObject)Instantiate(Resources.Load("Prefabs/YellowSlime", typeof(GameObject)), new Vector3(-2, -2, 0), Quaternion.identity));



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
        LevelManager.Instance.SpawnEnemies(10, 5);
    }



    void Update()
    {

    }
}
