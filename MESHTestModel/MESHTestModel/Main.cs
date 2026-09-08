using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MESH.Common;

namespace MESHTestModel
{
    public class DummyModel : MeshModel
    {
        private const float CYCLE_TIME = 215.0f;

        public override void Init()
        {
            Console.WriteLine("MESHTestModel Initiataaaaaaed");
        }

        public override MESH.Common.MESHModelOutputs DoOutputs()
        {
            Console.WriteLine("MESHTestModel Post cycle outputs222");

            MESH.Common.MESHModelOutputs dataset = new MESH.Common.MESHModelOutputs();

            return dataset;
        }
        public override void DoInputs()
        {
            Console.WriteLine("MESHTestModel pre cycle inputs222");
        }
        public override void DoCycle()
        {

            Console.WriteLine("MESHTestModel Do Cycle222");
        }

        public float GetCycleTime()
        {
            return CYCLE_TIME;
        }
    }

}
