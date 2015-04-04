using UnityEngine;
using System.Collections;

public class e01_a1_r2 : MonoBehaviour {

    /**
     * Static Event 01 - Act 1 Room 2
     * 
     * This event is activated once the player obtains the boots.
     * This event is triggered by the collision with the trigger
     * This event should only ever occur a total of One Times
     * Upon Trigger This event:
     *      - Locks Player Input
     *      - Freezes the Player
     *      // - Moves Player to ground Y Position at current X
     *      - Spawns the Spinning DeathBall at Y ground position off camera right
     *      - Triggers deathball to roll on collision course with the player
     *      - Collision will kill the player's adventurer
     *      - disable player Adventurer sprite
     *      - trigger after death animation
     *      - Fire chat bubble message "Whfoof! Talk about squished, Brains everywhere!"     
     */

    public bool isEventActive = false;
    public int eventFiredCount = 0;
	private BoxCollider2D actionTrigger;

	public GameObject spinningDeathBall;

	// Use this for initialization
	void Start ()
    {
	
		transform.position = new Vector3(13f, 8f, 0f);
		transform.localScale = new Vector3(15f, 1f, 3f);
		actionTrigger = gameObject.AddComponent<BoxCollider2D>();
		actionTrigger.isTrigger = true;
		actionTrigger.enabled = true;
		actionTrigger.offset = new Vector2(0f, 0.5f);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_.playerInventory.hasBoots && eventFiredCount == 0)
        {
            isEventActive = true;
        } else {
			isEventActive = false;
		}

        if (isEventActive)
        {
			actionTrigger.enabled = true;
        }
        else
        {
			actionTrigger.enabled = false;
        }
	}
	

    void OnTriggerEnter2D(Collider2D col)
    {
		if (isEventActive)
		{
			eventFiredCount++;

			// Lock character input and motion
			CharacterMotion cm = _.player.GetComponent<CharacterMotion>();
			cm.characterInput.inputLock(true);
			cm.StopAllMotion();

			// send a chat message
			_.chatManager.GetComponent<ChatManager>().CreateChatMessage(
				_.player.transform.position + new Vector3(0f, 1f, -.25f),
				"What's that noise?"
				);

			GameObject prefab = Resources.Load("Prefabs/AnimatedTiles/DeathBall") as GameObject;
			spinningDeathBall = Instantiate(prefab, _.player.transform.position + new Vector3(35f, 1.32f, 0f), Quaternion.identity) as GameObject;
			spinningDeathBall.GetComponent<Animator>().SetBool("isRolling", true);

		}
    }

}
