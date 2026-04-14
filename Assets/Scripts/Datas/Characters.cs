using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class Characters : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField levelInput;

    public void GetAllCharacters()
    {
        StartCoroutine(GetAllChar());
    }

    public void GetCharactersByLevel()
    {
        StartCoroutine(GetCharByLevel());
    }

    public void GetCharactersByAccountId()
    {
        StartCoroutine(GetCharByAccId());
    }

    IEnumerator GetAllChar()
    {
        var url = "http://localhost:5034/api/GetAllChar";

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
                ResponseCharacterList response = JsonUtility.FromJson<ResponseCharacterList>(request.downloadHandler.text);
                Debug.Log("Response: " + request.downloadHandler.text);
                Debug.Log("Notification: " + response.Notification);

                if (response.isSuccess)
                {
                    foreach (var character in response.Data)
                    {
                        Debug.Log("Character: " + character.Name + " Level: " + character.Level + " Exp: " + character.Exp);
                    }
                }
                else
                {
                    Debug.LogError("Failed to get characters!");
                }
            }
        }
    }

    IEnumerator GetCharByLevel()
    {
        var level = levelInput.text;
        var url = "http://localhost:5034/api/GetCharByLevel/" + level;

        Debug.Log("URL: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log("RAW RESPONSE: " + request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                ResponseCharacterList response = JsonUtility.FromJson<ResponseCharacterList>(request.downloadHandler.text);

                Debug.Log("Notification: " + response.Notification);

                if (response.isSuccess && response.Data != null)
                {
                    foreach (var cha in response.Data)
                    {
                        Debug.Log("Character: " + cha.Name + " Level: " + cha.Level + " Exp: " + cha.Exp);
                    }
                }
                else
                {
                    Debug.Log("No character found higher than this level!");
                }
            }
        }
    }

    IEnumerator GetCharByAccId()
    {
        var id = idInput.text;
        var url = "http://localhost:5034/api/GetCharByAccId/" + id;

        Debug.Log("URL: " + url);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log("RAW RESPONSE: " + request.downloadHandler.text);

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                ResponseCharacterList response = JsonUtility.FromJson<ResponseCharacterList>(request.downloadHandler.text);

                Debug.Log("Notification: " + response.Notification);

                if (response.isSuccess && response.Data != null)
                {
                    foreach (var cha in response.Data)
                    {
                        Debug.Log("Character: " + cha.Name + " Level: " + cha.Level + " Exp: " + cha.Exp);
                    }
                }
                else
                {
                    Debug.Log("No character found for this account!");
                }
            }
        }
    }
}
