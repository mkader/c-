## Target-Typed Constructors
1. C# 2.0 - instantiate a class, specify the type (IceCream) twice, variable declaration and calling the constructor
  ``` 
  // declare your type and call the constructor
  IceCream iceCream = new IceCream("Vanilla", 2);
```
2. C# 3.0 - using var keyword, var evaluate at run time
  ```
  // use var to avoid the initial type declaration
  var iceCream = new IceCream("Vanilla", 2);
  ```
3. Now C# 9.0 - With a target-typed constructor, declare the type of a variable and C# will figure out which constructor to call. No list the type twice and use var. 
  ```
  // using a target-typed constructor
  IceCream iceCream = new ("Vanilla", 2);
  ```

## String Interpolation
1. 2001  using string concatenation - hard to read and error prone
  ```
  // VB-style string concatenation
  string msg = "You ordered " + ic.Scoops.ToString() + " scoops of " + ic.Flavor + " on " + DateTime.Now.ToString("MMM dd");
  ```
2.  using composite formatting code - more readable and efficient, but still error prone 
  ```
  // Using a format string and composite formatting
  const string msgFmt = "You ordered {0} scoops of {1} on {2:MMM dd}";
  string msg = string.Format(msgFmt, ic.Scoops, ic.Flavor, DateTime.Now);
  ```
3. Now C# 10 - string interpolation which makes the processes of formatting strings cleaner.
  ```
  // using string interpolation (note the $)
  string msg = $"You ordered {ic.Scoops} scoops of {ic.Flavor}" + 
       $" on {DateTime.Now:MMM-dd}";
  ```

## Null Conditionals
1. C# 2.0 - IceCream class needs a static function that prints out the numberOfScoops,  written the function like this:
    ``` 
    static void PrintScoops(IceCream ic)   {
      Console.WriteLine("Scoops: " + ic.Scoops.ToString()); // not safe,
    }
    ```
    * If null, the code could throw a null reference exception. Because of this, you would have likely included a null check:
      ```
      static void PrintScoops(IceCream ic) {
        if (ic != null) // null check
        {
          Console.WriteLine("Scoops: " + ic.Scoops.ToString()); // safe
        }
      }
      ```
    * Static function to automatically make an ice cream a triple scooper, function like, it's not null safe
      ```
      static void MakeItATriple(IceCream ic) {
        ic.Scoops = 3; // not safe
      }
      ```
    * Null safe 
      ```
      static void MakeItATriple(IceCream ic) {
        if (ic != null) // null check
        {
          ic.Scoops = 3; // safe
        }
      }
      ```
