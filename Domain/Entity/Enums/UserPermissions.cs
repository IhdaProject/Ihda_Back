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
            ViewMyPermissions = 10100002,
            #endregion
        #region Role Controller 11
            ViewPermissions = 10110001,
            UpdatePermission = 10110002,
            ViewStructures = 10110003,
            ViewStructure = 10110004,
            CreateStructure = 10110005,
            UpdateStructure = 10110006,
            RemoveStructure = 10110009,
        #endregion
        #region User Controller 12
            ViewUsers = 10120001,
            ViewUser = 10120002,
            ViewProfile = 10120003,
            AddUserStructure = 10120004,
            RemoveUserStructure = 10120005,
        #endregion
    #endregion

    #region ReferenceBookApi 30

        #region CountryController 10
            ViewAllCountries = 30100001,
            ViewByIdCountry = 30100002,
            AddCountry = 30100003,
            RemoveCountry = 30100004,
            UpdateCountry = 30100005,
        #endregion
    
        #region DistrictController 11
            ViewAllDistrict = 30110001,
            ViewByIdDistrict = 30110002,
            AddDistrict = 30110003,
            RemoveDistrict = 30110004,
            UpdateDistrict = 30110005,
        #endregion
        
        #region RegionController 12
            ViewAllRegions = 30120001,
            ViewByIdRegion = 30120002,
            AddRegion = 30120003,
            RemoveRegion = 30120004,
            UpdateRegion = 30120005,
        #endregion
        
        #region UniversalTypeController 13
            UniversalTypeViewWithDetails = 30130001,
            UniversalTypesView = 30130002,
            UniversalTypeCreate = 30130003,
            UniversalTypeUpdate = 30130004,
        #endregion

    #endregion
    
    #region MosqueApi 40
        #region Mosques Controller 10
            OnSaveMosque = 40100001,
            OnSavePreyerTime = 40100002,
            ViewMosques = 40100003,
            ViewMosque = 40100004,
            ViewFavoritesMosque = 40100005,
            ToggleFavoriteMosque = 40100006,
            ViewMyMosques = 40100007,
            AddMosqueAdmin = 40100008,
        #endregion
    #endregion
    #region QuranCourse 60
        #region TrainingCenter Controller 10
            CreateTrainingCenter = 60100001,
            ViewTrainingCenters = 60100002,
            ViewTrainingCenter = 60100003,
            CreateCourseForm = 60100004,
            ViewCourseForms = 60100005,
            ViewCourseForm = 60100006,
            UpdateTrainingCenter = 60100007,
            UpdateCourseForm = 60100008,
        #endregion
        #region Quran Course Controller 20
            ViewPetitionQuranCourses = 60200001,
        #endregion
    #endregion
    
}