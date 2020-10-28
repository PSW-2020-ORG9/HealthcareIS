using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities
{
    class IntegerKeyGenerator
    {
        private int currentKey;

        public IntegerKeyGenerator(IEnumerable<int> existingKeys)
        {
            currentKey = -1;
            foreach (int key in existingKeys)
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
