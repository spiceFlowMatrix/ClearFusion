using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Common.Helpers
{
  public static class Constants
  {
    public static class Strings
    {
      public static class JwtClaimIdentifiers
      {
        public const string Rol = "rol", Id = "id", UserName = "username", CompanyType = "companytype";
        //public const string TeamRol = "TeamRol";
        public const string IsLicensed = "islicensed";
        public const string CompanyId = "companyid";
        public const string IsLicensedExpired = "islicensedexpired";
        public const string CompanyTypeId = "companyTypeId";
      }

      public static class JwtClaims
      {
        public const string ApiAccess = "abc";
      }
    }
  }
}
