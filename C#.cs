using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program{
	static void Main(){
		var person = new Person(10);
		person.Age = 10;
		Console.WriteLine (person.Age);
		Human human = new Person ();
		human.Eat();//Human Eat
		((Person)human).Eat();//Person Eat
		Console.ReadLine();
	}
}

class Human{
	public Human(){
		Console.WriteLine ("Human default Constructor");
	}

	public Human(int temp){
		Console.WriteLine ("Human Constructor");
	}

	public virtual void Eat(){
		Console.WriteLine ("Human Eat");
	}
}

class Person: Human{
	int age;
	public Person(){
		Console.WriteLine ("Person default Constructor");
	}

	public Person(int temp):base(temp){
		Console.WriteLine ("Person Constructor");
		age=temp;
	}

	public new void Eat(){
		Console.WriteLine ("Person Eat");
	}
//	public int getAge(){
//		return age;
//	}
	public int Age{
		get{return age;}
		set{age =value + 10;}
	}
}