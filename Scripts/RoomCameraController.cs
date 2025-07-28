using UnityEngine;
using System.Collections;

public class RoomCameraController : MonoBehaviour
{
    public Transform player;
    public Vector2 roomSize = new Vector2(16, 9); // Kích thước mỗi phòng (theo Tilemap)
    public float moveDuration = 0.4f; // Thời gian trượt camera (0 để nhảy ngay)

    private Vector2Int currentRoom;
    private bool isMoving = false;

    void Start()
    {
        currentRoom = GetPlayerRoom();
        SnapCameraToRoom(currentRoom);
    }

    void LateUpdate()
    {
        if (isMoving) return;

        Vector2Int playerRoom = GetPlayerRoom();
        if (playerRoom != currentRoom)
        {
            currentRoom = playerRoom;

            if (moveDuration > 0)
                StartCoroutine(SmoothMoveCameraToRoom(currentRoom));
            else
                SnapCameraToRoom(currentRoom);
        }
    }

    // Lấy phòng hiện tại của người chơi (tọa độ phòng trên lưới)
    Vector2Int GetPlayerRoom()
    {
        return new Vector2Int(
            Mathf.FloorToInt(player.position.x / roomSize.x),
            Mathf.FloorToInt(player.position.y / roomSize.y)
        );
    }

    // Dịch chuyển tức thì
    void SnapCameraToRoom(Vector2Int room)
    {
        transform.position = new Vector3(
            room.x * roomSize.x + roomSize.x / 2f,
            room.y * roomSize.y + roomSize.y / 2f,
            transform.position.z
        );
    }

    // Dịch chuyển mượt
    IEnumerator SmoothMoveCameraToRoom(Vector2Int room)
    {
        isMoving = true;
        Vector3 start = transform.position;
        Vector3 end = new Vector3(
            room.x * roomSize.x + roomSize.x / 2f,
            room.y * roomSize.y + roomSize.y / 2f,
            transform.position.z
        );

        float elapsed = 0;
        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        isMoving = false;
    }
}
