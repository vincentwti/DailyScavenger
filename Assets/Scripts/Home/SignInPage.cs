using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Net.Mail;
using UnityEngine.Networking;

[Serializable]
public class Location
{
    public string id;
    public string name;
    public float longtitude;
    public float latitude;
    public float height;
    public Event[] events;
}

[Serializable]
public class Event
{
    public string id;
    public string title;
    public string subtitle;
    public string image;
    public string video;
    public string audio;
    public string asset;
    public int credit;
    public string expDate;
}

[Serializable]
public class LocationList
{
    public List<Location> locations;
}

[Serializable]
public class EventList
{
    public List<Event> eventList;
}

[Serializable]
public class UserList
{
    public List<User> data;
}

[Serializable]
public class UserDict
{
    public Dictionary<int, User> customers;
}

public class SignInPage : UIController
{
    private SignInPageView signInPageView;

    private void Start()
    {
        //DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        signInPageView.SetSignInButtonEvent(SignIn);
        signInPageView.SetForgotPasswordButtonEvent(ForgotPassword);
        view.SetBackButtonEvent(() => HomePage.Instance.ShowWelcomePage(true));
        //GetUser(); 
    }


    protected override void Awake()
    {
        base.Awake();
        PageStateController.RegisterPage(Page.PageState.LOGIN, null, null);
    }

    protected override void Init()
    {
        base.Init();
        signInPageView = (SignInPageView)view;
    }

    private bool ValidateEmail()
    {
        try
        {
            MailAddress mailAddress = new MailAddress(signInPageView.GetEmailInput());
            return true;
        }
        catch (Exception exception)
        {
            PopupManager.Instance.ShowPopupMessage("err", "Email Validation",
               "Email format is not valid",
               new ButtonInfo { content = "Ok" });
            Debug.LogError("err : " + exception.Message);
        }
        return false;
    }

    private bool ValidatePassword()
    {
        string value = signInPageView.GetPasswordInput();
        if (string.IsNullOrEmpty(value))
        {
            PopupManager.Instance.ShowPopupMessage("err", "Please Enter Your Password",
               "Please enter your password to login.",
                new ButtonInfo { content = "Ok" });
            return false;
        }
        return true;
    }

    public void SignIn()
    {
        if (ValidateEmail())
        {
            if (ValidatePassword())
            {
                //StartCoroutine(RequestLogin());
                StartCoroutine(API.RequestLogin(signInPageView.GetEmailInput(), signInPageView.GetPasswordInput(), OnLoginSucceed));
            }
        }
    }

    public void ForgotPassword()
    {
        Application.OpenURL("https://support.google.com/accounts/answer/41078?hl=en&sjid=8457417858173608490-AP");
    }

    private IEnumerator RequestLogin()
    {
        string url = "https://dickyri.net/_optitreasure/login_api.php";
        WWWForm form = new WWWForm();
        form.AddField("email", signInPageView.GetEmailInput());
        form.AddField("password", signInPageView.GetPasswordInput());
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.Success)
            {
                OnLoginSucceed();
            }
            else
            {
                Debug.LogError("err : " + uwr.error);
                PopupManager.Instance.ShowPopupMessage("err", "Email or Password is Invalid", "Please check your email and password.", new ButtonInfo { content = "Ok" });
            }
        }
    }

    private void OnLoginSucceed()
    {
        Debug.LogWarning("login success");
        PlayerPrefs.SetString("login", signInPageView.GetEmailInput() + "|" + signInPageView.GetPasswordInput());
        PlayerPrefs.Save();

        Hide();
        HomePage.Instance.Show();
    }
}
