using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TinyFactoryGirl.Tests
{
    public class User : IEquatable<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; private set; }

        public void SetAge(int age)
        {
            Age = age;
        }

        public bool Equals(User other)
        {
            return other != null &&
                   other.Name == Name &&
                   other.Email == Email;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Email.GetHashCode() ^ Age.GetHashCode();
        }
    }

    [TestClass]
    public class TinyFactoryGirlTests
    {
        [TestInitialize]
        public void Init()
        {
            TinyFactoryGirl.ClearDefinitions();
        }

        [TestMethod]
        public void When_Try_Build_A_Defined_Object_Then_Should_Returns_It()
        {
            //arrange
            var original = new User
                               {
                                   Name = "John Due",
                                   Email = "john.due@example.org"
                               };

            TinyFactoryGirl.Define(() => original);

            //act
            var newUser = TinyFactoryGirl.Build<User>();

            //assert
            Assert.AreEqual(original, newUser);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundDefinitionException))]
        public void When_Try_Build_A_Undefined_Object_Then_Should_Throw_Exception()
        {
            //arrange

            //act
            TinyFactoryGirl.Build<User>();

            //assert
            
        }

        [TestMethod]
        public void When_Exists_More_Than_One_Defined_Object_Same_Type_And_Try_Build_By_Alias_Then_Should_Return_Correct_Instance()
        {
            //arrange
            TinyFactoryGirl.Define(() => new User{ Name = "John Due"});
            TinyFactoryGirl.Define("male", () => new User { Name = "Jane Due" });

            //act
            var user = TinyFactoryGirl.Build<User>("male");

            //assert
            Assert.AreEqual("Jane Due", user.Name);
        }

        [TestMethod]
        public void When_Clean_Definitions_Then_Should_Be_Possible_Create_One_Definition_The_Same_Type_Of_The_Previous()
        {
            //arrange
            TinyFactoryGirl.Define(() => new User { Name = "John Due" });
            TinyFactoryGirl.ClearDefinitions();
            TinyFactoryGirl.Define(() => new User { Name = "Jane Due" });

            //act
            var user = TinyFactoryGirl.Build<User>();

            //assert
            Assert.AreEqual("Jane Due", user.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsDefinitionExceptiion))]
        public void When_Try_Create_A_Already_Definition_With_Alias_Then_Should_Throw_Exception()
        {
            //arrange
            TinyFactoryGirl.Define("user", () => new User { Name = "John Due" });
            TinyFactoryGirl.Define("user", () => new User { Name = "Jane Due" });

            //act
            
            //assert
        }
    }
}
