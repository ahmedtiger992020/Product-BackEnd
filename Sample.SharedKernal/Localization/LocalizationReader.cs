namespace Sample.SharedKernal.Localization
{
    public class LocalizationReader : BaseFileReader, ILocalizationReader
    {
        public LocalizationReader()
        {

        }
        #region General Messages

        public string GeneralError(string _culture) => GetKeyValue("GeneralError", _culture);
        public string InvalidRequest(string _culture) => GetKeyValue("InvalidRequest", _culture);
        public string CommitFailed(string _culture) => GetKeyValue("CommitFailed", _culture);
        public string InvalidId(string _culture) => GetKeyValue("InvalidId", _culture);
        public string InvalidName(string _culture) => GetKeyValue("InvalidName", _culture);
        public string InvalidPhoto(string _culture) => GetKeyValue("InvalidPhoto", _culture);
        public string InvalidPrice(string _culture) => GetKeyValue("InvalidPrice", _culture);
        public string ProductNotFound(string _culture) => GetKeyValue("ProductNotFound", _culture);
        public string NameAlreadyExisit(string _culture) => GetKeyValue("NameAlreadyExisit", _culture);

        #endregion
    }
}