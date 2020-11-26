using System.Collections.Generic;

namespace HealthcareBase.Model.Utilities
{
    internal class IntegerKeyGenerator
    {
        private int currentKey;

        public IntegerKeyGenerator(IEnumerable<int> existingKeys)
        {
            currentKey = -1;
            foreach (var key in existingKeys)
                if (key > currentKey)
                    currentKey = key;
            currentKey++;
        }

        public int GenerateKey()
        {
            return currentKey++;
        }
    }
}