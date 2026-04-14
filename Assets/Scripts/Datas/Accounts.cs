using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class Accounts : MonoBehaviour
{
    public TMP_InputField idInput;
    public void GetAllAccounts()
    {
        StartCoroutine(GetAll());
    }

    public void GetAccountById()
    {
        StartCoroutine(GetById());
    }

    IEnumerator GetAll()
    {
        var url = "http://localhost:5034/api/GetAllAccs";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                ResponseAccountList response = JsonUtility.FromJson<ResponseAccountList>(request.downloadHandler.text);
                Debug.Log("Response: " + request.downloadHandler.text);
                Debug.Log("Notification: " + response.Notification);

                if (response.isSuccess)
                {
                    foreach (var acc in response.Data)
                    {
                        Debug.Log("Account: " + acc.Name + " (" + acc.Email + ")");
                    }
                }
                else
                {
                    Debug.LogError("Failed to get account!");
                }
            }
        }
    }

    IEnumerator GetById()
    {
        var id = idInput.text;
        var url = "http://localhost:5034/api/GetAccById/" + id;

        Debug.Log(url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log(request.error);
            else
            {
                Response response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
                Debug.Log("Response: " + request.downloadHandler.text);
                Debug.Log("Notification: " + response.Notification);

                if (response.isSuccess)
                {
                    Debug.Log(response.Data.Email);
                    Debug.Log(response.Data.Name);
                }
                else
                {
                    Debug.Log("Failed to get account data!");
                }
            }
        }

    }
}
