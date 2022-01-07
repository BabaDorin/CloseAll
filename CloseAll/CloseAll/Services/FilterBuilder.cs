using CloseAll.Contracts;
using CloseAll.FilterRules;

namespace CloseAll.Services
{
    internal class FilterBuilder
    {
        private List<IRule> rules;
        private List<string> exceptions;

        public FilterBuilder()
        {
            rules = new List<IRule>();
            exceptions = new List<string>();
        }

        public FilterBuilder Except(string processName)
        {
            exceptions.Add(processName);
            
            return this;
        }

        public FilterBuilder EnableWhiteList(IWhiteListManager whiteListManager)
        {
            rules.Add(new WhitelistRule(whiteListManager));

            return this;
        }

        public FilterBuilder IgnoreStartup(IProcessManager processManager)
        {
            rules.Add(new IgnoreStartupRule(processManager));

            return this;
        }

        public Filter Build()
        {
            if (exceptions != null && exceptions.Count > 0)
                rules.Add(new ExceptRule(exceptions));

            return new Filter(rules.ToArray());
        }
    }
}
