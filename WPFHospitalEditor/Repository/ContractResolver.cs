using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFHospitalEditor.Repository
{
    class ContractResolver : DefaultContractResolver
    {
        private List<String> _propertiesForElimination;

        public ContractResolver()
        {
            _propertiesForElimination = new List<String>();
            _propertiesForElimination.Add("FocusVisualStyle");
            _propertiesForElimination.Add("Dispatcher");
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            properties =
                properties.Where(p => !_propertiesForElimination.Contains(p.PropertyName)).ToList();

            return properties;
        }
    }
}
