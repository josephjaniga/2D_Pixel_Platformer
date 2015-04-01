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
     *      - Moves Player to ground Y Position at current X
     *      - Spawns the Spinning DeathBall at Y ground position off camera right
     *      - Triggers deathball to roll on collision course with the player
     *      - Collision will kill the player's adventurer
     *      - disable player Adventurer sprite
     *      - trigger after death animation
     *      - Fire chat bubble message "Whfoof! Talk about squished, Brains everywhere!"     
     */

    public bool isEventActive = false;
    public int eventFiredCount = 0;
    public Collider2D eventTrigger;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_.playerInventory.hasBoots && eventFiredCount == 0)
        {
            isEventActive = true;
        }

        if (isEventActive)
        {
            eventTrigger.enabled = true;
        }
        else
        {
            eventTrigger.enabled = false;
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isEventActive)
        {
            eventFiredCount++;
            // do the event stuff
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // should this be a trigger???
    }

}
