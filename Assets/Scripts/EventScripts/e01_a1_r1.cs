using UnityEngine;
using System.Collections;

public class e01_a1_r1 : MonoBehaviour {

	public bool isEventActive = true;
    public int eventFiredCount = 0;
	private BoxCollider2D actionTrigger;

	// Use this for initialization
	void Start ()
    {
	
		transform.position = new Vector3(17f, 3f, 0f);
		transform.localScale = new Vector3(2f, 3f, 1f);
		actionTrigger = gameObject.AddComponent<BoxCollider2D>();
		actionTrigger.isTrigger = true;
		actionTrigger.enabled = true;
		actionTrigger.offset = new Vector2(0f, 0.5f);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if ( eventFiredCount == 0 )
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
		if (isEventActive && col.name == "Player")
		{
			eventFiredCount++;

			// send a chat message
			_.chatManager.GetComponent<ChatManager>().CreateChatMessage(
				_.player.transform.position + new Vector3(0f, 1f, -.25f),
				"If only I knew how to [JUMP]..."
				);

		}
    }

}
