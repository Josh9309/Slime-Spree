using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class SlimeShot : MonoBehaviour {
    #region Attributes
    private int damage;
    private Vector3 slimeShotTarget;
    private GameObject player;
    private PlayerSlime slimePlayerScript;
    [SerializeField] private PlayerSlime.SlimeType slimeType;
    private Rigidbody2D rBody;
    #endregion
    // Use this for initialization
    void Start () {
        switch (slimeType)
        {
            case PlayerSlime.SlimeType.BLUE:
                player = GameObject.Find("BlueSlime");
                slimePlayerScript = player.GetComponent<PlayerSlime>();
                break;

            case PlayerSlime.SlimeType.RED:
                player = GameObject.Find("RedSlime");
                slimePlayerScript = player.GetComponent<PlayerSlime>();
                break;

            case PlayerSlime.SlimeType.GREEN:
                player = GameObject.Find("GreenSlime");
                slimePlayerScript = player.GetComponent<PlayerSlime>();
                break;

            case PlayerSlime.SlimeType.YELLOW:
                player = GameObject.Find("BlueSlime");
                slimePlayerScript = player.GetComponent<PlayerSlime>();
                break;
        }
        damage = slimePlayerScript.SlimeShotDamage;
        slimeShotTarget = player.transform.Find("Reticle").position;

        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Fire()
    {
        //
    }
}
