using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PubNubAPI;

public class PubnubConnection : MonoBehaviour
{
    public class EmailData
    {
        public string name;
        public string email;
        public int score;
    }

    public static PubNub pubnub;

    // Start is called before the first frame update
    void Start()
    {
        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.PublishKey = "pub-c-e9d3ecdd-f452-46e4-b52c-455dbf58079a";
        pnConfiguration.SubscribeKey = "sub-c-5d3ba9e0-36c7-11ec-b886-526a8555c638";

        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UUID = Random.Range(0f, 999999f).ToString();

        pubnub = new PubNub(pnConfiguration);
    }

    public static void SubmitEmail(string name, string email, int score)
    {
        EmailData emailData = new EmailData();
        emailData.email = email;
        emailData.name = name;
        emailData.score = score;

        string emailJson = JsonUtility.ToJson(emailData);

        pubnub.Publish()
            .Channel("email_list")
            .Message(emailJson)
            .Async((result, status) => {
                if (!status.Error)
                {
                    Debug.Log(string.Format("Publish Timetoken: {0}", result.Timetoken));
                }
                else
                {
                    Debug.Log(status.Error);
                    Debug.Log(status.ErrorData.Info);
                }
            });
    }
}
