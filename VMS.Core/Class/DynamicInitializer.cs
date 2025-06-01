using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;

namespace VNS.Core.Classes
{
    public class DynamicInitializer 
    {
        public DynamicInitializer()
        {
          //Utility.InitSubSonic(new ConnectionSQL().KhoiTaoKetNoi(), "ORM");
     
        }
        public  V NewInstance<V>() where V : class
        {
            return ObjectGenerator(typeof(V)) as V;
        }

        private  object ObjectGenerator(Type type)
        {
            var target = type.GetConstructor(Type.EmptyTypes);
            var dynamic = new DynamicMethod(string.Empty,
                          type,
                          new Type[0],
                          target.DeclaringType);
            var il = dynamic.GetILGenerator();
            il.DeclareLocal(target.DeclaringType);
            il.Emit(OpCodes.Newobj, target);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            var method = (Func<object>)dynamic.CreateDelegate(typeof(Func<object>));
            return method();
        }
       

        public  object NewInstance(Type type)
        {
            return ObjectGenerator(type);
        }
        public object  CreateInstance(Type type,  object[] thamso)

        {
            var objForm = Activator.CreateInstance(type, thamso);
            return objForm;
        }
       
    }
}
