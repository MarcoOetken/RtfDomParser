/*
 * 
 *   DCSoft RTF DOM v1.0
 *   Author : Yuan yong fu.
 *   Email  : yyf9989@hotmail.com
 *   blog site:http://www.cnblogs.com/xdesigner.
 * 
 */

using System;
using System.Text;

namespace RtfDomParser
{
    /// <summary>
    /// string attribute
    /// </summary>
    [Serializable()]
    public class StringAttribute
    {
        /// <summary>
        /// name
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Name { get; set; }

        /// <summary>
        /// value
        /// </summary>
        [System.ComponentModel.DefaultValue( null)]
        public string Value { get; set; }

        public override string ToString()
        {
            return Name + "=" + Value;
        }
    }

    [Serializable()]
    [System.Diagnostics.DebuggerTypeProxy(typeof(RTFInstanceDebugView))]
    public class StringAttributeCollection : System.Collections.CollectionBase
    {
        // 221026 ## MO: Removed Add and Remove - use this[] instead.
        public string this[string name]
        {
            get
            {
                foreach (StringAttribute attr in this)
                {
                    if (attr.Name == name)
                    {
                        return attr.Value;
                    }
                }
                return null;
            }
            set
            {
                foreach (StringAttribute item in this)
                {
                    if (item.Name == name)
                    {
                        if (value == null)
                            this.List.Remove(item);
                        else
                            item.Value = value;
                        return;
                    }
                }
                if (value != null)
                {
                    StringAttribute newItem = new StringAttribute();
                    newItem.Name = name;
                    newItem.Value = value;
                    this.List.Add(newItem);
                }
            }
        }
    }
}
