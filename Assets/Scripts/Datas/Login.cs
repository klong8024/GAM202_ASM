using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    public void GoToRegister()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Register");
    }

    public void OnLoginClick()
    {
        var email = emailInput.text;
        var password = passwordInput.text;

        var account = new LoginRequest
        {
            Email = email,
            Password = password
        };

        var json = JsonUtility.ToJson(account);

        // Debug để kiểm tra
        Debug.Log("JSON gửi: " + json);

        StartCoroutine(OnLogin(json));
    }

    IEnumerator OnLogin(string json)
    {
        var url = "http://localhost:5034/api/login";

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Request Error: " + request.error);
        }
        else
        {
            var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

            Debug.Log("Notification: " + response.Notification);

            if (response.isSuccess && response.Data != null)
            {
                Debug.Log("Login successful: " + response.Data.Name);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Login");
            }
            else
            {
                Debug.Log("Login failed: " + response.Notification);
            }
        }
    }
}
