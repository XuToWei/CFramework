namespace Game
{
    public enum HotfixType : byte
    {
        Undefined = 0,
        
        /// <summary>
        /// Mono
        /// </summary>
        Mono = 1,
        
        /// <summary>
        /// ILRuntime
        /// </summary>
        ILRuntime = 2,
        
        /// <summary>
        /// Unity Editor
        /// </summary>
        Editor = 3,
    }
}