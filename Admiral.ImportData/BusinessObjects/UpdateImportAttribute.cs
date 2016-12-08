using System;
using System.Collections;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.Linq;

namespace Admiral.ImportData
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class UpdateImportAttribute : Attribute
    {
        public string KeyMember { get; set; }

        public UpdateImportAttribute(string keyMember)
        {
            this.KeyMember = keyMember;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class FindObjectProviderAttribute : Attribute
    {

        public FindObjectProviderAttribute()
        {
        }

        public void Reset()
        {
            cache.Clear();
        }

        private Dictionary<Type, Dictionary<object,object>> cache = new Dictionary<Type, Dictionary<object,object>>();

        public virtual List<object> FindObject(IObjectSpace os, Type t, CriteriaOperator criteria, bool inTrans)
        {
            if (!cache.ContainsKey(t))
            {
                
                var allObjects = os.GetObjects(t, null, inTrans).OfType<XPBaseObject>();
                var dict = new Dictionary<object, object>();
                var m = ((criteria as BinaryOperator).LeftOperand as OperandProperty).PropertyName;
                foreach (var o in allObjects)
                {
                    dict.Add(o.GetMemberValue(m)+"", o);
                }
                cache.Add(t, dict);
            }
            var tt = cache[t];
            var r = ((criteria as BinaryOperator).RightOperand as OperandValue).Value;
            return tt.Where(x=> object.Equals(x.Key , r.ToString())).Select(x=>x.Value).ToList();
        }
    }
}