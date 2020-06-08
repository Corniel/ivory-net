using Ivory.Soap.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Ivory.Xml
{
    /// <summary>Represents a collection of unique <see cref="XmlQualifiedName"/>'s.</summary>
    [DebuggerDisplay("Count: {Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class XmlQualifiedNameCollection : ISet<XmlQualifiedName>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly HashSet<XmlQualifiedName> collection = new HashSet<XmlQualifiedName>();

        /// <inheritdoc/>
        public int Count => collection.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <summary>Adds a <see cref="XmlQualifiedName"/> to the collection.</summary>
        /// <param name="prefix">
        /// The prefix/name of the qualified name.
        /// </param>
        /// <param name="namespace">
        /// The namespace of the qualified name.
        /// </param>
        public XmlQualifiedNameCollection Add(string prefix, string @namespace)
        {
            return Add(new XmlQualifiedName(prefix, @namespace));
        }

        /// <summary>Adds a <see cref="XmlQualifiedName"/> to the collection.</summary>
        /// <param name="qualifiedName">
        /// the qualified name to add.
        /// </param>
        public XmlQualifiedNameCollection Add(XmlQualifiedName qualifiedName)
        {
            Guard.NotNull(qualifiedName, nameof(qualifiedName));
            collection.Add(qualifiedName);
            return this;
        }

        /// <summary>Updates the prefix of the qualified name with the specified namespace.</summary>
        /// <param name="newPrefix">
        /// The new prefix for namespace.
        /// </param>
        /// <param name="namespace">
        /// The namespace of the qualified name.
        /// </param>
        public XmlQualifiedNameCollection UpdatePrefix(string newPrefix, string @namespace)
        {
            var existing = collection.Where(qn => qn.Namespace == @namespace).ToArray();
            for (var i = 0; i < existing.Length; i++)
            {
                Remove(existing[i]);
            }

            return Add(newPrefix, @namespace);
        }

        /// <summary>Represents qualified names as <see cref="XmlSerializerNamespaces"/>.</summary>
        public XmlSerializerNamespaces ToSerializerNamespaces()
        {
            return new XmlSerializerNamespaces(this.ToArray());
        }

        #region ISet & ICollection

        /// <inheritdoc/>
        public void Clear() => collection.Clear();

        /// <inheritdoc/>
        public bool Remove(XmlQualifiedName item) => collection.Remove(item);

        /// <inheritdoc/>
        public void CopyTo(XmlQualifiedName[] array, int arrayIndex) => collection.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        void ICollection<XmlQualifiedName>.Add(XmlQualifiedName item) => Add(item);

        /// <inheritdoc/>
        bool ISet<XmlQualifiedName>.Add(XmlQualifiedName item) => collection.Add(Guard.NotNull(item, nameof(item)));

        /// <inheritdoc/>
        public bool Contains(XmlQualifiedName item) => collection.Contains(item);

        /// <inheritdoc/>
        public void ExceptWith(IEnumerable<XmlQualifiedName> other) => collection.ExceptWith(other);

        /// <inheritdoc/>
        public void IntersectWith(IEnumerable<XmlQualifiedName> other) => collection.IntersectWith(other);

        /// <inheritdoc/>
        public bool IsProperSubsetOf(IEnumerable<XmlQualifiedName> other) => collection.IsProperSubsetOf(other);

        /// <inheritdoc/>
        public bool IsProperSupersetOf(IEnumerable<XmlQualifiedName> other) => collection.IsProperSupersetOf(other);

        /// <inheritdoc/>
        public bool IsSubsetOf(IEnumerable<XmlQualifiedName> other) => collection.IsSubsetOf(other);

        /// <inheritdoc/>
        public bool IsSupersetOf(IEnumerable<XmlQualifiedName> other) => collection.IsSupersetOf(other);

        /// <inheritdoc/>
        public bool Overlaps(IEnumerable<XmlQualifiedName> other) => collection.Overlaps(other);

        /// <inheritdoc/>
        public bool SetEquals(IEnumerable<XmlQualifiedName> other) => collection.SetEquals(other);

        /// <inheritdoc/>
        public void SymmetricExceptWith(IEnumerable<XmlQualifiedName> other) => collection.SymmetricExceptWith(other);

        /// <inheritdoc/>
        public void UnionWith(IEnumerable<XmlQualifiedName> other) => collection.UnionWith(other);

        /// <inheritdoc/>
        public IEnumerator<XmlQualifiedName> GetEnumerator() => collection.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
