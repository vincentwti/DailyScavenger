using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class API
{
    public static void GetMenuFromLocalJson<T>(string fileName, Action<T> result)
    {
        TextAsset json = Resources.Load<TextAsset>("JSON/" + fileName);
        var obj = JsonUtility.FromJson<T>(json.text);
        result?.Invoke(obj);
    }

    public static IEnumerator RequestMenu(string url, Action<MenuItemList> onSuccess, Action<string> onFailed)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        yield return uwr.SendWebRequest();
        if (string.IsNullOrEmpty(uwr.error))
        {
            onSuccess?.Invoke(JsonUtility.FromJson<MenuItemList>(uwr.downloadHandler.text));
        }
        else
        {
            onFailed?.Invoke(uwr.error);
        }
    }

    public static IEnumerator RequestRegister(string username, string password, string email, Action onRegisterSucceed)
    {
        string url = "https://dickyri.net/_optitreasure/register_api.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("email", email);
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.Success)
            {
                onRegisterSucceed?.Invoke();
            }
            else
            {
                Debug.LogError("err : " + uwr.error);
                PopupManager.Instance.ShowPopupMessage("err", "Unable to Register User", "Username or email may already be taken. Please try another one.", new ButtonInfo { content = "Ok" });
            }
        }
    }

    public static IEnumerator RequestLogin(string email, string password, Action onLoginSucceed)
    {
        string url = "https://dickyri.net/_optitreasure/login_api.php";
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.Success)
            {
                onLoginSucceed?.Invoke();
            }
            else
            {
                Debug.LogError("err : " + uwr.error);
                PopupManager.Instance.ShowPopupMessage("err", "Email or Password is Invalid", "Please check your email and password.", new ButtonInfo { content = "Ok" });
            }
        }
    }

    public static IEnumerator RequestLogout(Action onLogoutSucceed)
    {
        string url = "https://dickyri.net/_optitreasure/logout_api.php";
        using (UnityWebRequest uwr = new UnityWebRequest(url))
        {
            uwr.method = "POST";
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.Success)
            {
                onLogoutSucceed?.Invoke();
            }
            else
            {
                Debug.LogError("err : " + uwr.error);
                PopupManager.Instance.ShowPopupMessage("err", "Logout Failed", "Logout failed, please try again later.", new ButtonInfo { content = "Ok" });
            }
        }
    }
}
