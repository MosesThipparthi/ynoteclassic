using System;
using System.Collections;
using Nini.Util;

namespace Nini.Ini
{
    /// <include file='IniSectionCollection.xml' path='//Class[@name="IniSectionCollection"]/docs/*' />
    public class IniSectionCollection : ICollection, IEnumerable
    {
        #region Private variables

        private readonly OrderedList list = new OrderedList();

        #endregion Private variables

        #region Public properties

        /// <include file='IniSectionCollection.xml' path='//Property[@name="ItemIndex"]/docs/*' />
        public IniSection this[int index]
        {
            get { return (IniSection) list[index]; }
        }

        /// <include file='IniSectionCollection.xml' path='//Property[@name="ItemName"]/docs/*' />
        public IniSection this[string configName]
        {
            get { return (IniSection) list[configName]; }
        }

        /// <include file='IniSectionCollection.xml' path='//Property[@name="Count"]/docs/*' />
        public int Count
        {
            get { return list.Count; }
        }

        /// <include file='IniSectionCollection.xml' path='//Property[@name="SyncRoot"]/docs/*' />
        public object SyncRoot
        {
            get { return list.SyncRoot; }
        }

        /// <include file='IniSectionCollection.xml' path='//Property[@name="IsSynchronized"]/docs/*' />
        public bool IsSynchronized
        {
            get { return list.IsSynchronized; }
        }

        #endregion Public properties

        #region Public methods

        /// <include file='IniSectionCollection.xml' path='//Method[@name="CopyTo"]/docs/*' />
        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array, index);
        }

        /// <include file='IniSectionCollection.xml' path='//Method[@name="GetEnumerator"]/docs/*' />
        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <include file='IniSectionCollection.xml' path='//Method[@name="Add"]/docs/*' />
        public void Add(IniSection section)
        {
            if (list.Contains(section))
            {
                throw new ArgumentException("IniSection already exists");
            }

            list.Add(section.Name, section);
        }

        /// <include file='IniSectionCollection.xml' path='//Method[@name="Remove"]/docs/*' />
        public void Remove(string config)
        {
            list.Remove(config);
        }

        /// <include file='IniSectionCollection.xml' path='//Method[@name="CopyToStrong"]/docs/*' />
        public void CopyTo(IniSection[] array, int index)
        {
            ((ICollection) list).CopyTo(array, index);
        }

        #endregion Public methods

        #region Private methods

        #endregion Private methods
    }
}