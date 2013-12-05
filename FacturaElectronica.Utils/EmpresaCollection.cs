using System;
using System.Collections;
using System.Data;

namespace FacturaElectronica.Utils
{
    public class EmpresaCollection : CollectionBase
    {
        public Empresa this[int index]
        {
            get { return ((Empresa)List[index]); }
            set { List[index] = value; }
        }

        public int Add(Empresa value)
        {
            return (List.Add(value));
        }

        public Boolean Contains(Empresa value)
        {
            return (List.Contains(value));
        }

        public int IndexOf(Empresa value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(Empresa value, int index)
        {
            List.Insert(index, value);
        }

        public void Remove(Empresa value)
        {
            List.Remove(value);
        }
    }
}
