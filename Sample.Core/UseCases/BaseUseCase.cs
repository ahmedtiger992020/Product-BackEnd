using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Sample.Core.Interfaces;
using Sample.Core.Interfaces.Repository;
using Sample.SharedKernal.Localization;

/// <summary>
/// Summary description for Class1
/// </summary>
public class BaseUseCase
{
    #region Props
    public IHttpContextAccessor HttpContextAccessor { get; set; }
    public ILocalizationReader MessageResource { get; set; }
    public IConfiguration Configuration { get; set; }
    public IMapper Mapper { get; set; }
    public IUnitOfWork UnitOfWork { get; set; }
    #endregion

    #region Ctor
    public BaseUseCase() { }

    #endregion

    #region Properties

    public string _culture
    {
        get
        {
            try
            {
                var HeaderCulture = HttpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
                return HeaderCulture == null || !string.IsNullOrEmpty(HeaderCulture)? Configuration.GetSection("DefaultLan").Value :HeaderCulture;
            }
            catch (Exception e)
            {
                return Configuration.GetSection("DefaultLan").Value;
            }
        }
    }
    #endregion
}
