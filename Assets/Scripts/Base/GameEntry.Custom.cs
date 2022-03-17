﻿namespace Game
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry
    {
        /// <summary>
        /// 自定义数据组件
        /// </summary>
        public static BuiltinComponent Builtin { get; private set; }
        
        /// <summary>
        /// 数据表扩展组件
        /// </summary>
        public static DataTableExtensionComponent DataTableExtension { get; private set; }
        
        /// <summary>
        /// 热更组件
        /// </summary>
        public static HotfixComponent Hotfix { get; private set; }
        
        /// <summary>
        /// 屏幕组件
        /// </summary>
        public static ScreenComponent Screen { get; private set; }

        /// <summary>
        /// 获取精灵收集组件。
        /// </summary>
        public static SpriteCollectionComponent SpriteCollection { get; private set; }

        /// <summary>
        /// 获取图片设置组件。
        /// </summary>
        public static TextureComponent TextureSet { get; private set; }
        
        /// <summary>
        /// 获取定时器组件。
        /// </summary>
        public static TimerComponent Timer { get; private set; }
        
        private static void InitCustomComponents()
        {
            Timer = UnityGameFramework.Runtime.GameEntry.GetComponent<TimerComponent>();
            Builtin = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinComponent>();
            SpriteCollection = UnityGameFramework.Runtime.GameEntry.GetComponent<SpriteCollectionComponent>();
            TextureSet = UnityGameFramework.Runtime.GameEntry.GetComponent<TextureComponent>();
            DataTableExtension = UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableExtensionComponent>();
            Hotfix = UnityGameFramework.Runtime.GameEntry.GetComponent<HotfixComponent>();
            Screen = UnityGameFramework.Runtime.GameEntry.GetComponent<ScreenComponent>();
        }
    }
}