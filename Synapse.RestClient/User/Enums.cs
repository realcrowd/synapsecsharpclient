using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.User
{
    public enum SynapsePermission
    {
        Unverified,
        SendAndReceive,
        ReceiveOnly,
        Locked
    }

    internal class SynapsePermissionEnumConverter : EnumConverter<SynapsePermission>
    {
        protected Dictionary<SynapsePermission, string> _lookup = new Dictionary<SynapsePermission, string>()
        {
            { SynapsePermission.Unverified, "UNVERIFIED" },
            { SynapsePermission.ReceiveOnly, "RECEIVE" },
            { SynapsePermission.SendAndReceive, "SEND-AND-RECEIVE" },
            { SynapsePermission.Locked, "LOCKED" },
        };

        protected override Dictionary<SynapsePermission, string> Lookup { get { return _lookup; } }
    }

    public enum SynapsePhysicalDocumentType
    {
        GovtId,
        Selfie,
        ProofOfAddress,
        ProofOfIncome,
        ProofOfAccount,
        Authorization,
        SSNCard,
        EINDoc,
        W9Doc,
        W2Doc,
        VoidedCheck,
        AOI,
        BylawsDoc,
        LOE,
        CIPDoc,
        SubscriptionAgreement,
        PromissoryNote,
        LegalAgreement,
        Other
    }

    internal class SynapsePhysicalDocumentTypeEnumConverter : EnumConverter<SynapsePhysicalDocumentType>
    {
        protected Dictionary<SynapsePhysicalDocumentType, string> _lookup = new Dictionary<SynapsePhysicalDocumentType, string>()
        {
            { SynapsePhysicalDocumentType.GovtId, "GOVT_ID" },
            { SynapsePhysicalDocumentType.Selfie, "SELFIE" },
            { SynapsePhysicalDocumentType.ProofOfAddress, "PROOF_OF_ADDRESS" },
            { SynapsePhysicalDocumentType.ProofOfIncome, "PROOF_OF_INCOME" },
            { SynapsePhysicalDocumentType.ProofOfAccount, "PROOF_OF_ACCOUNT" },
            { SynapsePhysicalDocumentType.Authorization, "AUTHORIZATION" },
            { SynapsePhysicalDocumentType.SSNCard, "SSN_CARD" },
            { SynapsePhysicalDocumentType.EINDoc, "EIN_DOC" },
            { SynapsePhysicalDocumentType.W9Doc, "W9_DOC" },
            { SynapsePhysicalDocumentType.W2Doc, "W2_DOC" },
            { SynapsePhysicalDocumentType.VoidedCheck, "VOIDED_CHECK" },
            { SynapsePhysicalDocumentType.AOI, "AOI" },
            { SynapsePhysicalDocumentType.BylawsDoc, "BYLAWS_DOC" },
            { SynapsePhysicalDocumentType.LOE, "LOE" },
            { SynapsePhysicalDocumentType.CIPDoc, "CIP_DOC" },
            { SynapsePhysicalDocumentType.SubscriptionAgreement, "SUBSCRIPTION_AGREEMENT" },
            { SynapsePhysicalDocumentType.PromissoryNote, "PROMISSORY_NOTE" },
            { SynapsePhysicalDocumentType.LegalAgreement, "LEGAL_AGREEMENT" },
            { SynapsePhysicalDocumentType.Other, "OTHER" }
        };

        protected override Dictionary<SynapsePhysicalDocumentType, string> Lookup { get { return _lookup; } }
    }

    public enum SynapseVirtualDocumentType
    {
        SSN,
        Passport,
        DriversLicense,
        TIN,
        DUNS,
        Other
    }

    internal class SynapseVirtualDocumentTypeEnumConverter : EnumConverter<SynapseVirtualDocumentType>
    {
        protected Dictionary<SynapseVirtualDocumentType, string> _lookup = new Dictionary<SynapseVirtualDocumentType, string>()
        {
            { SynapseVirtualDocumentType.SSN, "SSN" },
            { SynapseVirtualDocumentType.Passport, "PASSPORT" },
            { SynapseVirtualDocumentType.DriversLicense, "DRIVERS_LICENSE" },
            { SynapseVirtualDocumentType.TIN, "TIN" },
            { SynapseVirtualDocumentType.DUNS, "DUNS" },
            { SynapseVirtualDocumentType.Other, "OTHER" },
        };

        protected override Dictionary<SynapseVirtualDocumentType, string> Lookup { get { return _lookup; } }
    }

    public enum SynapseSocialDocumentType
    {
        Facebook,
        LinkedIn,
        Twitter,
        Other,
        // These are output only
        Email,
        PhoneNumber
    }

    internal class SynapseSocialDocumentTypeEnumConverter : EnumConverter<SynapseSocialDocumentType>
    {
        protected Dictionary<SynapseSocialDocumentType, string> _lookup = new Dictionary<SynapseSocialDocumentType, string>()
        {
            { SynapseSocialDocumentType.Facebook, "FACEBOOK" },
            { SynapseSocialDocumentType.LinkedIn, "LINKEDIN" },
            { SynapseSocialDocumentType.Twitter, "TWITTER" },
            { SynapseSocialDocumentType.Other, "OTHER" },
            // These are output only
            { SynapseSocialDocumentType.Email, "EMAIL" },
            { SynapseSocialDocumentType.PhoneNumber, "PHONE_NUMBER" },
        };

        protected override Dictionary<SynapseSocialDocumentType, string> Lookup { get { return _lookup; } }
    }

    public enum SynapseEntityType
    {
        Male,
        Female,
        Other,
        NotKnown,
        LLC,
        Corporation,
        Partnership,
        SoleProprietorship,
        Trust,
        Estate
    }

    internal class SynapseEntityTypeEnumConverter : EnumConverter<SynapseEntityType>
    {
        protected Dictionary<SynapseEntityType, string> _lookup = new Dictionary<SynapseEntityType, string>()
        {
            { SynapseEntityType.Male, "M" },
            { SynapseEntityType.Female, "F" },
            { SynapseEntityType.Other, "O" },
            { SynapseEntityType.NotKnown, "NOT_KNOWN" },
            { SynapseEntityType.LLC, "LLC" },
            { SynapseEntityType.Corporation, "CORP" },
            { SynapseEntityType.Partnership, "PARTNERSHIP" },
            { SynapseEntityType.SoleProprietorship, "SOLE-PROPRIETORSHIP" },
            { SynapseEntityType.Trust, "TRUST" },
            { SynapseEntityType.Estate, "ESTATE" },
        };

        protected override Dictionary<SynapseEntityType, string> Lookup { get { return _lookup; } }
    }

    public enum SynapseEntityScope
    {
        NotKnown,
        Airport,
        ArtsAndEntertainment,
        Automotive,
        BankAndFinancialServices,
        Bar,
        BookStore,
        BusinessServices,
        ReligiousOrganization,
        Club,
        CommunityOrGovernment,
        ConcertVenue,
        Doctor,
        EventPlanningOrEventServices,
        FoodOrGrocery,
        HealthOrMedicalOrPharmacy,
        HomeImprovement,
        HospitalOrClinic,
        Hotel,
        Landmark,
        Lawyer,
        Library,
        LicensedFinancialRepresentative,
        LocalBusiness,
        MiddleSchool,
        MovieTheater,
        MuseumOrArtGallery,
        OutdoorGearOrSportingGoods,
        PetServices,
        ProfessionalServices,
        PublicPlaces,
        RealEstate,
        RestaurantOrCafe,
        School,
        ShoppingOrRetail,
        SpasOrBeautyOrPersonalCare,
        SportsVenue,
        SportsOrRecreationOrActivities,
        ToursOrSightseeing,
        TrainStation,
        Transportation,
        University,
        AerospaceOrDefense,
        AutomobilesAndParts,
        BankOrFinancialInstitution,
        Biotechnology,
        Cause,
        Chemicals,
        CommunityOrganization,
        Company,
        ComputersOrTechnology,
        ConsultingOrBusinessServices,
        Education,
        ElementarySchool,
        EnergyOrUtility,
        EngineeringOrConstruction,
        FarmingOrAgriculture,
        FoodOrBeverages,
        GovernmentOrganization,
        HealthOrBeauty,
        HealthOrMedicalOrPharmaceuticals,
        Industrials,
        InsuranceCompany,
        InternetOrSoftware,
        LegalOrLaw,
        MediaOrNewsOrPublishing,
        MiningOrMaterials,
        NonGovernmentalOrganization,
        NonProfitOrganization,
        Organization,
        PoliticalOrganization,
        PoliticalParty,
        Preschool,
        RetailAndConsumerMerchandise,
        SmallBusiness,
        Telecommunication,
        TransportOrFreight,
        TravelOrLeisure
    }

    internal class SynapseEntityScopeEnumConverter : EnumConverter<SynapseEntityScope>
    {
        protected Dictionary<SynapseEntityScope, string> _lookup = new Dictionary<SynapseEntityScope, string>()
        {
            { SynapseEntityScope.NotKnown, "Not Known" },
            { SynapseEntityScope.Airport, "Airport" },
            { SynapseEntityScope.ArtsAndEntertainment, "Arts & Entertainment" },
            { SynapseEntityScope.Automotive, "Automotive" },
            { SynapseEntityScope.BankAndFinancialServices, "Bank & Financial Services" },
            { SynapseEntityScope.Bar, "Bar" },
            { SynapseEntityScope.BookStore, "Book Store" },
            { SynapseEntityScope.BusinessServices, "Business Services" },
            { SynapseEntityScope.ReligiousOrganization, "Religious Organization" },
            { SynapseEntityScope.Club, "Club" },
            { SynapseEntityScope.CommunityOrGovernment, "Community/Government" },
            { SynapseEntityScope.ConcertVenue, "Concert Venue" },
            { SynapseEntityScope.Doctor, "Doctor" },
            { SynapseEntityScope.EventPlanningOrEventServices, "Event Planning/Event Services" },
            { SynapseEntityScope.FoodOrGrocery, "Food/Grocery" },
            { SynapseEntityScope.HealthOrMedicalOrPharmacy, "Health/Medical/Pharmacy" },
            { SynapseEntityScope.HomeImprovement, "Home Improvement" },
            { SynapseEntityScope.HospitalOrClinic, "Hospital/Clinic" },
            { SynapseEntityScope.Hotel, "Hotel" },
            { SynapseEntityScope.Landmark, "Landmark" },
            { SynapseEntityScope.Lawyer, "Lawyer" },
            { SynapseEntityScope.Library, "Library" },
            { SynapseEntityScope.LicensedFinancialRepresentative, "Licensed Financial Representative" },
            { SynapseEntityScope.LocalBusiness, "Local Business" },
            { SynapseEntityScope.MiddleSchool, "Middle School" },
            { SynapseEntityScope.MovieTheater, "Movie Theater" },
            { SynapseEntityScope.MuseumOrArtGallery, "Museum/Art Gallery" },
            { SynapseEntityScope.OutdoorGearOrSportingGoods, "Outdoor Gear/Sporting Goods" },
            { SynapseEntityScope.PetServices, "Pet Services" },
            { SynapseEntityScope.ProfessionalServices, "Professional Services" },
            { SynapseEntityScope.PublicPlaces, "Public Places" },
            { SynapseEntityScope.RealEstate, "Real Estate" },
            { SynapseEntityScope.RestaurantOrCafe, "Restaurant/Cafe" },
            { SynapseEntityScope.School, "School" },
            { SynapseEntityScope.ShoppingOrRetail, "Shopping/Retail" },
            { SynapseEntityScope.SpasOrBeautyOrPersonalCare, "Spas/Beauty/Personal Care" },
            { SynapseEntityScope.SportsVenue, "Sports Venue" },
            { SynapseEntityScope.SportsOrRecreationOrActivities, "Sports/Recreation/Activities" },
            { SynapseEntityScope.ToursOrSightseeing, "Tours/Sightseeing" },
            { SynapseEntityScope.TrainStation, "Train Station" },
            { SynapseEntityScope.Transportation, "Transportation" },
            { SynapseEntityScope.University, "University" },
            { SynapseEntityScope.AerospaceOrDefense, "Aerospace/Defense" },
            { SynapseEntityScope.AutomobilesAndParts, "Automobiles and Parts" },
            { SynapseEntityScope.BankOrFinancialInstitution, "Bank/Financial Institution" },
            { SynapseEntityScope.Biotechnology, "Biotechnology" },
            { SynapseEntityScope.Cause, "Cause" },
            { SynapseEntityScope.Chemicals, "Chemicals" },
            { SynapseEntityScope.CommunityOrganization, "Community Organization" },
            { SynapseEntityScope.Company, "Company" },
            { SynapseEntityScope.ComputersOrTechnology, "Computers/Technology" },
            { SynapseEntityScope.ConsultingOrBusinessServices, "Consulting/Business Services" },
            { SynapseEntityScope.Education, "Education" },
            { SynapseEntityScope.ElementarySchool, "Elementary School" },
            { SynapseEntityScope.EnergyOrUtility, "Energy/Utility" },
            { SynapseEntityScope.EngineeringOrConstruction, "Engineering/Construction" },
            { SynapseEntityScope.FarmingOrAgriculture, "Farming/Agriculture" },
            { SynapseEntityScope.FoodOrBeverages, "Food/Beverages" },
            { SynapseEntityScope.GovernmentOrganization, "Government Organization" },
            { SynapseEntityScope.HealthOrBeauty, "Health/Beauty" },
            { SynapseEntityScope.HealthOrMedicalOrPharmaceuticals, "Health/Medical/Pharmaceuticals" },
            { SynapseEntityScope.Industrials, "Industrials" },
            { SynapseEntityScope.InsuranceCompany, "Insurance Company" },
            { SynapseEntityScope.InternetOrSoftware, "Internet/Software" },
            { SynapseEntityScope.LegalOrLaw, "Legal/Law" },
            { SynapseEntityScope.MediaOrNewsOrPublishing, "Media/News/Publishing" },
            { SynapseEntityScope.MiningOrMaterials, "Mining/Materials" },
            { SynapseEntityScope.NonGovernmentalOrganization, "Non-Governmental Organization (NGO)" },
            { SynapseEntityScope.NonProfitOrganization, "Non-Profit Organization" },
            { SynapseEntityScope.Organization, "Organization" },
            { SynapseEntityScope.PoliticalOrganization, "Political Organization" },
            { SynapseEntityScope.PoliticalParty, "Political Party" },
            { SynapseEntityScope.Preschool, "Preschool" },
            { SynapseEntityScope.RetailAndConsumerMerchandise, "Retail and Consumer Merchandise" },
            { SynapseEntityScope.SmallBusiness, "Small Business" },
            { SynapseEntityScope.Telecommunication, "Telecommunication" },
            { SynapseEntityScope.TransportOrFreight, "Transport/Freight" },
            { SynapseEntityScope.TravelOrLeisure, "Travel/Leisure" },
        };

        protected override Dictionary<SynapseEntityScope, string> Lookup { get { return _lookup; } }
    }
}