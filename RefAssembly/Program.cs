using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestMethod();
            TestMethod3();
        }

        public static List<string> GetSources()
        {
            var sourcePaths = new string[] {
                ""
                //,"D:\\code\\AppCenter\\Beisen.PageBuilder\\Src\\Beisen.UPaaSCore.OpsTools\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CloudConfig.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CloudConfig.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CustomTemplateExport.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CustomTemplateExport.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.DataRecovery.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.DataRecovery.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Cloud.Plugins.ExportServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Cloud.Plugins.ExportServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CloudImport.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CloudImport.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CloudLocalization.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.CloudLocalization.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.MetaDataModifyLog.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.MetaDataModifyLog.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Print.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Print.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Cloud.Plugin.OPS\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Cloud.Plugins\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Cloud.Plugins.Consumer\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Cloud.Plugins.ServiceImp\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.Cloud.Plugins\\Beisen.Cloud.Plugins.ServiceInterface\\bin\\Debug"
                //,"D:\\code\\platform-uiframework\\Beisen.MultiTenantV3\\Beisen.MultiTenant.ServiceImp\\bin\\Debug"
                ,@"D:\code\platform-uiframework\Web.Cloud\BeisenCloud.Rest.WebSite\bin"
                //,@"D:\code\platform-uiframework\packages\Beisen.MultiTenantV5.ServiceImp.1.1.0.2731\lib\net45"
            }.Where(x => !string.IsNullOrEmpty(x)).ToList();
            return sourcePaths;
        }

        /// <summary>
        /// 测试结果贴到这里
        /// </summary>
        public static void TestMethod()
        {
            var sourcePaths = GetSources();
            var targetNames = new string[] {
                    ""
                  //  ,"Beisen.MultiTenantV5.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.File.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.MetaData.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.UI.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.BizData.ServiceInterface.dll"
                    ,"Beisen.PageBuilder.ServiceInterface.dll"
            }.Where(x => !string.IsNullOrEmpty(x)).ToList();
            var lstSourceFile = sourcePaths.SelectMany(x => System.IO.Directory.GetFiles(x).ToList()).ToList();
            lstSourceFile = lstSourceFile.Where(x => x.EndsWith(".dll") || x.EndsWith(".exe")).ToList();
            var lst = lstSourceFile.SelectMany(x => RefAssemblyInfo.isRef(x, targetNames)).ToList();
            var lines = lst.Select(x => string.Format("{0} 引用 {1}", x.Key, x.Value)).ToList();
            var line = string.Join(Environment.NewLine, lines);
        }

        public static void TestMethod2()
        {
            var sourcePaths = GetSources();
            var targetNames = new string[] {
                    ""
                  //  ,"Beisen.MultiTenantV5.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.File.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.MetaData.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.UI.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.BizData.ServiceInterface.dll"
                    ,"Beisen.PageBuilder.ServiceInterface.dll"
            }.Where(x => !string.IsNullOrEmpty(x)).ToList();
            var lstSourceFile = sourcePaths.SelectMany(x => System.IO.Directory.GetFiles(x).ToList()).ToList();
            lstSourceFile = lstSourceFile.Where(x => x.EndsWith(".dll") || x.EndsWith(".exe")).ToList();
            var lst = lstSourceFile.SelectMany(x => RefAssemblyInfo.Refs(x)).ToList();
            var lines = lst.Select(x => string.Format("{0} 引用 {1}", x.Key, x.Value)).ToList();
            var line = string.Join(Environment.NewLine, lines);
        }

        public static void TestMethod3()
        {
            var sourcePaths = GetSources();
            var targetNames = new string[] {
                    ""
                    ,"Beisen.MultiTenantV5.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.File.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.MetaData.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.UI.ServiceInterface.dll"
                    ,"Beisen.MultiTenant.BizData.ServiceInterface.dll"
                    ,"Beisen.PageBuilder.ServiceInterface.dll"
            }.Where(x => !string.IsNullOrEmpty(x)).ToList();
            var lstSourceFile = sourcePaths.SelectMany(x => System.IO.Directory.GetFiles(x).ToList()).ToList();
            lstSourceFile = lstSourceFile.Where(x => x.EndsWith(".dll") || x.EndsWith(".exe")).ToList();
            var lst = lstSourceFile.SelectMany(x => RefAssemblyInfo.RefMothds(x, targetNames)).ToList();
            var lines = lst.Select(x => string.Format("{0} 引用 {1}", x.Key, x.Value)).ToList();
            var line = string.Join(Environment.NewLine, lines);
        }
    }
}
