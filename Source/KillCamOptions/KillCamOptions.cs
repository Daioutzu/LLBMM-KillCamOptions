using GameplayEntities;
using LLHandlers;
using UnityEngine;

namespace KillCamOptions
{
    class KillCamOptions : MonoBehaviour
    {
        private const string modVersion = "1.0.0";
        private const string repositoryOwner = "Daioutzu";
        private const string repositoryName = "LLBMM-KillCamOptions";
        private int koSpeedSlider;
        private bool KillCamEnabled = false;
        private bool modIntegrated;

        private static bool IsOnline => JOMBNFKIHIC.GDNFJCCCKDM;
        private static bool InGame => World.instance != null && (DNPFJHMAIBP.HHMOGKIMBNM() == JOFJHDJHJGI.CDOFDJMLGLO || DNPFJHMAIBP.HHMOGKIMBNM() == JOFJHDJHJGI.LGILIJKMKOD) && !LLScreen.UIScreen.loadingScreenActive;
        public static KillCamOptions Instance { get; private set; } = null;
        public static ModMenuIntegration MMI { get; private set; } = null;

        public static void Initialize()
        {
            GameObject gameObject = new GameObject("KillCamOptions");
            Instance = gameObject.AddComponent<KillCamOptions>();
            DontDestroyOnLoad(gameObject); // Makes sure our game object isn't destroyed
        }
        void Start()
        {
            if (MMI == null) { MMI = gameObject.AddComponent<ModMenuIntegration>(); Debug.Log("[LLBMM] KillCamOptions: Added GameObject \"ModMenuIntegration\""); }
        }

        void ModMenuInit()
        {
            if ((MMI != null && !modIntegrated) || LLModMenu.ModMenu.Instance.inModSubOptions && LLModMenu.ModMenu.Instance.currentOpenMod == "AdvancedTraining")
            {
                KillCamEnabled = MMI.GetTrueFalse(MMI.configBools["(bool)killCameraIs"]);
                koSpeedSlider = MMI.GetSliderValue("(slider)playKillCameraOverSpeed");

                if (!modIntegrated) { Debug.Log("[LLBMM] KillCamOptions: ModMenuIntegration Done"); };
                modIntegrated = true;
            }
        }

        private void Update()
        {
            ModMenuInit();
        }

        private void LateUpdate()
        {
            if (InGame && IsOnline == false)
            {
                if (KillCamEnabled)
                {
                    if (!JOMBNFKIHIC.GDNFJCCCKDM && World.instance.worldData.koCamState == 0 && HHBCPNCDNDH.CJBFNLGJNIH(BallHandler.instance.GetBall(0).GetPixelFlySpeed(true), HHBCPNCDNDH.NKKIFJJEPOL(koSpeedSlider)))
                    {
                        World.instance.SetKOCamState(AEJIEDFMDJM.GAOABBKLMDF);
#if DEBUG
                        Debug.Log("[LLBMM] KillCamOptions: Disabled KOCam");
#endif
                    }

                    if (!JOMBNFKIHIC.GDNFJCCCKDM && World.instance.worldData.koCamState == AEJIEDFMDJM.GAOABBKLMDF && HHBCPNCDNDH.OAHDEOGKOIM(BallHandler.instance.GetBall(0).GetPixelFlySpeed(true), HHBCPNCDNDH.NKKIFJJEPOL(koSpeedSlider)) && BallHandler.instance.GetBall(0).ballData.hitstunState != HitstunState.KILL_PLAYER_STUN)
                    {
                        World.instance.SetKOCamState(AEJIEDFMDJM.BHAIFNKJPAF);
#if DEBUG
                        Debug.Log("[LLBMM] KillCamOptions: Enabled KOCam");
#endif
                    }
                }
                else
                {
                    //No matter what speed KO camera is disabled.
                    if (!JOMBNFKIHIC.GDNFJCCCKDM && World.instance.worldData.koCamState == 0 && HHBCPNCDNDH.HPLPMEAOJPM(BallHandler.instance.GetBall(0).GetPixelFlySpeed(true), BallHandler.instance.GetBall(0).minFlySpeed))
                    {
                        World.instance.SetKOCamState(AEJIEDFMDJM.GAOABBKLMDF);
#if DEBUG
                        Debug.Log("[LLBMM] KillCamOptions: Disabled KOCam"); 
#endif
                    }
                }
            }
        }
    }
}
