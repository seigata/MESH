namespace MESH.Core
{
    using MESH.Common;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class MeshInstance
    {
        private MeshModel? modelInstance;

        private string dllFile = "";


        public MeshInstance(string dllFile)
        {
            this.dllFile = dllFile;
        }


        public void StartModel()
        {
            // Get valid model type from DLL
            GetModelInstance();

            // Setup Interupt for resets
            // TOODO
            if (modelInstance != null)
            {

                // Run Loop
                modelInstance.Init();

                // TODO Change to use interupt in while
                while (true)
                {
                    modelInstance.DoInputs();
                    modelInstance.DoCycle();
                    MESHModelOutputs output =  modelInstance.DoOutputs();

                }
            }
            else
            {
                LogError($"Cannot find the model interface in {dllFile}.");
            }

        }

        public void CheckDLLCommonAssemblyReference(Assembly dll)
        {
            AssemblyName myCommonAssemblyReference = AssemblyName.GetAssemblyName(typeof(MeshModel).Assembly.Location);
            AssemblyName? meshModelAssemblyReference = null;

            foreach (AssemblyName ass in dll.GetReferencedAssemblies())
            {
                if (ass.Name == myCommonAssemblyReference.Name)
                {
                    meshModelAssemblyReference = ass;
                }
            }

            if(meshModelAssemblyReference != null)
            {
                if (meshModelAssemblyReference != myCommonAssemblyReference)
                {
                    Console.WriteLine("MESH.Common Assembly Versions between MESH and the Model do not match.");
                }

                Console.WriteLine($"MESH using Common Assembly Version {myCommonAssemblyReference.Version}");
                Console.WriteLine($"{dll.GetName().Name} us using Common Assembly Version {meshModelAssemblyReference.Version}");
            }
            else
            {
                LogError("DLL does not seem to reference MESH.Common assembly");
            }
        }

        // Initialisation Methods

        /// <summary>
        /// Method to find a type in the DLL file that matches valid model interface.
        /// </summary>
        public void GetModelInstance()
        {
            // Dynamically load DLL
            var dll = Assembly.LoadFile(dllFile);

            CheckDLLCommonAssemblyReference(dll);


            LogMessage($"Looking for valid model type in library {dllFile}");
            // Run Init/Reset
            foreach (Type type in dll.GetExportedTypes().Where(r => !r.IsAbstract && r.IsClass))
            {
                if (type.IsAssignableTo(typeof(MeshModel)))
                {
                    try
                    {
                        object? instance = Activator.CreateInstance(type);
                        if (instance != null)
                        {
                            modelInstance = (MeshModel)instance;
                        }
                    }
                    catch
                    {
                        Console.Write($"Failed to initialise model from type {type.Name}");
                    }
                }
            }
        }


        public static void LogError(string error)
        {
            Console.WriteLine(error);
        }

        public static void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void LogWarning(string warning)
        {
            Console.WriteLine(warning);
        }

    }
}

