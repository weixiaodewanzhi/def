using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefAssembly
{
    public class RefAssemblyInfo
    {
        public static string Rep(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            if (str.ToLower().EndsWith(".dll") || str.ToLower().EndsWith(".exe"))
            {
                return str.Substring(0, str.Length - 4);
            }
            return str;
        }

        public static List<KeyValuePair<string, string>> isRef(string source, List<string> targets)
        {
            targets = targets.Where(x => !string.IsNullOrEmpty(x)).Select(x => Rep(x)).ToList();
            Mono.Cecil.AssemblyDefinition asemblySource = null;
            try
            {
                asemblySource = Mono.Cecil.AssemblyDefinition.ReadAssembly(source);
            }
            catch (Exception ex)
            {
                var aa = source;
                return new List<KeyValuePair<string, string>>();
            }
            var memberReferences = asemblySource.MainModule.GetMemberReferences();
            var lstRefName = memberReferences
                .Select(x => x?.DeclaringType?.Scope?.Name ?? "")
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .Select(x => Rep(x))
                .ToList();
            var qry = from a in lstRefName
                      from b in targets
                      where string.Compare(a, b, true) == 0
                      select b;
            var lst = qry.ToList();
            return lst.Select(x => new KeyValuePair<string, string>(source, x)).ToList();
        }

        public static List<KeyValuePair<string, string>> Refs(string source)
        {
            Mono.Cecil.AssemblyDefinition asemblySource = null;
            try
            {
                asemblySource = Mono.Cecil.AssemblyDefinition.ReadAssembly(source);
            }
            catch (Exception ex)
            {
                var aa = source;
                return new List<KeyValuePair<string, string>>();
            }
            var memberReferences = asemblySource.MainModule.GetMemberReferences();
            var lstRefName = memberReferences
                .Select(x => x?.DeclaringType?.Scope?.Name ?? "")
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .Select(x => Rep(x))
                .ToList();
            return lstRefName.Select(x => new KeyValuePair<string, string>(source, x)).ToList();
        }

        public static List<KeyValuePair<string, string>> RefMothds(string source, List<string> targets)
        {
            targets = targets.Where(x => !string.IsNullOrEmpty(x)).Select(x => Rep(x)).ToList();
            Mono.Cecil.AssemblyDefinition asemblySource = null;
            try
            {
                asemblySource = Mono.Cecil.AssemblyDefinition.ReadAssembly(source);
            }
            catch (Exception ex)
            {
                var aa = source;
                return new List<KeyValuePair<string, string>>();
            }
            var memberReferences = asemblySource.MainModule.GetMemberReferences();
            var lstRefName = memberReferences
                .Where(x => !string.IsNullOrEmpty(x?.DeclaringType?.Scope?.Name ?? ""))
                .Select(x => new { dll = Rep(x?.DeclaringType?.Scope?.Name ?? ""), method = x.FullName })
                .ToList();
            var qry = from a in lstRefName
                      from b in targets
                      where string.Compare(a.dll, b, true) == 0
                      select new { a, b };
            var lst = qry.ToList();
            return lst.Select(x => new KeyValuePair<string, string>(source, x.a.method)).ToList();
        }
    }
}
