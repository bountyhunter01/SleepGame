using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private PlayerMove2D player;
    private CameraPlayerMode theCamera;


    private void Start()
    {

        player = FindObjectOfType<PlayerMove2D>();

        player.transform.position = this.transform.position;

        theCamera = FindObjectOfType<CameraPlayerMode>();
        theCamera.transform.position = new Vector3(transform.position.x,
            transform.position.y, theCamera.transform.position.z);



    }

}
