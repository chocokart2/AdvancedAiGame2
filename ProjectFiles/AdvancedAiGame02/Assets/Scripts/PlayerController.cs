using UnityEngine;

public class PlayerController : MonoBehaviour, IShot
{
    [SerializeField] Transform playerCameraTransform;
    [SerializeField] GameObject bullet;

    [SerializeField] float cameraAngleValue;
    float cameraUpDown;
    float cameraUpDownMin = -80;
    float cameraUpDownMax = 80;
    float moveSpeed = 3.0f;

    public void Hit()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
        PlayerMove();
        Shot();
    }

    private void CameraControl()
    {
        float deltaX = Input.mousePositionDelta.x;
        float deltaY = -Input.mousePositionDelta.y;

        cameraUpDown += deltaY * cameraAngleValue;
        cameraUpDown = Mathf.Clamp(cameraUpDown, cameraUpDownMin, cameraUpDownMax);

        float cameraLeftRight = playerCameraTransform.localEulerAngles.y + deltaX * cameraAngleValue;

        // 카메라의 수직 회전만 처리
        playerCameraTransform.localEulerAngles = new Vector3(cameraUpDown, cameraLeftRight, 0f);

        //Vector3 angle = playerCameraTransform.eulerAngles;
        //angle += new Vector3(deltaY * cameraAngleValue, deltaX * cameraAngleValue, 0);
        //angle.x = Mathf.Clamp(angle.x, cameraUpDownMin, cameraUpDownMax);
        //Debug.Log($">> 앵글 : {angle.x}");


        //playerCameraTransform.rotation = Quaternion.Euler(angle);
        //playerCameraTransform.eulerAngles = angle;
    }
    private void PlayerMove()
    {
        Vector3 front = playerCameraTransform.forward;
        front.y = 0;
        front = front.normalized;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(front * moveSpeed * 2 * Time.deltaTime);
            }
            else
            {
                transform.Translate(front * moveSpeed * Time.deltaTime);
            }
        }
    }
    private void Shot()
    {
        if ((Input.GetMouseButtonDown(0) == false) && (Input.GetKeyDown(KeyCode.X) == false))
        {
            return;
        }
        
        GameObject bulletObj = Instantiate(bullet, transform.position + playerCameraTransform.forward * 2, Quaternion.identity);
        //bulletObj.transform.forward = playerCameraTransform.forward;
        bulletObj.GetComponent<Bullet>().direction = playerCameraTransform.forward;
    }
}
