using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESH.Common
{
    public class MeshModel
    {
        public virtual void Init()
        {
            throw new NotImplementedException();
        }
        public virtual MESH.Common.MESHModelOutputs DoOutputs()
        {
            throw new NotImplementedException();
        }
        public virtual void DoInputs()
        {
            throw new NotImplementedException();
        }
        public virtual void DoCycle()
        {
            throw new NotImplementedException();
        }

        public string GetCommonAssemblyVersion()
        {
            return GetMyCommonAssemblyVersion();
        }

        public static string GetMyCommonAssemblyVersion()
        {
            string version = "FileVersion not found";
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            if (!string.IsNullOrEmpty(fvi.FileVersion))
            {
                version = fvi.FileVersion;
            }
            return version;
        }


    }
}
