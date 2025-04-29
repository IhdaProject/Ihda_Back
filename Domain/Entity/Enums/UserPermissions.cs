namespace Entity.Enums;
/// <summary>
/// Represents permissions for users in the system.
/// The structure is defined as follows:
/// - First 2 digits: Project identifier.
/// - Next 2 digits: Controller (module) identifier.
/// - Last 4 digits: Specific endpoint or action identifier.
/// </summary>
public enum UserPermissions
{
    #region AuthApi project 10
        #region Auth Controller 10
            LogOut = 10100001,
        #endregion

        #region Role Controller 11
        #endregion
    #endregion
}