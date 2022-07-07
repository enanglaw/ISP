using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ISPoliceAppApi.DSR;
using ISPoliceAppApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace ISPoliceAppApi.Data
{
    public partial class ISPoliceAppApiDbContext : DbContext
    {
        public ISPoliceAppApiDbContext()
        {
        }

        public ISPoliceAppApiDbContext(DbContextOptions<ISPoliceAppApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WorkflowMaster> WorkflowMaster { get; set; }
        public virtual DbSet<Venue> Venue { get; set; }
        public virtual DbSet<VenuePermissionType> VenuePermissionType { get; set; }
        public virtual DbSet<CategoryMaster> CategoryMaster { get; set; }
        public virtual DbSet<DistrictMaster> DistrictMaster { get; set; }
        public virtual DbSet<StationMaster> StationMaster { get; set; }
        public virtual DbSet<SubCategoryMaster> SubCategoryMaster { get; set; }
        public virtual DbSet<ZoneMaster> ZoneMaster { get; set; }
        //DSR Start
        public virtual DbSet<ControlRoomDSR> ControlRoomDSR { get; set; }
        public virtual DbSet<ControlRoomDSRAccused> ControlRoomDSRAccused { get; set; }
        public virtual DbSet<ControlRoomDSRCategory> ControlRoomDSRCategory { get; set; }
        public virtual DbSet<ControlRoomDSRAccusedDetail> ControlRoomDSRAccusedDetail { get; set; }
        //DSR End
        public virtual DbSet<AllegationEnquiry> Enquiries { get; set; } 
        public virtual DbSet<AllegationEnquiryDocument> AllegationEnquiryDocuments { get; set; }
        public virtual DbSet<AllegationNote> AllegationNotes { get; set; }
        public virtual DbSet<Memorandum> Memoranda { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonAddress> PersonAddress { get; set; }
        public virtual DbSet<PersonAliasName> PersonAliasName { get; set; }
        public virtual DbSet<PersonCaseHistory> PersonCaseHistory { get; set; }
        public virtual DbSet<PersonMedia> PersonMedia { get; set; }
        public virtual DbSet<PersonPersonType> PersonPersonType { get; set; }
        public virtual DbSet<PersonRivalGang> PersonRivalGang { get; set; }
        public virtual DbSet<PersonStatusMaster> PersonStatusMaster { get; set; }
        public virtual DbSet<PersonTypeMaster> PersonTypeMaster { get; set; }
        public virtual DbSet<ProfileMaster> ProfileMaster { get; set; }
        public virtual DbSet<ProfileAlias> ProfileAlias { get; set; }
        public virtual DbSet<ProfileAssociates> ProfileAssociates { get; set; }
        public virtual DbSet<ProfileTransaction> ProfileTransaction { get; set; }
        public virtual DbSet<ProfileSpouse> ProfileSpouse { get; set; }
        public virtual DbSet<ProfileChildren> ProfileChildren { get; set; }
        public virtual DbSet<ProfileSibling> ProfileSibling { get; set; }
        public virtual DbSet<ProfileAbstract> ProfileAbstract { get; set; }
        public virtual DbSet<CaseDetail> CaseDetail { get; set; }
        #region Global
        public virtual DbSet<MaritalStatus> Maritals { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<Allegation> Allegations { get; set; }
        public  virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        #endregion

        #region Leaders Organization
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationEvent> OrganizationEvents { get; set; }
        public virtual DbSet<OrganizationMedia> OrganizationMedias { get; set; }
        public virtual DbSet<SubOrganizationCategory> SubOrganizationCategories { get; set; }
        public virtual DbSet<SubOrganizationEvent> SubOrganizationEvents { get; set; }
        public virtual DbSet<Organization_Sub_Events_Rs> Organization_Sub_Events_Rs { get; set; }
        public virtual DbSet<Organization_Sub_Leaders_Rs> Organization_Sub_Leaders_Rs { get; set; }
        public virtual DbSet<SubOrganizationLeaders> SubOrganizationLeaders { get; set; }

        #endregion
        #region Leaders
        public virtual DbSet<LeadersGroup> LeadersGroups { get; set; }
        #endregion

        #region Leader
        public virtual DbSet<Leader> Leaders { get; set; }
        public virtual DbSet<LeaderEvent> LeaderEvents { get; set; }
        public virtual DbSet<LeaderMedia> LeaderMedias { get; set; }
        public virtual DbSet<LeaderPoliticalBackground> LeaderPoliticalBackgrounds { get; set; }
        #endregion
        #region Personnel

        public virtual DbSet<Personnel> Personnels { get; set; }
        public virtual DbSet<PersonnelAllegationEnquiry> AllegationEnquiries { get; set; }
        public virtual DbSet<PersonnelCaseDetail> PersonnelCaseDetails { get; set; }
        public virtual DbSet<PersonnelEducationalBackground> PersonnelEducationalBackgrounds { get; set; }
        public virtual DbSet<PersonnelChildren> PersonnelChildrens { get; set; }
        public virtual DbSet<PersonnelSpouse> PersonnelSpouses { get; set; }
        public virtual DbSet<PersonnelFamily> PersonnelFamilies { get; set; }
        public virtual DbSet<PersonnelGallantryAward> PersonnelGallantryAwards { get; set; }
        public virtual DbSet<PersonnelPosting> PersonnelPostings { get; set; }
        public virtual DbSet<PersonnelPreviousAllegation> PersonnelPreviousAllegations { get; set; }
        public virtual DbSet<PersonnelWarningOrPunishment> PersonnelWarningOrPunishments { get; set; }

        #endregion
        #region Reports

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Your Connection string goes here");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();
                var connectionString = configuration.GetConnectionString("connString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowMaster>(entity =>
            {
                entity.HasKey(e => e.WorkflowId)
                    .HasName("PK__WorkflowMaster");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.WorkflowName).IsUnicode(false);
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())").ValueGeneratedOnAdd();
                entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))").ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedOn).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.CreatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.UpdatedOn).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.UpdatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<VenuePermissionType>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())").ValueGeneratedOnAdd();
                entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())").ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.CreatedOn).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.CreatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.UpdatedOn).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                entity.Property(e => e.UpdatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.VenuePermissionType)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VenuePermissionType_Venue");
            });
           
            modelBuilder.Entity<CategoryMaster>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DistrictMaster>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.DistrictMaster)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DistrictMaster_ZoneMaster");
            });

            modelBuilder.Entity<StationMaster>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.StationMaster)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationMaster_DistrictMaster");
            });

            modelBuilder.Entity<SubCategoryMaster>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategoryMaster)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategoryMaster_CategoryMaster");
            });

            modelBuilder.Entity<ZoneMaster>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Person_PersonStatusMaster");
            });


            modelBuilder.Entity<PersonAddress>(entity =>
            {
                entity.Property(e => e.AddressLabel).HasDefaultValueSql("(N'')");
                entity.Property(e => e.AddressText).HasDefaultValueSql("('')");
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAddress)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonAddress_Person");
            });

            modelBuilder.Entity<PersonAliasName>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonAliasName)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonAliasName_Person");
            });

            modelBuilder.Entity<PersonCaseHistory>(entity =>
            {
                // entity.HasOne(d => d.Case)
                //     .WithMany(p => p.PersonCaseHistory)
                //     .HasForeignKey(d => d.CaseId)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK_PersonCaseHistory_CaseMaster");

                // entity.HasOne(d => d.CaseStatus)
                //     .WithMany(p => p.PersonCaseHistory)
                //     .HasForeignKey(d => d.CaseStatusId)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK_PersonCaseHistory_CaseStatusMaster");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonCaseHistory)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonCaseHistory_Person");
            });

            modelBuilder.Entity<PersonMedia>(entity =>
            {
                entity.Property(e => e.MediaLabel).HasDefaultValueSql("(N'')");
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonMedia)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonMedia_Person");
            });

            modelBuilder.Entity<PersonPersonType>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonPersonType)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonPersonType_Person");

                entity.HasOne(d => d.PersonType)
                    .WithMany(p => p.PersonPersonType)
                    .HasForeignKey(d => d.PersonTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PersonPersonType_PersonTypeMaster");
            });

            modelBuilder.Entity<PersonRivalGang>(entity =>
            {
                // entity.HasOne(d => d.Gang)
                //     .WithMany(p => p.PersonRivalGang)
                //     .HasForeignKey(d => d.GangId)
                //     .OnDelete(DeleteBehavior.ClientSetNull)
                //     .HasConstraintName("FK_PersonRivalGang_Gang");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PersonRivalGang)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PersonRivalGang_Person");
            });

            modelBuilder.Entity<PersonTypeMaster>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });
            modelBuilder.Entity<ProfileAssociates>(entity =>
            {
                entity.HasOne(d => d.PrimaryProfileDetail)
                    .WithMany(p => p.ProfileAssociates)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PrimaryProfileDetail_Profile");

                entity.HasOne(d => d.AssociatesProfileDetail)
                    .WithMany(p => p.AssociatesProfiles)
                    .HasForeignKey(d => d.AssociatesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociatesProfile_Profile");
            });
         


            OnModelCreatingPartial(modelBuilder);
        }
        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(cancellationToken));
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return (await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken));
        }
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                // for entities that inherit from BaseEntity,
                // set UpdatedOn / CreatedOn appropriately

                if (entry.Entity is Venue trackable1)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable1.UpdatedOn = utcNow;
                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedOn").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable1.CreatedOn = utcNow;
                            trackable1.UpdatedOn = utcNow;
                            break;
                    }
                }
                else if (entry.Entity is VenuePermissionType trackable2)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            // set the updated date to "now"
                            trackable2.UpdatedOn = utcNow;
                            // mark property as "don't touch"
                            // we don't want to update on a Modify operation
                            entry.Property("CreatedOn").IsModified = false;
                            break;

                        case EntityState.Added:
                            // set both updated and created date to "now"
                            trackable2.CreatedOn = utcNow;
                            trackable2.UpdatedOn = utcNow;
                            break;
                    }
                }
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
