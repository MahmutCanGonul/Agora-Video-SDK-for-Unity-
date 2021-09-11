using System.Collections;
using System.Collections.Generic;
using agora_gaming_rtc;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public string App_ID = "81723c5dbd584f96b52c6b3f51533707";
    public string Channel_Name = "MahmutAbiAlpha01";
    VideoSurface myview;
    VideoSurface remoteview;

    IRtcEngine engine;

   
   
   


    void Awake()
    {
        SetupUI();
    }

    void Start()
    {
        SetupAgora();
    }

    void Join()
    {

        engine.EnableVideo();
        engine.EnableVideoObserver();
        myview.SetEnable(true);
        engine.JoinChannel(Channel_Name, "", 0);


    }

    void Leave()
    {

        engine.LeaveChannel();
        engine.DisableVideo();
        engine.DisableVideoObserver();
    }

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        Debug.LogFormat("Joined channel {0} successful, my uid = {1}", channelName, uid);
    }

    void OnLeaveChannelHandler(RtcStats stats)
    {
        myview.SetEnable(false);

        if (remoteview != null)
        {
            remoteview.SetEnable(false);
        }
    }

    void OnUserJoined(uint uid, int elapsed)
    {
        GameObject go = GameObject.Find("RemoteView");
        if (remoteview != null)
        {
            remoteview = go.AddComponent<VideoSurface>();
        }

        remoteview.SetForUser(uid);
        remoteview.SetEnable(true);
        remoteview.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
        remoteview.SetGameFps(30);

    }

    void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        remoteview.SetEnable(false);

    }

    void OnApplicationQuit()
    {
        if (engine != null)
        {
            IRtcEngine.Destroy();
            engine = null;

        }
    }

    void SetupUI()
    {
        GameObject go = GameObject.Find("MyView");
        myview = go.AddComponent<VideoSurface>();
        go = GameObject.Find("Leave Button");
        go?.GetComponent<Button>()?.onClick.AddListener(Leave);
        go = GameObject.Find("Join Button");
        go?.GetComponent<Button>()?.onClick.AddListener(Join);
    }
    void SetupAgora()
    {

        engine = IRtcEngine.getEngine(App_ID);
        engine.OnUserJoined = OnUserJoined;
        engine.OnUserOffline = OnUserOffline;
        engine.OnJoinChannelSuccess = OnJoinChannelSuccessHandler;
        engine.OnLeaveChannel = OnLeaveChannelHandler;

    }





}
