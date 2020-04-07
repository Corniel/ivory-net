using System;
using System.Xml.Linq;

namespace Ivory.Xml
{
    public interface IXNameTransform
    {
        XName Tranform(XName name);
    }
}
