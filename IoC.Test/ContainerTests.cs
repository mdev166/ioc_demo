using System;
using Xunit;

namespace IoC.Test
{
    public class ContainerTests
    {
        [Fact]
        public void Resolve_SucceedsTest()
        {
            IContainer container = new IoC.Container();
            container.Register<ITestClass2, TestClass2>(LifeCycleType.Singleton);
            container.Register<ITestClass3, TestClass3>();
            container.Register<ITestClass4, TestClass4>();
            container.Register<ITestClass, TestClass>(LifeCycleType.Transient);

            var actual = container.Resolve(typeof(ITestClass)) as ITestClass;

            Assert.NotNull(actual);
            Assert.NotNull(actual.TestClassProp2);
            Assert.NotNull(actual.TestClassProp3);
            Assert.NotNull(actual.TestClassProp3.TestClassProp4);
            Assert.Equal("TestClass2", actual.GetMyString());
        }

        [Fact]
        public void Resolve_ThrowsTypeNotRegisteredExceptionTest()
        {
            IContainer container = new IoC.Container();
            container.Register<ITestClass2, TestClass2>();
            // leave ITestClass3 out for test
            container.Register<ITestClass4, TestClass4>();
            container.Register<ITestClass, TestClass>();

            Exception actual = Assert.Throws<TypeNotRegisteredException>(
                () => container.Resolve(typeof(ITestClass)));

            Assert.NotNull(actual);
            Assert.Equal("Type 'IoC.Test.ContainerTests+ITestClass3' not registered with Container instance.", actual.Message);
        }

        [Fact]
        public void Register_LifeCycleTypeSingletonSucceedsTest()
        {
            IContainer container = new IoC.Container();
            container.Register<ITestClass4, TestClass4>();
            container.Register<ITestClass2, TestClass2>(LifeCycleType.Singleton);
            container.Register<ITestClass3, TestClass3>(LifeCycleType.Singleton);
            container.Register<ITestClass, TestClass>(LifeCycleType.Singleton);

            var actual1 = container.Resolve(typeof(ITestClass)) as ITestClass;
            var actual2 = container.Resolve(typeof(ITestClass)) as ITestClass;

            Assert.NotNull(actual1);
            Assert.NotNull(actual2);
            Assert.Equal(actual1, actual2);
            Assert.Equal(actual1.TestClassProp2, actual2.TestClassProp2);
            Assert.Equal(actual1.TestClassProp3, actual2.TestClassProp3);
        }

        [Fact]
        public void Register_LifeCycleTypeTransientSucceedsTest()
        {
            IContainer container = new IoC.Container();
            container.Register<ITestClass4, TestClass4>();
            container.Register<ITestClass2, TestClass2>(LifeCycleType.Transient);
            container.Register<ITestClass3, TestClass3>(LifeCycleType.Transient);
            container.Register<ITestClass, TestClass>(LifeCycleType.Transient);

            var actual1 = container.Resolve(typeof(ITestClass)) as ITestClass;
            var actual2 = container.Resolve(typeof(ITestClass)) as ITestClass;

            Assert.NotNull(actual1);
            Assert.NotNull(actual2);
            Assert.NotEqual(actual1, actual2);
            Assert.NotEqual(actual1.TestClassProp2, actual2.TestClassProp2);
            Assert.NotEqual(actual1.TestClassProp3, actual2.TestClassProp3);
        }

        [Fact]
        public void UnRegister_Succeeds()
        {
            IContainer container = new IoC.Container();
            container.Register<ITestClass4, TestClass4>();

            var actual = container.Resolve(typeof(ITestClass4)) as ITestClass4;

            Assert.NotNull(actual);

            container.UnRegister<ITestClass4>();

            Exception ex = Assert.Throws<TypeNotRegisteredException>(() => container.Resolve(typeof(ITestClass4)));
            Assert.NotNull(ex);
            Assert.Equal("Type 'IoC.Test.ContainerTests+ITestClass4' not registered with Container instance.", ex.Message);
        }

        #region helper test classes

        public interface ITestClass
        {
            ITestClass2 TestClassProp2 { get; }
            ITestClass3 TestClassProp3 { get; }
            string GetMyString();
        }

        public class TestClass : ITestClass
        {
            private readonly ITestClass2 _testClass2;
            private readonly ITestClass3 _testClass3;

            public TestClass(ITestClass2 testClass2, ITestClass3 testClass3)
            {
                _testClass2 = testClass2;
                _testClass3 = testClass3;
            }

            public ITestClass2 TestClassProp2 {
                get { return _testClass2; }
            }

            public ITestClass3 TestClassProp3
            {
                get { return _testClass3; }
            }

            public string GetMyString()
            {
                return _testClass2.GetMyString();
            }
        }

        public interface ITestClass2
        {
            string GetMyString();
        }

        public class TestClass2 : ITestClass2
        {
            public string GetMyString()
            {
                return "TestClass2";
            }
        }

        public interface ITestClass3
        {
            ITestClass4 TestClassProp4 { get; }
        }

        public class TestClass3 : ITestClass3
        {
            ITestClass4 _testClass4;
            public TestClass3(ITestClass4 testClass4)
            {
                _testClass4 = testClass4;
            }

            public ITestClass4 TestClassProp4
            {
                get { return _testClass4; }
            }
        }

        public interface ITestClass4
        {
        }

        public class TestClass4 : ITestClass4
        {
        }

        #endregion helper test classes
    }
}
