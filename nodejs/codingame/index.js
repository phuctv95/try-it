class A {
    constructor() {
        this.x = 11;
    }
    foo() {
        [1, 2, 3].forEach(item => log(this));
    }
}

function log(a) { console.log(a); }

let a = new A();
a.foo();
