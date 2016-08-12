using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public enum SynapseNodeType
    {
        Unknown,
        SynapseUS,
        ReserveUS,
        ACHUS,
        WireUS,
        WireInt,
        IOU,
        SynapseIND,
        SynapseNP,
        EFTIND,
        EFTNP
    }

    internal class SynapseNodeTypeEnumConverter : EnumConverter<SynapseNodeType>
    {
        protected Dictionary<SynapseNodeType, string> _lookup = new Dictionary<SynapseNodeType, string>()
        {
            { SynapseNodeType.SynapseUS, "SYNAPSE-US" },
            { SynapseNodeType.ReserveUS, "RESERVE-US" },
            { SynapseNodeType.ACHUS, "ACH-US" },
            { SynapseNodeType.WireUS, "WIRE-US" },
            { SynapseNodeType.WireInt, "WIRE-INT" },
            { SynapseNodeType.IOU, "IOU" },
            { SynapseNodeType.SynapseIND, "SYNAPSE-IND" },
            { SynapseNodeType.SynapseNP, "SYNAPSE-NP" },
            { SynapseNodeType.EFTIND, "EFT-IND" },
            { SynapseNodeType.EFTNP, "EFT-NP" }
        };

        protected override Dictionary<SynapseNodeType, string> Lookup { get { return _lookup; } }

        public SynapseNodeTypeEnumConverter() : base() { }
        public SynapseNodeTypeEnumConverter(object notFoundValue) : base(notFoundValue) { }
    }


    public enum SynapseACHNodeType
    {
        Unknown,
        Business,
        Personal        
    }

    internal class SynapseACHNodeTypeEnumConverter : EnumConverter<SynapseACHNodeType>
    {
        protected Dictionary<SynapseACHNodeType, string> _lookup = new Dictionary<SynapseACHNodeType, string>()
        {
            { SynapseACHNodeType.Business, "BUSINESS" },
            { SynapseACHNodeType.Personal, "PERSONAL" },
        };

        protected override Dictionary<SynapseACHNodeType, string> Lookup { get { return _lookup; } }

        public SynapseACHNodeTypeEnumConverter() : base() { }
        public SynapseACHNodeTypeEnumConverter(object notFoundValue) : base(notFoundValue) { }
    }

    public enum SynapseACHNodeClass
    {
        Unknown,
        Savings,
        Checking
    }

    internal class SynapseACHNodeClassEnumConverter : EnumConverter<SynapseACHNodeClass>
    {
        protected Dictionary<SynapseACHNodeClass, string> _lookup = new Dictionary<SynapseACHNodeClass, string>()
        {
            { SynapseACHNodeClass.Savings, "SAVINGS" },
            { SynapseACHNodeClass.Checking, "CHECKING" },
        };

        protected override Dictionary<SynapseACHNodeClass, string> Lookup { get { return _lookup; } }

        public SynapseACHNodeClassEnumConverter() : base() { }
        public SynapseACHNodeClassEnumConverter(object notFoundValue) : base(notFoundValue) { }
    }

    public enum SynapseNodePermission
    {
        Unknown,
        Credit,
        CreditAndDebit,
        Locked
    }

    internal class SynapseNodePermissionEnumConverter : EnumConverter<SynapseNodePermission>
    {
        protected Dictionary<SynapseNodePermission, string> _lookup = new Dictionary<SynapseNodePermission, string>()
        {
            { SynapseNodePermission.Credit, "CREDIT" },
            { SynapseNodePermission.CreditAndDebit, "CREDIT-AND-DEBIT" },
            { SynapseNodePermission.Locked, "LOCKED" },
        };

        protected override Dictionary<SynapseNodePermission, string> Lookup { get { return _lookup; } }

        public SynapseNodePermissionEnumConverter() : base() { }
        public SynapseNodePermissionEnumConverter(object notFoundValue) : base(notFoundValue) { }
    }
}
