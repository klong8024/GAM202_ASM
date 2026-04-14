using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField nameInput;

    public void GoToLogin()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
    }

    public void OnRegisterClick()
    {
        var email = emailInput.text;
        var password = passwordInput.text;
        var name = nameInput.text;

        var account = new RegisterRequest
        {
            Email = email,
            Password = password,
            Name = name
        };

        var json = JsonUtility.ToJson(account);

        Debug.Log("Registering account: " + json);

        StartCoroutine(Post(json));
    }

    IEnumerator Post(string json)
    {
        var url = "http://localhost:5034/api/register";

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Register Error: " + request.error);
        }
        else
        {
            var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

            Debug.Log("Notification: " + response.Notification);

            if (response.isSuccess)
            {
                Debug.Log("Register success!");

                UnityEngine.SceneManagement.SceneManager.LoadScene("Tools");
            }
            else
            {
                Debug.Log("Register failed: " + response.Notification);
            }
        }
    }
}