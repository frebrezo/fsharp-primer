# fsharp-primer

## F# introduction
* Initially published in 2005
* Maintained by F# Software Foundation, Microsoft, and individual contributors
* MIT License
* Inpired by Ocaml, Haskell, Python, and C#
  * C# for compatibility with the .NET platform, object-oriented and imperative programming models

## Programming paradigms
* Imperative vs declarative programming
  * Imperative programming describes HOW to compute the result/behavior
    * Implies state, further implies, side effects
    * NULL value handling
  * Declarative programming describes WHAT is to be computed
* Imperative languages
  * C – structured (procedural/functional)
  * C++ – structured and object-oriented
  * C#/Java – object-oriented (based on C++, arguably, not truly object oriented)
  * SmallTalk – object-oriented
* Declarative languages
  * Haskell, Lisp, OCaml, Erlang, etc.
* SQL is a form of declarative programming
  * When dealing with SQL, it’s important to think of the data as a WHOLE, not each record
  * Think set theory

## F# language fundamentals
* Function-oriented
* Expressions
* Algebraic types
* Pattern matching

## Function-oriented
* Functions are first class entities and can be passed to other expressions and it’s not weird
* Functions are treated the same as variables
* C#
  * `public static double Add(double x, double y) => x + y;`
    * Expresson bodied method operator (=>) was added in C# 6.0 to make C# for functional
  * `public static double Calc(Func<double, double, double> op, double x, double y) => op(x, y);`
    * Note the Func<…> notation. WEIRD!
      * Previoulsy, had to use a delegate. WEIRDER!
    * C# added the method expression to make the language more functional
  * **`var result = Calc(Add, 1.0d, 2.0d);`**
* F#
  * `let add x y = x + y`
  * `let calc op x y = op x y`
  * **`let result = calc add 1.0 2.0`**

# Expressions
* F# uses expressions over statements
* For example, a pangram is a string that contains every word in the alphabet, for example, string "The quick brown fox jumps over the lazy dog."
  * C# - statements
    * Loop over alphabet “abcdefghijklmnopqrstuvwxyz”
      * isPangram = false
      * Loop over string
        * If char in string = char in alphabet then
          * isPangram = true
        * If not isPangram then
          * Exit, not pangram
    * Exit, is pangram
* F# - expressions
  * Take the string -> lower case -> convert to character array -> filter out non digits and non-characters -> get distinct characters-> sort -> convert back string
  * Compare resulting string with alphabet “abcdefghijklmnopqrstuvwxyz”

# Algebraic types
* New compound types can be created using existing types
  * Product type (tuples)
  * Sum type (discriminated union)
    * Optional type
      * None == NULL, Some keyword checks for a value
      * F# does not have the concept of NULL values (except string)
      * Forces developer to DEFINE a variable as nullable
        * let salary : Option<int>
* Non-algebraic types
  * Class
  * Enumerations (Enums)

# F# vs C# language construct differences
* Discriminated union vs Enum
  * Enums DO NOT offer type safety
    * Enums are syntactic sugar over primitive integral types, thus are evaluated FASTER than discriminated unions
    * Code must check enum received from client is valid using Enum.IsDefined
  * Enums Only hold 1 value
  * Enums can be used to hold bit-wise flags
  * Discriminated unions are reference types
* Record (tuple) vs Class
  * Records and classes are similar in usage
  * Compiler generates structural equality for structural comparison for records
  * Use classes for cross .NET platform compatibility

## Pattern matching
* On the surface, pattern matching looks like the switch…case flow control construct (if…then…else)
* Pattern matching is a combination of a flow control AND binding construct
* Exhaustive, F# compiler WILL generate warning if case is left out
* Zip/Zap/Zoom example
  * Zip if num divisible by 3, Zap if num divisible by 5, Zoom if num divisible by 3 and 5, otherwise, Invalid
```
match num with
| x when zip x -> "Zip"
| x when zap x -> "Zap"
| x when zoom x -> "Zoom"
| _ -> "Invalid"
```

## Domain specific language (DSL) in F#
* Defining a language within the language itself that looks like pseudo code but compiles and executes
* Modeling with types
  * Modeling the domain
  * Types can hold extra data (discriminated union)
* F# is white space significant
  * White space is used to delineate sub-statements, not special characters { … }
* No return keyword
  * Last expression in function is the return value
* F# supports type inferencing
  * F# compiler in most cases can determine what the types of parameters are without explicit typing
  * type CardNumber = CardNumber of string
  * let cardNumber = CardNumber "1234-5678-9012-3456“
* Record
  * Compared by value
  * Non-nullable, must be initialized
  * Immutable
* Pipe operator (|>)
  * Allows greater readability
  * String.IsNullOrEmptyString str vs str |> String.IsNullOrEmptyString
* Partial application
  * Functions called without required parameter will automatically expect calling function to accept additional parameter
```
let multiply x y = x * y
let percent = multiply 100
let result = percent 1 // result = 100
```

## Unit testing F# code
* F# being a declarative language doesn’t need the same level of unit testing that an imperative language requires
* When to unit test F# code
  * Complex business rules, especially conditional logic
  * Parsing
* Not required
  * Expressions – F# code is generally written as expressions, like math formulas, with immutable values
  * Pattern matching – F# compiler WARNINGS are important. F# compiler will warn you if you’ve missed a condition
  * Single case discriminated union – Discriminated unions prevent you from mixing up fields
  * Option types – There are NO NULLs in F#. Only the absence of a value, when such a case truly exists

## References (unsorted)
* https://inst.eecs.berkeley.edu/~cs61a/sp14/slides/31_6pp.pdf
* https://en.wikipedia.org/wiki/Programming_paradigm
* https://en.wikipedia.org/wiki/Imperative_programming
* https://en.wikipedia.org/wiki/Declarative_programming
* https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/
* https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/loops-for-in-expression
* https://riptutorial.com/fsharp/example/7373/intro-to-folds--with-a-handful-of-examples
* https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/values/null-values
* https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/sequences
* https://blog.submain.com/csharp-functional-programming/
* https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/tuples
* https://thomasbandt.com/fsharp-introduction
* https://fsharpforfunandprofit.com/posts/key-concepts/
* https://fsharpforfunandprofit.com/posts/let-use-do/
* https://stackoverflow.com/questions/450335/f-keyword-some
* https://sergeytihon.com/2013/04/10/f-null-trick/
* https://github.com/lefthandedgoat/canopy
* https://theburningmonk.com/2011/10/fsharp-enums-vs-discriminated-unions/
* https://stackoverflow.com/questions/41410296/f-record-vs-class
* https://fsharpforfunandprofit.com/posts/the-option-type/
* https://stackoverflow.com/questions/199918/explaining-pattern-matching-vs-switch
* https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/match-expressions
* https://www.youtube.com/watch?v=NoGyFQ99NgY
