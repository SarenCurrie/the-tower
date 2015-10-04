using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public enum DOOR_ORIENTATION { TOP, BOTTOM, LEFT, RIGHT, DISABLED };
    public DOOR_ORIENTATION orientation;

    private const float DOOR_MOVEMENT_VALUE = 1.5f;

    private const float ROOM_WIDTH = 9.0f;
    private const float ROOM_HEIGHT = 5.4f;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.transform.tag == "Player")
        {
            //transform.parent.GetComponent<WorldManager>().DisableEnableEnemies();

            Vector3 locationOffset = new Vector3();
            Vector3 cameraOffset = new Vector3();
            switch (orientation)
            {
                case DOOR_ORIENTATION.BOTTOM:
                    locationOffset.y = -1;
                    cameraOffset.y = -ROOM_HEIGHT;
                    break;
                case DOOR_ORIENTATION.TOP:
                    locationOffset.y = 1;
                    cameraOffset.y = ROOM_HEIGHT;
                    break;
                case DOOR_ORIENTATION.LEFT:
                    locationOffset.x = -1;
                    cameraOffset.x = -ROOM_WIDTH;
                    break;
                case DOOR_ORIENTATION.RIGHT:
                    locationOffset.x = 1;
                    cameraOffset.x = ROOM_WIDTH;
                    break;
            }
            other.gameObject.transform.position += locationOffset * DOOR_MOVEMENT_VALUE;
            Camera.main.transform.position += cameraOffset;
        }

    }
}
