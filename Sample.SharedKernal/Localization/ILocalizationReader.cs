
using System.Runtime.Serialization;

namespace Sample.SharedKernal.Localization
{
    public interface ILocalizationReader
    {
        #region General Messages
        string GeneralError(string _culture);
        string InvalidId(string _culture);
        string InvalidRequest(string _culture);
        string InvalidName(string _culture);
        string InvalidPhoto(string _culture);
        string InvalidPrice(string culture);
        string NameAlreadyExisit(string culture);
        string ProductNotFound(string _culture);
        string CommitFailed(string _culture);
        #endregion
    }
}
