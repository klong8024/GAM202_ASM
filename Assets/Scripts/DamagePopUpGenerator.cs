using UnityEngine;
using TMPro;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator current;
    public GameObject popupPrefab;
    public Transform headPoint; //Bổ sung

    void Awake()
    {
        current = this;
    }

    public void CreatePopUp(Vector3 position, int damage)
    {
        Vector3 offset = new Vector3(
            Random.Range(-1.5f, 1.5f),
            Random.Range(0f, 1.5f),
            Random.Range(-1.5f, 1.5f)
        );

        GameObject popup = Instantiate(popupPrefab, position + offset, Quaternion.identity);

        TextMeshProUGUI text = popup.GetComponentInChildren<TextMeshProUGUI>();

        text.text = damage.ToString();

        popup.AddComponent<Billboard>(); //Bổ sung

        Destroy(popup, 1f);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     CreatePopUp(headPoint.position, 9999);
        // }
    }

    public class Billboard : MonoBehaviour
    {
        void LateUpdate()
        {
            if (Camera.main != null)
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
                //thay thế dòng trên bằng transform.LookAt(Camera.main.transform); nếu chữ bị ngược
        }
    }
}
