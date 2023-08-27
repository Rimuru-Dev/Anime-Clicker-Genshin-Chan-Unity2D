using AbyssMoth.Internal.Codebase.Runtime.GameData;

namespace YG
{
    [System.Serializable]
    public sealed class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        public Storage storage;
    }
}