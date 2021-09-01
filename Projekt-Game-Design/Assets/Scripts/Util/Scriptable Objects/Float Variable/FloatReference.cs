using System;

namespace Util.ScriptableObjects {
    
    [Serializable]
    public class FloatReference {
        public bool UseConstant = true;
        public float ConstantValue;
        public FloatVariable Variable;
        
        public float Value {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
    }
}