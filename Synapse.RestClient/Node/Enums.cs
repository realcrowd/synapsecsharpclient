using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synapse.RestClient.Node
{
    public enum SynapseNodeType
    {
        Business,
        Personal        
    }

    internal class SynapseNodeTypeEnumConverter : EnumConverter<SynapseNodeType>
    {
        protected Dictionary<SynapseNodeType, string> _lookup = new Dictionary<SynapseNodeType, string>()
        {
            { SynapseNodeType.Business, "BUSINESS" },
            { SynapseNodeType.Personal, "PERSONAL" },
        };

        protected override Dictionary<SynapseNodeType, string> Lookup { get { return _lookup; } }
    }

    public enum SynapseNodeClass
    {
        Savings,
        Checking
    }

    internal class SynapseNodeClassEnumConverter : EnumConverter<SynapseNodeClass>
    {
        protected Dictionary<SynapseNodeClass, string> _lookup = new Dictionary<SynapseNodeClass, string>()
        {
            { SynapseNodeClass.Savings, "SAVINGS" },
            { SynapseNodeClass.Checking, "CHECKING" },
        };

        protected override Dictionary<SynapseNodeClass, string> Lookup { get { return _lookup; } }
    }

    public enum SynapseNodePermission
    {
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
    }
}
