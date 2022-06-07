//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace Hotfix.Framework
{
    /// <summary>
    /// 游戏框架组件抽象类。
    /// </summary>
    public abstract class GameFrameworkComponent
    {
        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected GameFrameworkComponent()
        {
            GameEntry.RegisterComponent(this);
        }
    }
}
