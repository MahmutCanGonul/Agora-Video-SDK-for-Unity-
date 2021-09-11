# Agora-Video-SDK-for-Unity-
Agora Video SDK for Unity Quick Start Programming Guide (Facetime Communication)




Prerequisites
Unity Editor (version 2017 LTS or up, 2019 recommended)
An understanding of the Unity editor, Game Objects, Unity scripting, and publishing Unity apps to mobile devices
Basic understanding of C#
Agora Developer Account (see: How To Get Started with Agora)



Overview
Before we dive in, let’s take a moment to review all the steps that we will go through.

=>Setup New Project and Import Agora Video SDK for Unity

=>Create User Interfaces (UIs)

=>Create Controller Script (Top-down approach)

=>Setup UIs

=>Agora Engine Event Set up and Handling

=>Clean up

=>Test


SET-UP:

![image](https://user-images.githubusercontent.com/75094927/132946557-de3e3a5e-9448-4d04-92ff-a58736662788.png)



Create the User Interfaces
When Unity creates the new project, a default Scene “Sample” is created for you. We will use this scene for our project. As shown in Figure 1, we will create the following four objects on the scene:

RawImage #1: name it “MyView”, size is 180w X 320h.

RawImage #2: duplicate from MyView, named “RemoteView”, move to right position next to MyView.

Button #1: name it “JoinButton”, size is 100w X 45h, the button text should be “Join”.

Button #2: duplicate from JoinButton, and button text should be “Leave”.

Make sure to change the Canvas Scaler option to “Scale With Screen Size”.







Create Scripts



▶️1.Step:
💣CREATE CONTROL SCRIPT

public string AppID;
    public string ChannelName;

    VideoSurface myView;
    VideoSurface remoteView;
    IRtcEngine mRtcEngine;

    void Awake()
    {
        // SetupUI();
    }

    void Start()
    {
        // SetupAgora();
    }

    void Join()
    {}

    void Leave()
    {}

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {}

    void OnLeaveChannelHandler(RtcStats stats)
    {}
    
    void OnUserJoined(uint uid, int elapsed)
    {}

    void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {}

    void OnApplicationQuit()
    {}
    
    
▶️2.Step:

💣UI SETUP

void SetupUI()
{
        GameObject go = GameObject.Find("MyView");
        myView = go.AddComponent<VideoSurface>();
        go = GameObject.Find("LeaveButton");
        go?.GetComponent<Button>()?.onClick.AddListener(Leave);
        go = GameObject.Find("JoinButton");
        go?.GetComponent<Button>()?.onClick.AddListener(Join);
}

  And the Join and Leave Function is simply calling the relevant Agora APIs:
  
  
  void Join()
    {
        mRtcEngine.EnableVideo();
        mRtcEngine.EnableVideoObserver();
        myView.SetEnable(true);
        mRtcEngine.JoinChannel(ChannelName, "", 0);
    }

    void Leave()
    {
        mRtcEngine.LeaveChannel();
        mRtcEngine.DisableVideo();
        mRtcEngine.DisableVideoObserver();
    }
    
    
▶️3.Step:
  
 💣IMPLEMENT AGORA ENGINE SETUP
  
  
  void SetupAgora()
    {
        mRtcEngine = IRtcEngine.GetEngine(AppID);

        mRtcEngine.OnUserJoined = OnUserJoined;
        mRtcEngine.OnUserOffline = OnUserOffline;
        mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccessHandler;
        mRtcEngine.OnLeaveChannel = OnLeaveChannelHandler;
    }


▶️4.Step:
  💣PROVIDE THE HANDLERS ON THE EVENT CALLBACKS
  
  
void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        // can add other logics here, for now just print to the log
        Debug.LogFormat("Joined channel {0} successful, my uid = {1}", channelName, uid);
    }

    void OnLeaveChannelHandler(RtcStats stats)
    {
        myView.SetEnable(false);
        if (remoteView != null)
        {
            remoteView.SetEnable(false);
        }
    }

    void OnUserJoined(uint uid, int elapsed)
    {
        GameObject go = GameObject.Find("RemoteView");

        if (remoteView == null)
        {
            remoteView = go.AddComponent<VideoSurface>();
        }

        remoteView.SetForUser(uid);
        remoteView.SetEnable(true);
        remoteView.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
        remoteView.SetGameFps(30);
    }

    void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        remoteView.SetEnable(false);
    }
  
  
  
  ▶️5.Step:
  
  APPLICATION CLEAN-UP
  
  void OnApplicationQuit()
    {
        if (mRtcEngine != null)
        {
            IRtcEngine.Destroy(); 
            mRtcEngine = null;
        }
    }
  
  
  
  
  
  
  
  
  
  🥇FINISHHHHH 🥇
  
  
  ![channel](https://user-images.githubusercontent.com/75094927/132946865-43ebd21b-458c-48fd-9ebc-83fdf8e230f0.png)

  
  
  
  
  
  
  
  
  
  






