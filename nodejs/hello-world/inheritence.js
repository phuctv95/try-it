// Reference: https://developer.mozilla.org/en-US/docs/Learn/JavaScript/Objects/Inheritance

exports.demo = () => {
    let s = new Student('Leo', 30, 'male', 100);
    s.greeting();
};

class Person {
    constructor(name, age, gender) {
        this.name = name;
        this.age = age;
        this.gender = gender;
    }

    greeting() {
        console.log(`Hi! I am ${this.name}, I'm ${this.age} years old.`);
    }
}

class Student extends Person {
    constructor(name, age, gender, studentId) {
        super(name, age, gender);
        this.studentId = studentId;
    }
}
