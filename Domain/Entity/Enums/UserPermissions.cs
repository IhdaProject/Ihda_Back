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
    #region Auth (01 - Authentication and Authorization)
    
    /// <summary>
    /// View permissions (Project: Auth, Controller: Permissions).
    /// </summary>
    PermissionView = 1100,

    /// <summary>
    /// Edit permission names (Project: Auth, Controller: Permissions).
    /// </summary>
    PermissionNameEdit = 1102,

    /// <summary>
    /// View structure (Project: Auth, Controller: Structures).
    /// </summary>
    StructureView = 1200,

    /// <summary>
    /// Create new structures (Project: Auth, Controller: Structures).
    /// </summary>
    StructureCreate = 1201,

    /// <summary>
    /// Edit structures (Project: Auth, Controller: Structures).
    /// </summary>
    StructureEdit = 1202,

    /// <summary>
    /// Delete structures (Project: Auth, Controller: Structures).
    /// </summary>
    StructureDelete = 1203,

    /// <summary>
    /// View user information (Project: Auth, Controller: Users).
    /// </summary>
    UserInfoView = 1300,

    /// <summary>
    /// View all users (Project: Auth, Controller: Users).
    /// </summary>
    UsersView = 1301,

    /// <summary>
    /// Change user structure (Project: Auth, Controller: Users).
    /// </summary>
    ChangeUserStructure = 1302,

    #endregion

    #region ProjectName (02 - Other Project Identifier)
    
    /// <summary>
    /// Placeholder for future permissions related to ProjectName.
    /// </summary>
    FuturePermission = 2000,

    #endregion
}