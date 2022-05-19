using System;
using System.Collections;
using System.Collections.Generic;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward;
using TwitchLib.Unity;
using UnityEngine;

public class TwitchAPI : MonoBehaviour
{
    public Api api;
    private string channel_name = "El_Largosstia";
    private List<string> rewardIds;

    // Start is called before the first frame update
    void Start()
    {

        Application.runInBackground = true;
        api = new Api();
        api.Settings.ClientId = Secrets.CLIENT_ID;
        api.Settings.AccessToken = Secrets.OAUTH_TOKEN;

    }

    // Update is called once per frame
    void Update()
    {


    }

}
