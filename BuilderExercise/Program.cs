using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderExercise
{
    public class CodeBuilder
    {
        public Code newClass;

        public CodeBuilder(string className)
        {
            newClass = new Code(className);
        }

        public CodeBuilder AddField(string name, string type)
        {
            var newField = new Field(name, type);
            newClass.Fields.Add(newField);

            return this;
        }

        public override string ToString()
        {
            return newClass.ToString();
        }
    }

    public class Code
    {
        private const int indentSize = 2;
        public string ClassName { get; }
        public List<Field> Fields { get; set; } = new List<Field>();
        
        public Code() { }

        public Code(string className)
        {
            ClassName = className;
        }

        public override string ToString()
        {
            return ToStringImpl(1);
        }

        public string ToStringImpl(int indent)
        {
            var sb = new StringBuilder($"public class {ClassName}\n{{");
            sb = sb.Append(Environment.NewLine);
            var i = new string(' ', indentSize * indent);

            foreach (var field in Fields)
            {
                sb = sb.Append(i + $"public {field.Type} {field.Name};");
                sb = sb.Append(Environment.NewLine);
            }

            sb = sb.Append(@"}");

            return sb.ToString();
        }
    }

    public class Field
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public Field(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
            Console.ReadKey();
        }
    }
}
