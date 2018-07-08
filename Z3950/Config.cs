using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Linq;
using System.Xml;
using System.Linq.Expressions;
using System.Reflection;

namespace Z3950
{
    
    public class Config:IConfigurationSectionHandler
    {
        public Config()
        {
        }
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            var list = typeof(Server).GetProperties();
            Func<XmlNode, Server> getServer = (obj) =>
             {
                

                 return Expression.Lambda<Func<Server>>(Expression.MemberInit(Expression.New(typeof(Server)),
                    obj.Attributes.Cast<XmlAttribute>().Select(tmp =>
                    {
                        var p = list.FirstOrDefault(t => string.Equals(t.Name, tmp.Name,
       StringComparison.CurrentCultureIgnoreCase));
                        if (p == null) return null;
                        return Expression.Bind(p as MemberInfo, Expression.Constant(Convert.ChangeType(tmp.Value, p.PropertyType)));
                    }).Where(tmp => tmp != null).ToArray())).Compile()();
             };
            Action<string, Server> body = (name,that) =>
              {
                  if (string.IsNullOrEmpty(name)) return ;
                  var node = section.ChildNodes.Cast<XmlNode>()
                  .FirstOrDefault(obj =>
                  {
                      var nameAttr = obj.Attributes.GetNamedItem("name");
                      return string.Equals(nameAttr != null ? nameAttr.Value : "",
                      name, StringComparison.CurrentCultureIgnoreCase);
                  });
                  if (node == null) return ;
                  node.Attributes.Cast<XmlAttribute>().ToList().ForEach(tmp =>
                  {
                      var p = list.FirstOrDefault(t => string.Equals(t.Name, tmp.Name,
     StringComparison.CurrentCultureIgnoreCase));
                      if (p == null) return;
                      p.SetValue(that, Convert.ChangeType(tmp.Value, p.PropertyType), null);
                  });
              };
            return body;
        }
    }
}
