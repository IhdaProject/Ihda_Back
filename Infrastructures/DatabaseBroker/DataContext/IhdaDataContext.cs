using System.Security.Claims;
using Entity.Enums;
using Entity.Models.Auth;
using Entity.Models.Common;
using Entity.Models.Mosques;
using Entity.Models.QuranCourses;
using Entity.Models.ReferenceBook;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using File = Entity.Models.Clouds.File;

namespace DatabaseBroker.DataContext;

public class IhdaDataContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;
    public IhdaDataContext(DbContextOptions<IhdaDataContext> options, IServiceProvider serviceProvider)
        : base(options)
    {
        _serviceProvider = serviceProvider;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private int GetCurrentUserId()
    {
        var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();
        var userIdClaim = httpContextAccessor?.HttpContext?.User?.FindFirst(CustomClaimNames.UserId);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            return userId;

        return -1;
    }
    
    private void TrackActionsAt()
    {
        var dateTimeNow = DateTime.UtcNow;
        var userId = GetCurrentUserId();
        
        foreach (var entity in ChangeTracker.Entries()
                     .Where(x => x is { State: EntityState.Added, Entity: AuditableModelBase<long> }))
        {
            var model = (AuditableModelBase<long>)entity.Entity;
            model.CreatedAt = dateTimeNow;
            model.CreatedBy = userId;
        }

        foreach (var entity in ChangeTracker.Entries()
                     .Where(x => x is { State: EntityState.Modified, Entity: AuditableModelBase<long> }))
        {
            var model = (AuditableModelBase<long>)entity.Entity;
            model.UpdatedAt = dateTimeNow;
            model.UpdatedBy = userId;
        }
    }

    public override int SaveChanges()
    {
        TrackActionsAt();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        TrackActionsAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        TrackActionsAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        TrackActionsAt();
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Configuring all MultiLanguage fields over entities
        var multiLanguageFields = modelBuilder
            .Model
            .GetEntityTypes()
            .SelectMany(x => x.ClrType.GetProperties())
            .Where(x => x.PropertyType == typeof(MultiLanguageField));

        foreach (var multiLanguageField in multiLanguageFields)
            modelBuilder
                .Entity(multiLanguageField.DeclaringType!)
                .Property(multiLanguageField.PropertyType, multiLanguageField.Name)
                .HasColumnType("jsonb");

        modelBuilder
            .Entity<SignMethod>()
            .HasOne(x => x.User)
            .WithMany(x => x.SignMethods)
            .HasForeignKey(x => x.UserId);

        modelBuilder
            .Entity<SignMethod>()
            .HasDiscriminator(x => x.Type)
            .HasValue<DefaultSignMethod>(SignMethods.Normal);
    }

    #region Mosques Schema
    public DbSet<Mosque> Mosques { get; set; }
    public DbSet<FavoriteMosque> FavoriteMosques { get; set; }
    public DbSet<MosquePrayerTime> MosquePrayerTimes { get; set; }
    #endregion

    #region Auth schema
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Structure> Structures { get; set; }
    public DbSet<StructurePermission> StructurePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SignMethod> UserSignMethods { get; set; }
    public DbSet<TokenModel> Tokens { get; set; }
    #endregion

    #region Cloud schema
    public DbSet<File>  Files { get; set; }
    #endregion
    
    #region Reference Book schema
    public DbSet<Country> Countries { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<PrayerTimeStyle> PrayerTimeStyles { get; set; }
    public DbSet<CalculatingCentury> CalculatingCenturies { get; set; }
    public DbSet<TypeSchema> Types { get; set; }
    #endregion

    #region Quran Course
    public DbSet<TrainingCenter> TrainingCenters { get; set; }
    public DbSet<CourseForm> CourseForms { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseFormTeacher> CourseFormTeachers { get; set; }
    public DbSet<PetitionForQuranCourse> PetitionForQuranCourses { get; set; }
    #endregion
}