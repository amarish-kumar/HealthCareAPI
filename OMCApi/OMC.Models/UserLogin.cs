using System;
using System.Collections.Generic;

namespace OMC.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public string GetCodeMethod { get; set; }
        public List<Role> RoleList { get; set; }
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsDefault { get; set; }
    }
    public class SignInResponse
    {
        public int? UserId { get; set; }
        public bool IsPasswordVerified { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public int? UserDeviceId { get; set; }
        public bool TwoFactorAuthDone { get; set; }
        public DateTime? TwoFactorAuthTimestamp { get; set; }
        public string SessionId { get; set; }
        public bool IsUserActive { get; set; }
    }

    public class UserAccessCodeResponse
    {
        public int? UserId { get; set; }
        public string AccessCode { get; set; }
        public int? UserLoginAuditId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ErrorMessage { get; set; }
        public ValidateAccessCodeResponse objValidateAccessCodeResponse { get; set; }
    }

    public class ValidateAccessCodeResponse
    {
        public string Message { get; set; }
        public bool IsValidCodePassed { get; set; }
        public int UnsuccessfulAttemptCount { get; set; }
        public bool IsAccountLocked { get; set; }
    }
}
