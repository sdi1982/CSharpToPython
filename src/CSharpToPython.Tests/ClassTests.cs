﻿using System;
using Xunit;

namespace CSharpToPython.Tests {
    public class ClassTests {

        private readonly EngineWrapper engine = new EngineWrapper();

        [Fact]
        public void SimpleClassWorks() {
            var code = @"
public class SomeClass {
    int Main() { return 1; }
}";
            var rslt = Program.ConvertAndRunCode(engine, code);
            Assert.NotNull(rslt);
        }

        [Fact]
        public void InstanceMethodWorks() {
            var code = @"
public class SomeClass {
    public int GetInt() { return 1; }
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            int theInt = rslt.GetInt();
            Assert.Equal(1, theInt);
        }

        [Fact]
        public void StaticMethodWorks() {
            var code = @"
public class SomeClass {
    public static int StaticGetInt() { return 1; }
    public int GetInt() { return SomeClass.StaticGetInt(); }
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            int theInt = rslt.GetInt();
            Assert.Equal(1, theInt);
        }

        [Fact]
        public void FieldWithInitializerWorks() {
            var code = @"
public class SomeClass {
    public int SomeInt = 1;
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            int someInt = rslt.SomeInt;
            Assert.Equal(1, someInt);
        }
        [Fact]
        public void FieldWithoutInitializerWorks() {
            var code = @"
public class SomeClass {
    public object SomeValue;
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            object someValue = rslt.SomeValue;
            Assert.Null(someValue);
        }
        [Fact]
        public void StaticFieldWithoutInitializerWorks() {
            var code = @"
public class SomeClass {
    public static object SomeValue;
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            object someValue = rslt.SomeValue;
            Assert.Null(someValue);
        }
        [Fact(Skip = "Not implemented yet. Need to figure out best way to get default value for a type.")]
        public void ValueTypeFieldWithoutInitializerWorks() {
            var code = @"
public class SomeClass {
    public int SomeValue;
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            object someValue = rslt.SomeValue;
            Assert.Equal(0, someValue);
        }

        [Fact]
        public void PropertyWorks() {
            var code = @"
public class SomeClass {
    int _someValue;
    public int SomeValue { get { return this._someValue; } set { this._someValue = value; }
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            rslt.SomeValue = 2;
            int someValue = rslt.SomeValue;
            Assert.Equal(2, someValue);
        }

        [Fact]
        public void ExpressionBodiedPropertyWorks() {
            var code = @"
public class SomeClass {
    public int SomeValue => 1;
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            int someValue = rslt.SomeValue;
            Assert.Equal(1, someValue);
        }

        [Fact]
        public void AutoPropertyWorks() {
            var code = @"
public class SomeClass {
    public int SomeValue { get; set; } = 1;
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            int someValue = rslt.SomeValue;
            Assert.Equal(1, someValue);
        }

        [Fact(Skip = "Not sure how to implement static properties")]
        public void StaticPropertyWorks() {
            var code = @"
public class SomeClass {
    public static int SomeValue { get { return 1; } }
    public int GetInt() { return SomeClass.SomeValue; }
}";
            dynamic rslt = Program.ConvertAndRunCode(engine, code);
            int someValue = rslt.GetInt();
            Assert.Equal(1, someValue);
        }

        [Fact]
        public void NamespaceWorks() {
            var code = @"
namespace SomeNamespace {
    public class SomeClass { }
}";
            var rslt = Program.ConvertAndRunCode(engine, code);
            Assert.NotNull(rslt);
        }

        [Fact]
        public void UsingDirectiveWorks() {
            var code = @"
using System;
class SomeClass {
   public object GetObj() { return new Random() ; }
}
";
            dynamic someClass = Program.ConvertAndRunCode(engine, code);
            var obj = (object)someClass.GetObj();
            Assert.IsType<System.Random>(obj);
        }

    }
}
