

using LanguageExt;


var persons = new [] { new Person("Jon", 1), new Person("Peter", 2) }.ToList();
var animals = new[] { new Animal("Cat", 1), new Animal("Dog", 2) }.ToList();

Either<Person, Animal> p = persons[0];
Either<Person, Animal> a = animals[0];
IEnumerable<Either<Person, Animal>> lst = new[] { p, a };

var allNames = persons.Bind(x => x.Name);
var allNames2 = persons.SelectMany(x=>x.Name);

var allNames3 = persons.Map(x => x.Name);


Console.WriteLine("bye");

string ToUpper(string s) => s.ToUpper();
int ChangeID(int ID) => ID++;

record struct Person(string Name, int ID) { }
record struct Animal(string Name, int ID) { }
record struct Order(int ID) { }

