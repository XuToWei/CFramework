using GameFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Game
{
    public class ScreenComponent : GameFrameworkComponent
    {
        [SerializeField] private int m_StandardWidth = 1080;
        [SerializeField] private int m_StandardHeight = 1920;
        
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        public int Width { private set; get; }

        /// <summary>
        /// 屏幕高度
        /// </summary>
        public int Height { private set; get; }

        /// <summary>
        /// 屏幕安全区域
        /// </summary>
        public Rect SafeArea { private set; get; }

        /// <summary>
        /// 标准屏幕宽
        /// </summary>
        public int StandardWidth => m_StandardWidth;
        
        /// <summary>
        /// 标准屏幕高
        /// </summary>
        public int StandardHeight => m_StandardHeight;
        
        /// <summary>
        /// 标准屏幕比例（高/宽）
        /// </summary>
        public float StandardRatio { private set; get; }

        private void Start()
        {
            StandardRatio = 1f * m_StandardHeight / m_StandardWidth;
        }

        /// <summary>
        /// 设置屏幕数据
        /// </summary>
        /// <param name="width">屏幕宽</param>
        /// <param name="height">屏幕高</param>
        /// <param name="safeArea">屏幕的安全区域</param>
        public void Set(int width, int height, Rect safeArea)
        {
            Width = width;
            Height = height;
            Log.Info(Utility.Text.Format("设置屏幕宽:{0} ,高:{1} .", width, height));
            SafeArea = safeArea;
            Log.Info(Utility.Text.Format("设置屏幕安全区域 x:{0} ,y:{1} ,width:{2} ,height:{3} .", safeArea.x, safeArea.y, safeArea.width, safeArea.height));
            AdjustCanvasScaler();
        }

        private void AdjustCanvasScaler()
        {
            CanvasScaler canvasScaler = GameEntry.UI.transform.Find("UI Form Instances").GetComponent<CanvasScaler>();
            float ratio = GameEntry.Screen.SafeArea.height / GameEntry.Screen.SafeArea.width;
            canvasScaler.matchWidthOrHeight = ratio > GameEntry.Screen.StandardRatio ? 0 : 1;
        }
    }
}