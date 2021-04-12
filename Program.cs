using System;
using System.IO;
using Scriban;

namespace cs
{
    class Program
    {
        class ClassProperty
        {
            public string Name { get; set; }
            public bool IsServerOnly { get; set; }
            public string Type { get; set; }
        }
        class Model
        {
            public string Name { get; set; }
            public ClassProperty[] Properties { get; set; }
        }
        static ClassProperty[] userProperties = { 
            new ClassProperty { Name = "Id", IsServerOnly = false, Type = "int" },
            new ClassProperty { Name = "UserName", IsServerOnly = false, Type = "string" },
            new ClassProperty { Name = "Email", IsServerOnly = false, Type = "string" },
            new ClassProperty { Name = "PasswordHash", IsServerOnly = true, Type = "string" },
        };
        static Model user = new Model { Name = "User", Properties = userProperties };
        static void Main(string[] args)
        {
            var fileName = "template.sbn";
            var content = File.ReadAllText(fileName);
            var template = Template.Parse(content, fileName);
            var result = template.Render(new { 
                NameSpace = "CsExample",
                Class = user
            });
            
            var path = @".\user.cs";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(result);
            }
        }
    }
}
