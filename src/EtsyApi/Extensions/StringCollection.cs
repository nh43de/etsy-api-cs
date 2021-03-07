using System.Collections.Generic;

namespace EtsyApi
{
    public class StringCollection 
    {
        private readonly IEnumerable<string> _strings;

        public StringCollection(IEnumerable<string> strings)
        {
            _strings = strings;
        }

        public override string ToString()
        {
            return string.Join(",", _strings);
        }
    }
}
