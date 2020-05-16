// Reference:
//   - https://developer.mozilla.org/en-US/docs/Learn/JavaScript/Objects/Inheritance
//   - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Inheritance_and_the_prototype_chain
// 
// Basic idea:
//   "When trying to access a property of an object, the property will not only 
//   be sought on the object but on the prototype of the object, the prototype 
//   of the prototype, and so on until either a property with a matching name 
//   is found or the end of the prototype chain is reached."

exports.demo = function() {
    let s = new Student('Leo', 30, 'male', 100);
    s.greeting();
    console.log(Person.prototype);
    console.log(Student.prototype);
};

function Person(name, age, gender) {
    this.name = name;
    this.age = age;
    this.gender = gender;
}

Person.prototype.greeting = function() {
    console.log(`Hi! I am ${this.name} and I'm ${this.age} years old.`);
};

function Student(name, age, gender, studentId) {
    // "This function basically allows you to call a function defined somewhere 
    // else, but in the current context."
    // Here it means it callls Person constructor function, which is set name, 
    // age and gender for this current object (Student).
    Person.call(this, name, age, gender);
    this.studentId = studentId;
}

// Inherit prototype from Person.
Student.prototype = Object.create(Person.prototype);

// If you don't set Student.prototype.constructor to Student,
// it will take the prototype.constructor of Person (parent).
// To avoid that, we set the prototype.constructor to Student (child).
Object.defineProperty(Student.prototype, 'constructor', { 
    value: Student, 
    enumerable: false, // so that it does not appear in 'for in' loop
    writable: true
});
