# ShouldlyExtension
The code you have provided is a custom extension method for the Shouldly assertion library in .NET applications. This method allows you to assert that two objects have equivalent property values, with the option to include only certain properties in the comparison.

### Overview:

The CustomShouldlyExtension class contains two extension methods:

ShouldBeEquivalentTo<T> - This method compares two objects of type T and asserts that they have the same property values. You can provide an array of property expressions to include only certain properties in the comparison. You can also provide a custom error message if the assertion fails.

ShouldBeEquivalentTo<T> - This method is similar to the first method, but it allows you to compare one object of type T to a collection of objects of type T. The method calls the first method for each object in the collection.

### Usage:

To use the ShouldBeEquivalentTo extension methods in your .NET application, you will need to add the using Shouldly; and using ShouldlyExtension; directives at the top of your C# file.
Here is an example of how to use the ShouldBeEquivalentTo method to assert that two objects of type Person have the same name and age:
  
  <pre>
using Shouldly;
using ShouldlyExtension;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

// Create two Person objects to compare
var person1 = new Person { Name = "Alice", Age = 30 };
var person2 = new Person { Name = "Alice", Age = 30 };

// Assert that the two objects have the same name and age
person1.ShouldBeEquivalentTo(person2, 
    new Expression<Func<Person, object>>[] { p => p.Name, p => p.Age });
  </pre>
  In the example above, we are using the ShouldBeEquivalentTo method to compare two Person objects and assert that they have the same Name and Age properties. We have provided an array of property expressions to include only those two properties in the comparison.

  If the two objects do not have the same Name and Age properties, the assertion will fail and Shouldly will provide a descriptive error message to help identify the issue.