2. Now C# 6.0  introduced the null-conditional operator and C# 14.0 introduced null-conditional assignment.
    * PrintScoops with a null-conditional operator:
      ```
      static void PrintScoops(IceCream ic) {
        // using the null-conditional operator
        Console.WriteLine("Scoops: " + ic?.Scoops.ToString()); // note the ?
      }
      ```
    * Combining with string interpolation and the null-coalescing operator (C# 2.0), even cleaner
      ```
      static void PrintScoops(IceCream ic)
      {
          // prints "Scoops: 0" if ic is null
          Console.WriteLine($"Scoops: {ic?.Scoops ?? 0}");
      }
      ```
    * using null-conditional assignment
      ```
      static void MakeItATriple(IceCream ic)
      {
          // using null-conditional assignment
          ic?.Scoops = 3; // note the ?
      }
      ```

### Value Tuples
1. C# 2.0 - write a function that returns multiple values rather than just one.
    ``` 
    // a function with several out parameters
    static void SplitDate(DateTime date, out int month, out int day, out int year)
    {
        month = date.Month;
        day = date.Day;
        year = date.Year;
    }
    ```
    * Manageable if 2 or 3 parameters OR avoid having to declare multiple individual parameters. Solution, use an encapsulating class to hold the output variables, like this:
    ```
    class DateParts
    {
        public int Month;
        public int Day;
        public int Year;
    
        static DateParts SplitDate(DateTime date)
        {
            return new DateParts()
            {
                Month = date.Month,
                Day = date.Day,
                Year = date.Year
            };
        }
    }
    ```
    * works well, i not only makes the collection of values more portable, reusable and explicitly defined, but it gives you a namespace for the SplitDate function to live in.
    * Problem no need extra code of an encapsulating class.
2. C# 7.- define the variables at the function level,  concept of value tuples.
    * A value tuple is a value type, like a struct, which is implicitly defined by clustering a series of value types in a group. 
    ```
    // using a tuple as the return value
    static (int, int, int) SplitDate(DateTime date)
    {
        return (date.Month, date.Day, date.Year);
    }
    ```
    * One nice feature of value tuples is that they allow you to use field names to distinguish the parts.
    ```
    // with field names
    static (int Month, int Day, int Year) SplitDate(DateTime date)
    {
        return (date.Month, date.Day, date.Year);
    }
    ```
    * Refer to the individual elements by name to avoid confusion:
    ```
    // referring to the elements by field name
    static void UseATuple(DateTime date)
    {
        (int Month, int Day, int Year) parts = SplitDate(date);
    
        if (parts.Month == parts.Day)
        {
            Console.WriteLine("Cool! Month = Day!");
        }
    }
    ```
    * With value tuples equivalence is calculated by comparing the values of all the elements, somewhat like how equivalence is handled for structs and records.
    ```
    // tuple equivalence is based on the values
    static void TestEquivalence(DateTime date)
    {
        // tuple equivalence is based on the values
        (int, int, int) t1 = SplitDate(date);
        (int, int, int) t2 = SplitDate(date);
        (int, int, int) t3 = SplitDate(date.AddDays(1));
    
        if (t1 == t2)
        {
            Console.Write("T1 == T2");
        }
    
        if (t2 != t3)
        {
            Console.Write("and T2 != T3");
        }
    
        // output: T1 == T2 and T2 != T3
    }
    ```

## Pattern Matching Switch Expressions
1. C# 2.0 -  using he if-then statement
    ```
    static string GetFruitName(int input)
    {
        if (input == 1)
        {
            return "Lemon";
        }
        else if (input == 2)
        {
            return "Orange";
        }
        else if (input == 3)
        {
            return "Grape"; //etc...
        }
        else
        {
            return "Unknown";
        }
    }
    ```
    * Call subfunctions like this:
    ```
    static void PeelTheFruit(int input)
    {
        if (input == 1)
        {
            PeelALemon();
        }
        else if (input == 2)
        {
            PeelAnOrange();
        }
        else if (input == 3)
        {
            PeelAGrape(); //etc...
        }
        else
        {
            throw new Exception("invalid input");
        }
    }
    ```
    * Classic switch statement.
    ```
    static string GetFruitName(int input)
    {
        switch (input)
        {
            case 1: return "Lemon";
            case 2: return "Orange";
            case 3: return "Grape";
            default: return "Unknown";
        }
    }
     
    static void PeelTheFruit(int input)
    {
        switch (input)
        {
            case 1:
                PeelALemon();
                break;
            case 2:
                PeelAnOrange();
                break;
            case 3:
                PeelAGrape();
                break;
            default:
                throw new Exception("invalid input");
        }
    }
    ```
2. C# 8.0 - introduced the concept of pattern matching switch expressions.
    ```
    static string GetFruitName(int input)
    {
        return input switch
        {
            1 => "Lemon",
            2 => "Orange",
            3 => "Grape",
            _ => "Unknown" // the discard case
        };
    }
     
    static void PeelTheFruit(int input)
    {
        bool result = input switch
        {
            1 => PeelALemon(),
            2 => PeelAnOrange(),
            3 => PeelAGrape(),
            _ => throw new Exception("invalid input")
        };
    }
    ```
    * using this to build a factory method:
    ``` 
    static IFruit FruitFactory(int input)
    {
        return input switch
        {
            1 => new Lemon(),
            2 => new Orange(),
            3 => new Grape(),
            _ => throw new Exception("invalid input")
        };
    }
    ```
    * using this to await the correct asynchronous function:
    ``` 
    static async Task PeelTheFruitAsync(int input)
    {
        Task t = input switch
        {
            1 => PeelALemonAsync(),
            2 => PeelAnOrangeAsync(),
            3 => PeelAGrapeAsync(),
            _ => throw new Exception("invalid input")
        };
    
        await t;
    }
    ```
    * there are “relational” and “logical” patterns as shown in this function:
    ``` 
    static string GetNumberKind(int input)
    {
        return input switch
        {
            < 0 => "Negative",
            0 => "Zero",
            > 0 and < 10 => "Single Digit",
            11 or 13 or 17 or 19 => "Two digit prime",
            _ => "Some other number"
        };
    }
    ```

## Collection Initializers and Spread
1. c# 2.0 
  ```
  // populating a list with Add
  List<string> animals = new List<string>();
  animals.Add("Iguana");
  animals.Add("Wombat");
  aminals.Add("Crow");
  ```
  * no wrong,  the List<T> class had a constructor that accepts an IEnumerable<T>, rewrite
  ```
  string[] animals = { "Iguana", "Wombat", "Crow" };
  
  // List<T> constructor taking IEnumerable<T>
  List<string> animalsList = new List<string>(animals);
  ```
2. C# 3
  ```
  // using a List Initializer
  List<string> animals = new List<string>()
  {
    "Iguana", "Wombat", "Crow"
  };   
  ```
  * multiple lists and wanted to combine them
  ```
  List<string> mammals = new()
    { "Panda", "Peccary", "Porcupine" };
  
  List<string> reptiles = new()
    { "Gecko", "Iguana", "Python" };
  
  List<string> fish = new()
    { "Trout", "Guppie", "Chub" };
  
  List<string> animals new();
  animals.AddRange(mammals);
  animals.AddRange(reptiles);
  animals.AddRange(fish);
  ```
3. C# 12.0 - introduced collection expressions and the spread element
  ```
  // initialize a list with a collection expression
  List<string> list = ["Iguana", "Wombat", "Crow"];
  ```
  * the spread element (..e), which allows you to unpack or expand an array within a containing collection expression. 
  ```
  string[] mammals = ["Panda", "Peccary", "Porcupine"];
  string[] reptiles = ["Gecko", "Iguana", "Python"];
  string[] fish = ["Trout", "Guppie", "Chub"];
  
  // combine using spread elements (..e)
  List<string> animals = [..mammals, ..reptiles, ..fish];
  
  Console.WriteLine(string.Join(",", animals));
  ```

## Properties
1. C# 2.0 - define a property: use a manual getter and setter
  ```
  class IceCream
  {
      private int _scoops; // backing field
  
      public int Scoops
      {
          get { return _scoops; // getter }
          set { _scoops = value; // setter }
      }
  }
  ```
  * The class hides its internal state and provides controlled methods for accessing that state.
  * The getter and setter provide natural places for doing things like masking, formatting, validation and event signaling.
2. 3.0 - introduced the concept of an auto-property. Getter, and setter are all automatically generated.
  ```
  class IceCream
  {
      public int Scoops { get; set; } // auto-property
  }
  ```
3. C# 6.0 - host of property enhancements including property initializers, read-only properties and arrow functions for property getters and setters.
  ```
  class IceCream
  {
      private int _scoops = 1; // default
  
      public int Scoops
      {
          get { return _scoops; // getter }
          set { _scoops = value; // setter }
      }
  }
  ```
  * With an auto-property, you could not set a default until C# 6 which provided this syntax:
  ``` 
  class IceCream
  {
      public int Scoops { get; set; } = 1; // default
  }
  ```
  * In C# 3.0, define both a get and a set, not allowed a read-only get auto-property. In C# 6.0, this became supported:
  ```
  class IceCream
  {
      public int Scoops { get; } // read only
  }
  ```
   * C# 6.0 also introduced the ability to use arrow functions to implement getters and setters (in C# 7.0). These can be used both with backed properties and auto-properties. Here are some examples:
  ```
  class IceCream
  {
      private int _scoops;
      private string _flavor;
  
      public int Scoops { get => _scoops; }
  
      public string Flavor
      {
          get => _flavor ?? "None";
          set => _flavor = value?.Trim();
      }
  
      public bool IsMultiScoop => (_scoops > 1);
      
  }
  ```
  *  use a full getter or setter is when there is some multi-statement business logic that needs to be implemented.
  ```
  class IceCream
  {
      private int _scoops;
  
      public int Scoops
      {
          get => _scoops;
          set
          {
              if (value < 1 || value > 3)
              {
                  throw new Exception("Unsupported");
              }
  
              if (value != _scoops)
              {
                  _scoops = value;
                  PropChangedEvent("Scoops");
              }
          }
      }
  }
  ```
4. C# 9.0 introduced init-only properties. Imagine you have a property that you want to only be settable during object creation.
  * Prior to C# 9.0, the only way to do this was to use a constructor and make the property read only, like this:
  ```
  class IceCream
  {
      private int _scoops;
  
      // constructor supports setting scoops
      public IceCream(int scoops)
      {
          _scoops = scoops;
      }
  
      // Scoops property is read only
      public int Scoops { get => _scoops; }
  }
  ```
  * In C# 9.0, however, you can declare an auto-property as init-only, which allows a caller to set the value during an object initializer like this:
  ```
  class IceCream
  {
      // init-only auto-property
      public int Scoops { get; init; }
  }
  
  static void ShowUsage()
  {
      IceCream ic = new()
      {
          Scoops = 2 // this compiles
      };
  
      ic.Scoops = 3; // this is not allowed
  }
  ```
5. C# 11.0 introduced the concept of required properties. With a property marked as required, the compiler will check any instantiation of the class and will block compilation if the property is not initialized when exiting the constructor:
  ```
  class IceCream
  {
      // required auto-property
      public required int Scoops { get; set; }
  }
  
  static void ShowUsage()
  {
      // this compiles because we set Scoops
      IceCream ic = new()
      {
          Scoops = 2 // 
      };
  
      // this is not allowed since Scoops not set
      IceCream ic = new();
  }
  ```
