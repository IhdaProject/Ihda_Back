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
    All = 1
}